using LiteNetLib;
using System.Collections.Generic;

namespace Battleship.src.Networking.NetworkShared
{
    class ServerPlayerData
    {
        public double MillisecondsSinceLastHeard { get; set; }
        public System.Net.IPAddress IPAddress { get; set; }
        public int Port { get; set; }
        public string ClientID { get; set; }   // proxy for player
        public string GameSessionId { get; set; } //what specific game we're in
        public NetPeer _clientRef { get; set; }  //determine if we need this..
        public Loc PlayerLocation { get; set; }
        public int PlayerNumber { get; set; } //server assigns them as player 1-4 according to order of connection        

    }
    class Loc
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class GameSession
    {
        // unique key used to lookup this particular game
        string GameID { get; set; }
        // list of all players in the game
        public List<PlayerData> Players { get; set; }
        // used to purge this game from the list of active games
        bool InProgress { get; set; }

    }

}
