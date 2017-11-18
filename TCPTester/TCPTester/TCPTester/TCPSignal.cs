using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPTester
{
    public class TCPSignal
    {
        public int SignalType { get; set; }
	    public int SessionID { get; set; }
	    //PlayerMove ChessData.Move
        public Session NewSession { get; set; }
        public string PlayerMessage { get; set; }
    }
}
