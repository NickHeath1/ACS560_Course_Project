package ChessData

import (
	"net/http"
	"github.com/gorilla/mux"
	"log"
	"golang.org/x/crypto/bcrypt"
	"encoding/json"
	"database/sql"
	_ "github.com/denisenkom/go-mssqldb"
	"time"
	"fmt"
)

const Datasource string = `sqlserver://ChessGameService:ILikeChicken@(local)/SQLExpress?database=ChessGame&connection+timeout=30`

func RegisterRoutes() {
	router := mux.NewRouter()
	// User data
	router.HandleFunc("/AddUser", AddUser).Methods("POST")
	router.HandleFunc("/AuthenticateUser", AuthenticateUser).Methods("POST")
	router.HandleFunc("/CheckUserAvailability", CheckUserAvailability).Methods("POST")
	router.HandleFunc("/ChangePassword", ChangePassword).Methods("POST")
	router.HandleFunc("/UpdateGamesWon", UpdateGamesWon).Methods("POST")
	router.HandleFunc("/UpdateGamesLost", UpdateGamesLost).Methods("POST")
	router.HandleFunc("/UpdateGamesDrawn", UpdateGamesDrawn).Methods("POST")

	// Custom Game Data
	router.HandleFunc("/AddCustomGame", AddCustomGame).Methods("POST")
	router.HandleFunc("/GetCustomGamesForUser", GetCustomGamesForUser).Methods("GET")
	router.HandleFunc("/DeleteCustomGame", DeleteCustomGame).Methods("POST")

	// Custom piece images
	router.HandleFunc("/AddCustomPieceImages", AddCustomPieceImages).Methods("POST")
	router.HandleFunc("/GetCustomPieceImagesForUser", GetCustomPieceImagesForUser).Methods("GET")
	router.HandleFunc("/DeleteCustomPieceImages", DeleteCustomPieceImages).Methods("POST")

	// Achievement data
	router.HandleFunc("/GetAllAchievements", GetAllAchievements).Methods("GET")
	router.HandleFunc("/GetAchievementsForUser", GetAchievementsForUser).Methods("GET")
	router.HandleFunc("/AwardAchievementToUser", AwardAchievementToUser).Methods("POST")

	// Custom chessboard data
	router.HandleFunc("/AddCustomChessboard", AddCustomChessboard).Methods("POST")
	router.HandleFunc("/GetCustomChessboardForUser", GetCustomChessboardForUser).Methods("GET")
	router.HandleFunc("/DeleteCustomChessboard", DeleteCustomChessboard).Methods("POST")
	log.Fatal(http.ListenAndServe(":2345", router))
}

// User data
func AddUser(w http.ResponseWriter, r *http.Request) {
	var user User
	json.NewDecoder(r.Body).Decode(&user)
	if UserExists(user.Username) {
		http.Error(w, "User already exists!", 400)
		return
	}
	salt, err := bcrypt.GenerateFromPassword([]byte(user.PasswordHash), bcrypt.DefaultCost)
	hashedPassword, err := bcrypt.GenerateFromPassword([]byte(user.PasswordHash + string(salt)), bcrypt.DefaultCost)
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
	user.PasswordHash = string(hashedPassword)
	user.PasswordSalt = string(salt)
	conn, err := sql.Open("sqlserver", Datasource)
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
	defer conn.Close()
	query := "INSERT INTO USERS (Username, PasswordHash, PasswordSalt, JoinDate) Values(@Username, @PasswordHash, @PasswordSalt, @JoinDate)"
	_, err = conn.Query(query, sql.Named("Username", user.Username), sql.Named("PasswordHash", user.PasswordHash), sql.Named("PasswordSalt", user.PasswordSalt), sql.Named("JoinDate",time.Now().Format("02-Jan-2006 15:04:05")))
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
}

func CheckUserAvailability(w http.ResponseWriter, r *http.Request) {
	var username string
	json.NewDecoder(r.Body).Decode(&username)
	if UserExists(username) {
		http.Error(w, "User already exists!", 400)
	}
}

