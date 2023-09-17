using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.src.Networking.NetworkShared
{
    public class Messages
    {
        public class WelcomePacket
        {
            public string PlayerId { get; set; }
            public int PlayerNumber { get; set; }
            public int XStart { get; set; }
            public int YStart { get; set; }
        }
        public class ServerSnapshotPacket
        {
            public short Players { get; set; }
            public int[] X { get; set; }
            public int[] Y { get; set; }

        }

    }
}
