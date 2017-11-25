using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChessGameAttempt.GameSession;

namespace ChessGameAttempt
{
    public enum Signal
    {
        CreateSession = 1,
        MakeAMove = 2,
        GetSessions = 3,
        SendMessage = 4,
        JoinSession = 5
    }

    class Move
    {
        int Player;
        Piece Source;
        Piece Destination;
        bool CheckState;
    }

    class Session
    {
        public int SessionID;
        public string HostPlayer;
        public string GuestPlayer;
        public int GameTimerSeconds;
        public int MoveTimerSeconds;
        public Piece[][] BoardPieces;
        public int CustomGameMode;
    }

    class TCPSignal
    {
        public Signal SignalType;
        public int SessionID;
        public Move PlayerMove;
        public Session NewSession;
        public String PlayerMessage;
    }
}
