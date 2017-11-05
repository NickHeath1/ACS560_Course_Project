package main

import (
	"./ChessData"
	"net/http"
	"log"
	"encoding/json"
	_ "github.com/denisenkom/go-mssqldb"
	"database/sql"
	"fmt"
)

type Person struct {
	ID string `json:"id,omitempty"`
	Name string `json:"name,omitempty"`
}

func main() {
	ChessData.RunServer()
}

func Test(w http.ResponseWriter, r *http.Request) {
	var people []Person
	var newPeople []Person
	json.NewDecoder(r.Body).Decode(&people)
	newPeople = make([]Person, len(people))
	i := 0
	for _, person := range people {
		newPeople[i] = person
		newPeople[i].Name = "Copycat!"
		i++
	}
	json.NewEncoder(w).Encode(newPeople)
	GetSQLData()
}

func GetSQLData() {
	conn, err := sql.Open("sqlserver", `sqlserver://ChessGameService:ILikeChicken@(local)/SQLExpress?database=ChessGame&connection+timeout=30`)
	if err != nil {
		log.Fatal(err.Error())
	}
	defer conn.Close()
	stmt, err := conn.Prepare("SELECT Username, PasswordHash FROM Users")
	if err != nil {
		log.Fatal(err.Error())
	}
	defer stmt.Close()
	row := stmt.QueryRow()
	var username string
	var password string
	row.Scan(&username, &password)
	if err != nil {
		log.Fatal(err.Error())
	}
	fmt.Printf(username + " " + password)
}