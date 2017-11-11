package main

import (
	"./ChessData"
	"./GameSession"
	_ "github.com/denisenkom/go-mssqldb"
)

func main() {
	go GameSession.ListenOnTCP()
	ChessData.RunServer()
}