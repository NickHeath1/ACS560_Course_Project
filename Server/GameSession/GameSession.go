package GameSession

import (
	"../ChessData"
	"net"
	"log"
	"os"
	"fmt"
)

type Session struct {
	SessionID int
	HostPlayer string
	GuestPlayer string
	GameTimerMinutes int
	MoveTimerSeconds int
	HostPlayerPieces []ChessData.Piece
	GuestPlayerPieces []ChessData.Piece
}

const EXIT_FAILURE = 1
var activeSessions []Session

func ListenOnTCP() {
	listener, err := net.Listen("tcp", "localhost:2345")
	if err != nil {
		log.Fatal(err.Error())
		os.Exit(EXIT_FAILURE)
	}
	defer listener.Close()
	fmt.Println("Listening on localhost:2345")
	for {
		conn, err := listener.Accept()
		if err != nil {
			fmt.Println("Error: ", err.Error())
			continue
		}

		conn.Close()
	}
}

func CreateSession(conn net.Conn) {
	var session Session
	// TODO Create session
	activeSessions = append(activeSessions, session)
	conn.Write([]byte("Success"))
}

func MakeMove(conn net.Conn) {
	for _, session := range activeSessions {

	}
}