package GameSession

import (
	"../ChessData"
	"net"
	"fmt"
	"encoding/json"
	"bufio"
	"os"
)

type Session struct {
	SessionID int `json:"SessionID,omitempty"`
	HostPlayer string `json:"HostPlayer,omitempty"`
	GuestPlayer string `json:"GuestPlayer,omitempty"`
	GameTimerSeconds int `json:"GameTimerSeconds,omitempty"`
	MoveTimerSeconds int `json:"MoveTimerSeconds,omitempty"`
	BoardPieces [][]ChessData.Piece `json:"BoardPieces,omitempty"`
	CustomGameMode int `json:"CustomGameMode,omitempty"`
	GameID int `json:"GameID,omitempty"`
	GuestColor int `json:"GuestColor,omitempty"`
}

type TCPSignal struct {
	SignalType int `json:"SignalType,omitempty"`
	SessionID int `json:"SessionID,omitempty"`
	HostPlayerName string `json:"HostPlayerName,omitempty"`
	GuestPlayerName string `json:"GuestPlayerName,omitempty"`
	PlayerMove ChessData.Move `json:"PlayerMove,omitempty"`
	NewSession Session `json:"NewSession,omitempty"`
	PlayerMessage string `json:"PlayerMessage,omitempty"`
}

const EXIT_FAILURE = 1
var activeSessions []Session

func GetSessions(client *Client) {
	jsonBytes, err := json.Marshal(activeSessions)
	if err != nil {
		fmt.Println("Error: ", err.Error())
		return
	}
	client.WriteDataString(string(jsonBytes))
}

func CreateSession(client *Client, session *Session) {
	session.SessionID = len(activeSessions) + 1
	activeSessions = append(activeSessions, *session)
	client.session = &activeSessions[len(activeSessions) - 1]
}

func JoinSession(client *Client, sessionID int, guestPlayerName string, hostPlayerName string) {
	found := false
	for clientList, _ := range allClients {
		if clientList.session != nil && clientList.session.SessionID == sessionID {
			found = true;
			client.connection = clientList
			clientList.connection = client
			client.session = clientList.session
			signal := new(TCPSignal)
			signal.SignalType = 7
			client.session.GuestPlayer = guestPlayerName
			client.session.HostPlayer = hostPlayerName
			signal.NewSession = *client.session
			jsonBytes, _ := json.Marshal(signal)
			client.WriteDataInt(1)
			client.WriteDataString(string(jsonBytes))
			clientList.WriteDataString(string(jsonBytes))
			break
		}
	}
	if found == false {
		client.WriteDataInt(0)
		return
	}

}

func MakeMove(client *Client, move ChessData.Move) {
	signal := new(TCPSignal)
	signal.SignalType = 2
	signal.PlayerMove = move
	jsonBytes, _ := json.Marshal(signal)
	client.connection.WriteDataString(string(jsonBytes))
}

func SendMessage(client *Client, message string) {
	tcpSignal := new(TCPSignal)
	tcpSignal.PlayerMessage = message
	jsonBytes, _ := json.Marshal(tcpSignal)
	sentMessage := string(jsonBytes)
	client.connection.outgoing <- sentMessage
}

func DeleteSession(client *Client, sessionID int) {
	for i := 0; i < len(activeSessions); i++  {
		if activeSessions[i].SessionID == sessionID {
			activeSessions = append(activeSessions[:i], activeSessions[i+1:]...)
			return
		}
	}
}

func ListenOnTCP() {
	allClients = make(map[*Client]int)
	listener, err := net.Listen("tcp", "localhost:2346")
	if err != nil {
		fmt.Println("Error setting up TCP: ", err.Error())
		os.Exit(EXIT_FAILURE)
	}
	fmt.Println("Listening on :2346")
	for {
		conn, err := listener.Accept()
		if err != nil {
			fmt.Println(err.Error())
		}
		client := NewClient(conn)
		allClients[client] = 1
		fmt.Println(len(allClients))
	}
}

var allClients map[*Client]int

type Client struct {
	// incoming chan string
	session *Session
	outgoing   chan string
	reader     *bufio.Reader
	writer     *bufio.Writer
	conn       net.Conn
	connection *Client
}

func (client *Client) Read() {
	for {
		line, err := client.reader.ReadString('\r')
		if err == nil {
			tcpSignal := new(TCPSignal)
			byteData := []byte(line)
			err = json.Unmarshal(byteData[:len(line)], &tcpSignal)
			if err != nil {
				fmt.Println("Error: ", err.Error())
				continue
			}
			if tcpSignal.SignalType == 1 {
				CreateSession(client, &tcpSignal.NewSession)
			} else if tcpSignal.SignalType == 2 {
				MakeMove(client, tcpSignal.PlayerMove)
			} else if tcpSignal.SignalType == 3 {
				GetSessions(client)
			} else if tcpSignal.SignalType == 4 {
				SendMessage(client, tcpSignal.PlayerMessage)
			} else if tcpSignal.SignalType == 5 {
				JoinSession(client, tcpSignal.SessionID, tcpSignal.GuestPlayerName, tcpSignal.HostPlayerName)
			} else if tcpSignal.SignalType == 6 {
				DeleteSession(client, tcpSignal.SessionID)
			}
		} else {
			break
		}
	}

	client.conn.Close()
	delete(allClients, client)
	if client.connection != nil {
		client.connection.connection = nil
	}
	client = nil
}

func (client *Client) WriteDataString(data string) {
	client.writer.WriteString(data)
	client.writer.Flush()
}

func (client *Client) WriteDataInt(data int) {
	client.writer.WriteByte(byte(data))
	client.writer.Flush()
}

func (client *Client) Write() {
	for data := range client.outgoing {
		client.writer.WriteString(data)
		client.writer.Flush()
	}
}

func (client *Client) Listen() {
	go client.Read()
	go client.Write()
}

func NewClient(connection net.Conn) *Client {
	writer := bufio.NewWriter(connection)
	reader := bufio.NewReader(connection)

	client := &Client{
		// incoming: make(chan string),
		outgoing: make(chan string),
		conn:     connection,
		reader:   reader,
		writer:   writer,
	}
	client.Listen()

	return client
}