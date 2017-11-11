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
	Username string `json:"Username,omitempty"`
	GameID int `json:"GameID,omitempty"`
	Pieces []Piece `json:"Pieces,omitempty"`
}

type CustomImage struct {
	Username string `json:"Username,omitempty"`
	ID int `json:"ID,omitempty"`
	PieceName string `json:"PieceName,omitempty"`
	Image []byte `json:"Image,omitempty"`
}

type Piece struct {
	Name string `json:"Name,omitempty"`
	XYCoordinates Coordinate `json:"Coordinates,omitempty"`
}

type Achievement struct {
	AchievementID int `json:"AchievementID,omitempty"`
	Title string `json:"Title,omitempty"`
	Description string `json:"Description,omitempty"`
	Difficulty string `json:"Difficulty,omitempty"`
}

type Color struct {
	Red int
	Green int
	Blue int
}

type Move struct {
	SessionID int `json:"SessionID,omitempty"`
	Player int `json:"Player,omitempty"`
	Source Piece `json:"Source,omitempty"`
	Destination Piece `json:"Destination,omitempty"`
	Checkstate bool `json:"Checkstate,omitempty"`
}

type Coordinate struct {
	X int `json:"X,omitempty"`
	Y int `json:"Y,omitempty"`
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
