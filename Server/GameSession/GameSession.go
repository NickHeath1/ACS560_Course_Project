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
	SessionID int
	HostPlayer string
	GuestPlayer string
	GameTimerMinutes int
	MoveTimerSeconds int
	HostPlayerPieces [][]ChessData.Piece
	GuestPlayerPieces [][]ChessData.Piece
}

type TCPSignal struct {
	SignalType int `json:"SignalType,omitempty"`
	SessionID int `json:"SessionID,omitempty"`
	PlayerMove ChessData.Move `json:"PlayerMove,omitempty"`
	NewSession Session `json:"NewSession,omitempty"`
	PlayerMessage string `json:"PlayerMessage,omitempty"`
}

const EXIT_FAILURE = 1
var activeSessions []Session

//func ListenOnTCP() {
//	listener, err := net.Listen("tcp", "localhost:2346")
//	if err != nil {
//		log.Fatal(err.Error())
//		os.Exit(EXIT_FAILURE)
//	}
//	defer listener.Close()
//	fmt.Println("Listening on localhost:2346")
//	for {
//		conn, err := listener.Accept()
//		if err != nil {
//			fmt.Println("Error: ", err.Error())
//			continue
//		}
//		tcpSignal := new(TCPSignal)
//		byteData := make([]byte, 4096)
//		n, err := conn.Read(byteData)
//		if err != nil {
//			fmt.Println("Error: ", err.Error())
//			continue
//		}
//		err = json.Unmarshal(byteData[:n], &tcpSignal)
//		if err != nil {
//			fmt.Println("Error: ", err.Error())
//			continue
//		}
//		if tcpSignal.SignalType == 1 {
//			CreateSession(conn, tcpSignal.NewSession)
//		} else if tcpSignal.SignalType == 2 {
//			MakeMove(conn, tcpSignal.PlayerMove, tcpSignal.SessionID)
//		} else if tcpSignal.SignalType == 3 {
//			GetSessions(conn)
//		} else if tcpSignal.SignalType == 4 {
//			SendMessage(conn, tcpSignal.PlayerMessage)
//		}
//	}
//}

func GetSessions(client *Client) {
	jsonBytes, err := json.Marshal(activeSessions)
	if err != nil {
		fmt.Println("Error: ", err.Error())
		return
	}
	client.WriteData(string(jsonBytes))
}

func CreateSession(client *Client, session *Session) {
	session.SessionID = len(activeSessions) + 1
	activeSessions = append(activeSessions, *session)
	client.session = &activeSessions[len(activeSessions) - 1]
}

func JoinSession(conn net.Conn, session Session) {

}

func MakeMove(client *Client, move ChessData.Move) {
	if move.Player == 1 {
		client.session.HostPlayerPieces[move.Source.XYCoordinates.X - 1][move.Source.XYCoordinates.Y - 1].Name = "NoPiece"
		client.session.HostPlayerPieces[move.Destination.XYCoordinates.X - 1][move.Destination.XYCoordinates.Y - 1].Name = move.Destination.Name
	} else if move.Player == 2 {
		client.session.GuestPlayerPieces[move.Source.XYCoordinates.X - 1][move.Source.XYCoordinates.Y - 1].Name = "NoPiece"
		client.session.GuestPlayerPieces[move.Destination.XYCoordinates.X - 1][move.Destination.XYCoordinates.Y - 1].Name = move.Destination.Name
	}
}

func SendMessage(client *Client, message string) {

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

		//for clientList, _ := range allClients {
		//	if clientList.connection == nil {
		//		client.connection = clientList
		//		clientList.connection = client
		//		fmt.Println("Connected")
		//	}
		//}
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
						client.connection.outgoing <- tcpSignal.PlayerMessage
					}

			if client.connection != nil {
				client.connection.outgoing <- line
			}
			fmt.Println(line)
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

func (client *Client) WriteData(data string) {
	client.writer.WriteString(data)
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