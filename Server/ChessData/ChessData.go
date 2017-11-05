package ChessData

import (
	"time"
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
	GameID int
	Pieces []Piece
}

type CustomImage struct {
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

func RunServer() {
	RegisterRoutes()
}
