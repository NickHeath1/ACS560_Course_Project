package GameSession

import (
	"../ChessData"
	"net"
	"log"
	"os"
	"fmt"
	"encoding/json"
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

func ListenOnTCP() {
	listener, err := net.Listen("tcp", "localhost:2346")
	if err != nil {
		log.Fatal(err.Error())
		os.Exit(EXIT_FAILURE)
	}
	defer listener.Close()
	fmt.Println("Listening on localhost:2346")
	for {
		conn, err := listener.Accept()
		if err != nil {
			fmt.Println("Error: ", err.Error())
			continue
		}
		tcpSignal := new(TCPSignal)
		byteData := make([]byte, 4096)
		n, err := conn.Read(byteData)
		if err != nil {
			fmt.Println("Error: ", err.Error())
			continue
		}
		err = json.Unmarshal(byteData[:n], &tcpSignal)
		if err != nil {
			fmt.Println("Error: ", err.Error())
			continue
		}
		if tcpSignal.SignalType == 1 {
			CreateSession(conn, tcpSignal.NewSession)
		} else if tcpSignal.SignalType == 2 {
			MakeMove(conn, tcpSignal.PlayerMove, tcpSignal.SessionID)
		} else if tcpSignal.SignalType == 3 {
			GetSessions(conn)
		} else if tcpSignal.SignalType == 4 {
			SendMessage(conn, tcpSignal.PlayerMessage)
		}
	}
}

func GetSessions(conn net.Conn) {
	jsonBytes, err := json.Marshal(activeSessions)
	if err != nil {
		fmt.Println("Error: ", err.Error())
		return
	}
	conn.Write(jsonBytes)
}

func CreateSession(conn net.Conn, session Session) {
	activeSessions = append(activeSessions, session)
	conn.Write([]byte("Success"))
}

func MakeMove(conn net.Conn, move ChessData.Move, SessionID int) {
	for _, session := range activeSessions {
		if SessionID == session.SessionID {
			if move.Player == 1 {
				session.HostPlayerPieces[move.Source.XYCoordinates.X - 1][move.Source.XYCoordinates.Y - 1].Name = "NoPiece"
				session.HostPlayerPieces[move.Destination.XYCoordinates.X - 1][move.Destination.XYCoordinates.Y - 1].Name = move.Destination.Name
			} else if move.Player == 2 {
				session.GuestPlayerPieces[move.Source.XYCoordinates.X - 1][move.Source.XYCoordinates.Y - 1].Name = "NoPiece"
				session.GuestPlayerPieces[move.Destination.XYCoordinates.X - 1][move.Destination.XYCoordinates.Y - 1].Name = move.Destination.Name
			}
			break
		}
	}
}

func SendMessage(conn net.Conn, message string) {
	conn.Write([]byte(message))
}