func AuthenticateUser(w http.ResponseWriter, r *http.Request) {
	var user User
	json.NewDecoder(r.Body).Decode(&user)
	conn, err := sql.Open("sqlserver", Datasource)
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
	result, err := conn.Query("SELECT PasswordHash, PasswordSalt FROM Users WHERE Username = @Username", sql.Named("Username", user.Username))
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
	if !result.Next() {
		http.Error(w, "Incorrect username or password!", 400)
		return
	}
	var PasswordHash string
	var PasswordSalt string
	result.Scan(&PasswordHash, &PasswordSalt)
	if !CheckPassword(user.PasswordHash, PasswordSalt, PasswordHash) {
		http.Error(w, "Incorrect username or password!", 400)
		return
	}
}

func ChangePassword(w http.ResponseWriter, r *http.Request) {
	var user User
	json.NewDecoder(r.Body).Decode(&user)
	salt, err := bcrypt.GenerateFromPassword([]byte(user.PasswordHash), bcrypt.DefaultCost)
	hashedPassword, err := bcrypt.GenerateFromPassword([]byte(user.PasswordHash + string(salt)), bcrypt.DefaultCost)
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
	user.PasswordHash = string(hashedPassword)
	user.PasswordSalt = string(salt)
	conn, err := sql.Open("sqlserver", Datasource)
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
	defer conn.Close()
	query := "UPDATE USERS SET PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt WHERE Username = @Username"
	_, err = conn.Query(query, sql.Named("Username", user.Username), sql.Named("PasswordHash", user.PasswordHash), sql.Named("PasswordSalt", user.PasswordSalt))
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
}

func UpdateGamesWon(w http.ResponseWriter, r *http.Request) {
	var user User
	json.NewDecoder(r.Body).Decode(&user)
	statistic := GetGameStat("Won", user)
	UpdateGameStat("Won", user, statistic + 1)
}

func UpdateGamesLost(w http.ResponseWriter, r *http.Request) {
	var user User
	json.NewDecoder(r.Body).Decode(&user)
	statistic := GetGameStat("Lost", user)
	UpdateGameStat("Lost", user, statistic + 1)
}

func UpdateGamesDrawn(w http.ResponseWriter, r *http.Request) {
	var user User
	json.NewDecoder(r.Body).Decode(&user)
	statistic := GetGameStat("Drawn", user)
	UpdateGameStat("Drawn", user, statistic + 1)
}

// Custom game data
func AddCustomGame(w http.ResponseWriter, r *http.Request) {
	var game CustomGame
	json.NewDecoder(r.Body).Decode(&game)
	conn, err := sql.Open("sqlserver", Datasource)
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
	defer conn.Close()
	query := "INSERT INTO UserCustomGames VALUES(@Username)"
	result, err := conn.Exec(query, sql.Named("Username", game.Username))
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
	GameID, _ := result.LastInsertId()
	for _, piece := range game.Pieces {
		query = "INSERT INTO UserCustomGamePieces VALUES(@GameID, @Piece, @XCoordinate, @YCoordinate)"
		_, err := conn.Query(query, sql.Named("GameID", GameID), sql.Named("Piece", piece.Name), sql.Named("XCoordinate", piece.XCoordinate), sql.Named("YCoordinate", piece.YCoordinate))
		if err != nil {
			http.Error(w, "An error occurred on the server!", 500)
			fmt.Println(err.Error())
		}
	}
}

func GetCustomGamesForUser(w http.ResponseWriter, r *http.Request) {

}

func DeleteCustomGame(w http.ResponseWriter, r *http.Request) {

}

// Custom piece images
func AddCustomPieceImages(w http.ResponseWriter, r *http.Request) {

}

func GetCustomPieceImagesForUser(w http.ResponseWriter, r *http.Request) {

}

func DeleteCustomPieceImages(w http.ResponseWriter, r *http.Request) {

}

// Achievement data
func GetAllAchievements(w http.ResponseWriter, r *http.Request) {

}

func GetAchievementsForUser(w http.ResponseWriter, r *http.Request) {

}

func AwardAchievementToUser(w http.ResponseWriter, r *http.Request) {

}

// Custom chessboard data
func AddCustomChessboard(w http.ResponseWriter, r *http.Request) {

}

func GetCustomChessboardForUser(w http.ResponseWriter, r *http.Request) {

}

func DeleteCustomChessboard(w http.ResponseWriter, r *http.Request) {

}

