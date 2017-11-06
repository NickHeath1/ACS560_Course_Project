package ChessData

import (
	"time"
	"database/sql"
	"golang.org/x/crypto/bcrypt"
)

type User struct {
	Username string `json:"username,omitempty"`
	PasswordHash string `json:"Password,omitempty"`
	PasswordSalt string `json:"PasswordSalt,omitempty"`
	JoinDate time.Time `json:"joinDate,omitempty"`
	GamesWon int `json:"GamesWon,omitempty"`
	GamesLost int `json:"GamesLost,omitempty"`
	GamesDrawn int `json:"GamesDrawn,omitempty"`
	CustomGames []CustomGame
	Achievements []Achievement
	ChessboardColor1 Color
	ChessboardColor2 Color
}

type CustomGame struct {
	Username string
	GameID int
	Pieces []Piece
}

type CustomImage struct {
	Username string
	ID int
	PieceName string
	Image []byte
}

type Piece struct {
	Name string
	XCoordinate int
	YCoordinate int
}

type Achievement struct {
	AchievementID int
	Title string
	Description string
	Difficulty string
}

type Color struct {
	Red int
	Green int
	Blue int
}

func UserExists(username string) bool {
	conn, err := sql.Open("sqlserver", Datasource)
	if err != nil {
		panic(err.Error())
	}
	defer conn.Close()
	query := "SELECT * FROM USERS WHERE Username = @Username"
	result, err := conn.Exec(query, sql.Named("Username", username))
	if err != nil {
		panic(err.Error())
	}

	if rowsAffected, _ := result.RowsAffected(); rowsAffected > 0 {
		return true
	}
	return false
}

func CheckPassword(password, salt, hash string) bool {
	if err := bcrypt.CompareHashAndPassword([]byte(hash), []byte(password + salt)); err != nil {
		return false
	}
	return true
}

func GetGameStat(StatType string, user User) int {
	var statistic int
	conn, _ := sql.Open("sqlserver", Datasource)
	defer conn.Close()
	query := ""
	if StatType == "Won" {
		query = "SELECT GamesWon FROM Users WHERE Username = @Username"

	} else if StatType == "Lost" {
		query = "SELECT GamesLost FROM Users WHERE Username = @Username"
	} else if StatType == "Drawn" {
		query = "SELECT GamesDrawn FROM Users WHERE Username = @Username"
	}
	result, _ := conn.Query(query, sql.Named("Username", user.Username))
	result.Scan(&statistic)
	return statistic
}

func UpdateGameStat(StatType string, user User, statistic int) {
	conn, _ := sql.Open("sqlserver", Datasource)
	defer conn.Close()
	query := ""
	if StatType == "Won" {
		query = "UPDATE USERS SET GamesWon = @statistic WHERE Username = @Username"
	} else if StatType == "Lost" {
		query = "UPDATE USERS SET GamesLost = @statistic WHERE Username = @Username"
	} else if StatType == "Drawn" {
		query = "UPDATE USERS SET GamesDrawn = @statistic WHERE Username = @Username"
	}
	conn.Exec(query, sql.Named("Username", user.Username), sql.Named ("statistic", statistic))
}

func RunServer() {
	RegisterRoutes()
}
