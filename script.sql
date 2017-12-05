USE [master]
GO
/****** Object:  Database [ChessGame]    Script Date: 12/4/2017 9:38:45 PM ******/
CREATE DATABASE [ChessGame]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ChessGame', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\ChessGame.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ChessGame_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\ChessGame_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ChessGame] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ChessGame].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ChessGame] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ChessGame] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ChessGame] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ChessGame] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ChessGame] SET ARITHABORT OFF 
GO
ALTER DATABASE [ChessGame] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ChessGame] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ChessGame] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ChessGame] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ChessGame] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ChessGame] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ChessGame] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ChessGame] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ChessGame] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ChessGame] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ChessGame] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ChessGame] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ChessGame] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ChessGame] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ChessGame] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ChessGame] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ChessGame] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ChessGame] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ChessGame] SET  MULTI_USER 
GO
ALTER DATABASE [ChessGame] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ChessGame] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ChessGame] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ChessGame] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ChessGame] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ChessGame] SET QUERY_STORE = OFF
GO
USE [ChessGame]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [ChessGame]
GO
/****** Object:  User [ChessGameService]    Script Date: 12/4/2017 9:38:45 PM ******/
CREATE USER [ChessGameService] FOR LOGIN [ChessGameService] WITH DEFAULT_SCHEMA=[db_datareader]
GO
ALTER ROLE [db_datareader] ADD MEMBER [ChessGameService]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [ChessGameService]
GO
/****** Object:  Table [dbo].[AchievementDifficultyLevels]    Script Date: 12/4/2017 9:38:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AchievementDifficultyLevels](
	[Difficulty] [varchar](20) NOT NULL,
 CONSTRAINT [PK_AchievementDifficultyLevels] PRIMARY KEY CLUSTERED 
(
	[Difficulty] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Achievements]    Script Date: 12/4/2017 9:38:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Achievements](
	[AchievementID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](max) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Difficulty] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Achievements] PRIMARY KEY CLUSTERED 
(
	[AchievementID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pieces]    Script Date: 12/4/2017 9:38:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pieces](
	[Piece] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Pieces] PRIMARY KEY CLUSTERED 
(
	[Piece] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAchievements]    Script Date: 12/4/2017 9:38:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAchievements](
	[User] [varchar](20) NOT NULL,
	[AchievementID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCustomChessboard]    Script Date: 12/4/2017 9:38:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCustomChessboard](
	[User] [varchar](20) NOT NULL,
	[Color1Red] [int] NOT NULL,
	[Color1Green] [int] NOT NULL,
	[Color1Blue] [int] NOT NULL,
	[Color2Red] [int] NOT NULL,
	[Color2Green] [int] NOT NULL,
	[Color2Blue] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCustomGamePieces]    Script Date: 12/4/2017 9:38:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCustomGamePieces](
	[GameID] [int] NOT NULL,
	[Piece] [varchar](20) NOT NULL,
	[XCoordinate] [int] NOT NULL,
	[YCoordinate] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCustomGames]    Script Date: 12/4/2017 9:38:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCustomGames](
	[GameID] [int] IDENTITY(1,1) NOT NULL,
	[User] [varchar](20) NOT NULL,
	[GameTimer] [int] NULL,
	[MoveTimer] [int] NULL,
	[HostMovesFirst] [bit] NULL,
	[CustomGameName] [varchar](50) NULL,
 CONSTRAINT [PK_UserCustomGames] PRIMARY KEY CLUSTERED 
(
	[GameID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCustomPieceImages]    Script Date: 12/4/2017 9:38:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCustomPieceImages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User] [varchar](20) NOT NULL,
	[Piece] [varchar](20) NOT NULL,
	[Image] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_UserCustomPieceImages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/4/2017 9:38:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Username] [varchar](20) NOT NULL,
	[PasswordHash] [varchar](max) NOT NULL,
	[PasswordSalt] [varchar](max) NOT NULL,
	[JoinDate] [datetime] NOT NULL,
	[GamesWon] [int] NOT NULL,
	[GamesLost] [int] NOT NULL,
	[GamesDrawn] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_GamesWon]  DEFAULT ((0)) FOR [GamesWon]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_GamesLost]  DEFAULT ((0)) FOR [GamesLost]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_GamesDrawn]  DEFAULT ((0)) FOR [GamesDrawn]
GO
ALTER TABLE [dbo].[Achievements]  WITH CHECK ADD  CONSTRAINT [FK_Achievements_AchievementDifficultyLevels] FOREIGN KEY([Difficulty])
REFERENCES [dbo].[AchievementDifficultyLevels] ([Difficulty])
GO
ALTER TABLE [dbo].[Achievements] CHECK CONSTRAINT [FK_Achievements_AchievementDifficultyLevels]
GO
ALTER TABLE [dbo].[UserAchievements]  WITH CHECK ADD  CONSTRAINT [FK_UserAchievements_Achievements] FOREIGN KEY([AchievementID])
REFERENCES [dbo].[Achievements] ([AchievementID])
GO
ALTER TABLE [dbo].[UserAchievements] CHECK CONSTRAINT [FK_UserAchievements_Achievements]
GO
ALTER TABLE [dbo].[UserAchievements]  WITH CHECK ADD  CONSTRAINT [FK_UserAchievements_Users] FOREIGN KEY([User])
REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[UserAchievements] CHECK CONSTRAINT [FK_UserAchievements_Users]
GO
ALTER TABLE [dbo].[UserCustomChessboard]  WITH CHECK ADD  CONSTRAINT [FK_UserCustomChessboard_Users] FOREIGN KEY([User])
REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[UserCustomChessboard] CHECK CONSTRAINT [FK_UserCustomChessboard_Users]
GO
ALTER TABLE [dbo].[UserCustomGamePieces]  WITH CHECK ADD  CONSTRAINT [FK_UserCustomGamePieces_Pieces] FOREIGN KEY([Piece])
REFERENCES [dbo].[Pieces] ([Piece])
GO
ALTER TABLE [dbo].[UserCustomGamePieces] CHECK CONSTRAINT [FK_UserCustomGamePieces_Pieces]
GO
ALTER TABLE [dbo].[UserCustomGamePieces]  WITH CHECK ADD  CONSTRAINT [FK_UserCustomGamePieces_UserCustomGames] FOREIGN KEY([GameID])
REFERENCES [dbo].[UserCustomGames] ([GameID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserCustomGamePieces] CHECK CONSTRAINT [FK_UserCustomGamePieces_UserCustomGames]
GO
ALTER TABLE [dbo].[UserCustomGames]  WITH CHECK ADD  CONSTRAINT [FK_UserCustomGames_Users] FOREIGN KEY([User])
REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[UserCustomGames] CHECK CONSTRAINT [FK_UserCustomGames_Users]
GO
ALTER TABLE [dbo].[UserCustomPieceImages]  WITH CHECK ADD  CONSTRAINT [FK_UserCustomPieceImages_Pieces] FOREIGN KEY([Piece])
REFERENCES [dbo].[Pieces] ([Piece])
GO
ALTER TABLE [dbo].[UserCustomPieceImages] CHECK CONSTRAINT [FK_UserCustomPieceImages_Pieces]
GO
ALTER TABLE [dbo].[UserCustomPieceImages]  WITH CHECK ADD  CONSTRAINT [FK_UserCustomPieceImages_Users] FOREIGN KEY([User])
REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[UserCustomPieceImages] CHECK CONSTRAINT [FK_UserCustomPieceImages_Users]
GO
USE [master]
GO
ALTER DATABASE [ChessGame] SET  READ_WRITE 
GO
