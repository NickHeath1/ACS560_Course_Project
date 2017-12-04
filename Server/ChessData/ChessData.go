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
}

type CustomGame struct {
	GameID int `json:"GameID,omitempty"`
	Username string `json:"Username,omitempty"`
	GameTimer int `json:"GameTimer",omitempty"`
	MoveTimer int `json:"MoveTimer,omitempty"`
	WhiteMovesFirst bool `json:"WhiteMovesFirst,omitempty"`
	CustomGameName string `json:"CustomGameName,omitempty"`
	Pieces []Piece `json:"Pieces,omitempty"`
}

type AllPieceImages struct {
	WhitePawn CustomImage `json:"WhitePawn,omitempty"`
	BlackPawn CustomImage `json:"BlackPawn,omitempty"`
	WhiteKnight CustomImage `json:"WhiteKnight,omitempty"`
	BlackKnight CustomImage `json:"BlackKnight,omitempty"`
	WhiteBishop CustomImage `json:"WhiteBishop,omitempty"`
	BlackBishop CustomImage `json:"BlackBishop,omitempty"`
	WhiteRook CustomImage `json:"WhiteRook,omitempty"`
	BlackRook CustomImage `json:"BlackRook,omitempty"`
	WhiteQueen CustomImage `json:"WhiteQueen,omitempty"`
	BlackQueen CustomImage `json:"BlackQueen,omitempty"`
	WhiteKing CustomImage `json:"WhiteKing,omitempty"`
	BlackKing CustomImage `json:"BlackKing,omitempty"`
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

type Move struct {
	Player int `json:"Player,omitempty"`
	Source Piece `json:"Source,omitempty"`
	Destination Coordinate `json:"Destination,omitempty"`
	CheckState bool `json:"Checkstate,omitempty"`
}

type CustomChessboard struct {
	User string `json:"Username,omitempty"`
	Red1 int `json:"Red1,omitempty"`
	Green1 int `json:"Green1,omitempty"`
	Blue1 int `json:Blue1,omitempty"`
	Red2 int `json:"Red2,omitempty"`
	Green2 int `json:"Green2,omitempty"`
	Blue2 int `json:"Blue2,omitempty"`
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
	statistic := 0
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
	if result.Next() {
		result.Scan(&statistic)
	}
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
