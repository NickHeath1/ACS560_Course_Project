using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChessGameAttempt.MoveLogic;

namespace ChessGameAttempt
{
    public enum Signal
    {
        CreateSession = 1,
        MakeAMove = 2,
        GetSessions = 3,
        SendMessage = 4,
        JoinSession = 5,
        DeleteSession = 6,
        StartSession = 7
    }

    public class Move
    {
        public int Player;
        public Piece Source;
        public Coordinates Destination;
        public bool CheckState;
    }

    public class Session
    {
        public int SessionID;
        public string HostPlayer;
        public string GuestPlayer;
        public int GameTimerSeconds;
        public int MoveTimerSeconds;
        public Piece[][] BoardPieces;
        public int CustomGameMode;
        public int GameID;
        public int GuestColor;
    }

    public class TCPSignal
    {
        public Signal SignalType;
        public int SessionID;
        public Move PlayerMove;
        public string HostPlayerName;
        public string GuestPlayerName;
        public Session NewSession;
        public String PlayerMessage;
    }
}
