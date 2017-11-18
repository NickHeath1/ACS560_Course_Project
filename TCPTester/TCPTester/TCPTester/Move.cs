using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPTester
{
    public class Move
    {
        public int Player { get; set; }
        public Piece Source { get; set; }
        public Piece Destination { get; set; }
        public bool Checkstate { get; set; }
    }
}
