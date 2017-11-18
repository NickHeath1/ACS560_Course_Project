using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPTester2
{
    public class Session
    {
        public int SessionID { get; set; }
        public string HostPlayer { get; set; }
        public string GuestPlayer { get; set; }
        public int GamerTimerMinutes { get; set; }
        public int MoveTimerSeconds { get; set; }
    }
}
