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
	"strconv"
)

const Datasource string = `sqlserver://ChessGameService:ILikeChicken@(local)/SQLExpress?database=ChessGame&connection+timeout=30`

func RegisterRoutes() {
	router := mux.NewRouter()
	// User data
	router.HandleFunc("/AddUser", AddUser).Methods("POST")
	router.HandleFunc("/AuthenticateUser", AuthenticateUser).Methods("POST")
	router.HandleFunc("/CheckUserAvailability/{username}", CheckUserAvailability).Methods("GET")
	router.HandleFunc("/ChangePassword", ChangePassword).Methods("POST")
	router.HandleFunc("/UpdateGamesWon", UpdateGamesWon).Methods("POST")
	router.HandleFunc("/UpdateGamesLost", UpdateGamesLost).Methods("POST")
	router.HandleFunc("/UpdateGamesDrawn", UpdateGamesDrawn).Methods("POST")

	// Custom Game Data
	router.HandleFunc("/AddCustomGame", AddCustomGame).Methods("POST")
	router.HandleFunc("/GetCustomGamesForUser/{username}", GetCustomGamesForUser).Methods("GET")
	router.HandleFunc("/DeleteCustomGame/{GameID}", DeleteCustomGame).Methods("POST")

	// Custom piece images
	router.HandleFunc("/AddCustomPieceImages", AddCustomPieceImages).Methods("POST")
	router.HandleFunc("/GetCustomPieceImagesForUser/{username}", GetCustomPieceImagesForUser).Methods("GET")
	router.HandleFunc("/DeleteCustomPieceImage/{id}", DeleteCustomPieceImages).Methods("POST")

	// Achievement data
	router.HandleFunc("/GetAllAchievements", GetAllAchievements).Methods("GET")
	router.HandleFunc("/GetAchievementsForUser/{username}", GetAchievementsForUser).Methods("GET")
	router.HandleFunc("/AwardAchievementToUser/{username}/{id}", AwardAchievementToUser).Methods("POST")

	// Custom chessboard data
	router.HandleFunc("/EditCustomChessboard", EditCustomChessboard).Methods("POST")
	router.HandleFunc("/GetCustomChessboardForUser/{username}", GetCustomChessboardForUser).Methods("GET")
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
	params := mux.Vars(r)
	if UserExists(params["username"]) {
		http.Error(w, "User already exists!", 409)
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
	defer conn.Close()
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
	query := "INSERT INTO UserCustomGames VALUES(@Username, @GameTimer, @MoveTimer, @HostMovesFirst, @CustomGameName)"
	result, err := conn.Exec(query, sql.Named("Username", game.Username), sql.Named("GameTimer", game.GameTimer), sql.Named("MoveTimer", game.MoveTimer), sql.Named("HostMovesFirst", game.HostMovesFirst), sql.Named("CustomGameName", game.CustomGameName))
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
	GameID, _ := result.LastInsertId()
	for _, piece := range game.Pieces {
		query = "INSERT INTO UserCustomGamePieces VALUES(@GameID, @Piece, @XCoordinate, @YCoordinate)"
		_, err := conn.Query(query, sql.Named("GameID", GameID), sql.Named("Piece", piece.Name), sql.Named("XCoordinate", piece.XYCoordinates.X), sql.Named("YCoordinate", piece.XYCoordinates.Y))
		if err != nil {
			http.Error(w, "An error occurred on the server!", 500)
			fmt.Println(err.Error())
		}
	}
}

func GetCustomGamesForUser(w http.ResponseWriter, r *http.Request) {
	params := mux.Vars(r)
	conn, err := sql.Open("sqlserver", Datasource)
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
	defer conn.Close()
	query := "SELECT * FROM UserCustomGames WHERE [User] = @Username"
	gameResults, err := conn.Query(query, sql.Named("Username", params["username"]))
	query = "SELECT * FROM UserCustomGamePieces WHERE GameID = @GameID"
	var games []CustomGame

	for gameResults.Next() {
		var currentCustomGame CustomGame
		gameResults.Scan(&currentCustomGame.GameID, &currentCustomGame.Username)
		gamePiecesResults, _ := conn.Query(query, sql.Named("GameID", currentCustomGame.GameID))
		for gamePiecesResults.Next() {
			var piece Piece
			gamePiecesResults.Scan(&piece.Name, &piece.XYCoordinates.X, &piece.XYCoordinates.Y)
			currentCustomGame.Pieces = append(currentCustomGame.Pieces, piece)
		}
		games = append(games, currentCustomGame)
	}
	json.NewEncoder(w).Encode(games)
}

func DeleteCustomGame(w http.ResponseWriter, r *http.Request) {
	params := mux.Vars(r)
	conn, err := sql.Open("sqlserver", Datasource)
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
	defer conn.Close()
	query := "DELETE FROM UserCustomGames WHERE GameID = @GameID"
	GameID, err := strconv.ParseInt(params["GameID"], 10, 32)
	conn.Exec(query, sql.Named("GameID", GameID))
}

// Custom piece images
func AddCustomPieceImages(w http.ResponseWriter, r *http.Request) {
	var customPieceImages []CustomImage
	json.NewDecoder(r.Body).Decode(customPieceImages)
	conn, err := sql.Open("sqlserver", Datasource)
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
	defer conn.Close()
	query := "INSERT INTO UserCustomPieceImages VALUES (@Username, @PieceName, @Image)"
	for _, image := range customPieceImages {
		_, err := conn.Exec(query, sql.Named("Username", image.Username), sql.Named("PieceName", image.PieceName), sql.Named("Image", image.Image))
		if err != nil {
			http.Error(w, "An error occurred on the server!", 500)
			fmt.Println(err.Error())
		}
	}
}

func GetCustomPieceImagesForUser(w http.ResponseWriter, r *http.Request) {
	params := mux.Vars(r)
	conn, err := sql.Open("sqlserver", Datasource)
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
	defer conn.Close()
	var customPieceImages []CustomImage
	query := "SELECT * FROM UserCustomPieceImages WHERE User = @Username"
	results, err := conn.Query(query, sql.Named("Username", params["username"]));
	for results.Next() {
		var customPieceImage CustomImage
		results.Scan(&customPieceImage.ID, &customPieceImage.Username, &customPieceImage.PieceName, &customPieceImage.Image)
		customPieceImages = append(customPieceImages, customPieceImage)
	}
	json.NewEncoder(w).Encode(customPieceImages)
}

func DeleteCustomPieceImages(w http.ResponseWriter, r *http.Request) {
	params := mux.Vars(r)
	conn, err := sql.Open("sqlserver", Datasource)
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
	defer conn.Close()
	query := "DELETE FROM UserCustomPieceImages WHERE ID = @ID"
	conn.Exec(query, sql.Named("ID", params["id"]))
}

// Achievement data
func GetAllAchievements(w http.ResponseWriter, r *http.Request) {
	conn, err := sql.Open("sqlserver", Datasource)
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
	defer conn.Close()
	query := "SELECT * FROM Achievements"
	var achievements []Achievement
	results, err := conn.Query(query)
	for (results.Next()) {
		var achievement Achievement
		results.Scan(&achievement.AchievementID, &achievement.Title, &achievement.Description, &achievement.Difficulty)
		achievements = append(achievements, achievement)
	}
	json.NewEncoder(w).Encode(achievements)
}

func GetAchievementsForUser(w http.ResponseWriter, r *http.Request) {
	conn, err := sql.Open("sqlserver", Datasource)
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
	defer conn.Close()
	params := mux.Vars(r)
	query := "SELECT a.* FROM UserAchievements AS ua JOIN Achievements AS a on ua.AchievementID = a.AchievementID WHERE ua.[User] = @Username"
	results, err := conn.Query(query, sql.Named("Username", params["username"]))
	var achievements []Achievement
	for results.Next() {
		var achievement Achievement
		results.Scan(&achievement.AchievementID, &achievement.Title, &achievement.Description, &achievement.Difficulty)
		achievements = append(achievements, achievement)
	}
	json.NewEncoder(w).Encode(achievements)
}

func AwardAchievementToUser(w http.ResponseWriter, r *http.Request) {
	conn, err := sql.Open("sqlserver", Datasource)
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
	defer conn.Close()
	params := mux.Vars(r)
	query := "INSERT INTO UserAchievements VALUES (@Username, @AchievementID)"
	conn.Exec(query, sql.Named("Username", params["username"]), sql.Named("AchievementID", params["id"]))
}

// Custom chessboard data
func EditCustomChessboard(w http.ResponseWriter, r *http.Request) {
	var customChessBoard CustomChessboard
	json.NewDecoder(r.Body).Decode(customChessBoard)
	conn, err := sql.Open("sqlserver", Datasource)
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
	defer conn.Close()
	query := "SELECT * FROM UserCustomChessboard WHERE User = @Username"
	result, err := conn.Exec(query, sql.Named("Username", customChessBoard.User))
	if rowsAffected, _ := result.RowsAffected(); rowsAffected == 0 {
		query = "INSERT INTO UserCustomChessboard VALUES (@Username, @Color1Red, @Color1Green, @Color1Blue, @Color2Red, @Color2Green, @Color2Blue)"
		conn.Exec(query, sql.Named("Username", customChessBoard.User), sql.Named("Color1Red", customChessBoard.Red1), sql.Named("Color1Green", customChessBoard.Green1), sql.Named("Color1Blue", customChessBoard.Blue1), sql.Named("Color2Red", customChessBoard.Red2), sql.Named("Color2Green", customChessBoard.Green2), sql.Named("Color2Blue", customChessBoard.Blue2))
	} else {
		query = "UPDATE UserCustomChessboard SET Color1Red = @Color1Red, Color1Green = @Color1Green, Color1Blue = @Color1Blue, Color2Red = @Color2Red, Color2Green = @Color2Green, Color2Blue = @Color2Blue"
		conn.Exec(query, sql.Named("Color1Red", customChessBoard.Red1), sql.Named("Color1Green", customChessBoard.Green1), sql.Named("Color1Blue", customChessBoard.Blue1), sql.Named("Color2Red", customChessBoard.Red2), sql.Named("Color2Green", customChessBoard.Green2), sql.Named("Color2Blue", customChessBoard.Blue2))
	}
}

func GetCustomChessboardForUser(w http.ResponseWriter, r *http.Request) {
	conn, err := sql.Open("sqlserver", Datasource)
	if err != nil {
		http.Error(w, "An error occurred on the server!", 500)
		fmt.Println(err.Error())
		return
	}
	defer conn.Close()
	params := mux.Vars(r)
	query := "SELECT * FROM UserCustomChessboard WHERE User = @Username"
	result, err := conn.Query(query, sql.Named("Username", params["username"]))
	var customChessBoard CustomChessboard
	result.Scan(&customChessBoard.User, &customChessBoard.Red1, &customChessBoard.Green1, &customChessBoard.Blue1, &customChessBoard.Red2, &customChessBoard.Green2, &customChessBoard.Blue2)
	json.NewEncoder(w).Encode(customChessBoard)
}

