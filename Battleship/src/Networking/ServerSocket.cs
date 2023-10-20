
using Battleship.src.Networking;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Drawing.Printing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace Battleship.src.Networking
{


    public class Ship
    {

        public int[] p { get; set; }
        public int[] d { get; set; }
        public int[] s { get; set; }
    }


    public class myData
    {
        public string action { get; set; }
        public int bot { get; set; }
        public Ship ships { get; set; }
        public int[] position { get; set; }
        public string reserva1 { get; set; }
        public string reserva2 { get; set; }
    }


    public class ServerSocket
    {
        private UdpClient udpServer;
        private int serverPort;
        private Thread serverThread; // Hilo para el servidor
        private GameDataJSON GameDataJSON;
        GameControllers GameControllers;


        public ServerGameplay ServerGameplay;

        public bool canSendS = false;

        public ServerSocket(GameDataJSON GameDataJSON, GameControllers GameControllers)
        {
            this.GameDataJSON = GameDataJSON;
            this.GameControllers = GameControllers;
            ServerGameplay = new ServerGameplay();

        }

        public void Start(string IP, int PORT)
        {

            IPAddress ipAddress = IPAddress.Parse(IP);
            serverPort = PORT;

            //Stablish Connection
            udpServer = new UdpClient(PORT);
            serverThread = new Thread(ListenForClients);
            serverThread.Start();
        }


        public void Stop()
        {
            udpServer.Close();
            serverThread.Join();
            Console.WriteLine("Servidor UDP ha cerrado su sesion");

        }

        private void ListenForClients()
        {
            Console.WriteLine("Servidor UDP iniciado en el puerto {0}", serverPort);

            try
            {
                while (true)
                {
                    IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receivedBytes = udpServer.Receive(ref clientEndPoint);
                    string receivedMessage = Encoding.UTF8.GetString(receivedBytes);
                    myData receivedStringData = JsonConvert.DeserializeObject<myData>(receivedMessage);

                    Console.WriteLine("Mensaje recibido del cliente {0}:{1}: {2}", clientEndPoint.Address, clientEndPoint.Port, receivedMessage);


                    if(receivedStringData.action == "c")
                    {
                        string msg = GameDataJSON.ServerJSON("c", 1);
                        SendResponse(msg, clientEndPoint);

                        ServerGameplay.Players.Add(clientEndPoint);
                    }

                    //BOT
                    if(receivedStringData.action == "s")
                    {

                        //contra bots
                        if(receivedStringData.bot == 1)
                        {
                            ServerGameplay.SinglePlayer = true;
                            string msg = GameDataJSON.ServerJSON("s", 1);
                            SendResponse(msg, ServerGameplay.Players[0]);


                            string buildRequest = GameDataJSON.ServerJSON("b", 1);
                            SendResponse(buildRequest, ServerGameplay.Players[0]);

                        }
                        if (receivedStringData.bot == 0 && canSendS)
                        {

                            string sRequestP1 = GameDataJSON.ServerJSON("s", 1);
                            SendResponse(sRequestP1, ServerGameplay.Players[0]);

                            string sRequestP2 = GameDataJSON.ServerJSON("s", 1);
                            SendResponse(sRequestP2, ServerGameplay.Players[1]);
                        }
                        if (receivedStringData.bot == 0 && !canSendS)
                        {
                            ServerGameplay.SinglePlayer = false;
                            canSendS = true;
                        }
                 
                    }


                    if (receivedStringData.action == "b" &&
                        (ServerGameplay.Players[0].ToString() == clientEndPoint.ToString()))
                    {
                        Console.WriteLine("MENSAJE RECIBIDO");

                        var p = receivedStringData.ships.p;
                        var d = receivedStringData.ships.d;
                        var s = receivedStringData.ships.s;

                        Console.WriteLine(p[0] + " " + d[0] + " " + s[0]);



                        ServerGameplay.addShips(p, d, s, ServerGameplay.playerOneMatrix);

                        string msg = GameDataJSON.ServerJSON("b", 1);
                        SendResponse(msg, clientEndPoint);


                        if (ServerGameplay.SinglePlayer)
                        {
                            // Solo singlePlayer
                            ServerGameplay.startBotGameplay();

                            string turnoP1 = GameDataJSON.ServerJSON("t", 1);
                            SendResponse(turnoP1, ServerGameplay.Players[0]);

                        }
                        else if (!ServerGameplay.SinglePlayer)
                        {
                            string buildRequest = GameDataJSON.ServerJSON("b", 1);
                            SendResponse(buildRequest, ServerGameplay.Players[1]);
                        }
                    }
                  


                    if (!ServerGameplay.SinglePlayer)  // Solo si es multiplayer
                    {
                        if (receivedStringData.action == "b" &&
                            ServerGameplay.Players[1].ToString() == clientEndPoint.ToString())
                        {
                            var p = receivedStringData.ships.p;
                            var d = receivedStringData.ships.d;
                            var s = receivedStringData.ships.s;

                            ServerGameplay.addShips(p, d, s, ServerGameplay.playerTwoMatrix);

                            string msg = GameDataJSON.ServerJSON("b", 1);
                            SendResponse(msg, clientEndPoint);


                            string turnoP1 = GameDataJSON.ServerJSON("t", 1);
                            SendResponse(turnoP1, ServerGameplay.Players[0]);

                            string turnoP2 = GameDataJSON.ServerJSON("t", 0);
                            SendResponse(turnoP2, ServerGameplay.Players[1]);

                        }
                    }

                    if (receivedStringData.action == "a") {

                        Console.WriteLine("P1 Ships: " + ServerGameplay.PlayerOneCountShips);
                        Console.WriteLine("P2 Ships: " + ServerGameplay.PlayerOneCountShips);

                        if (ServerGameplay.SinglePlayer)
                        {
                            // Ataque jugador
                            var attackValue = ServerGameplay.attackPosition(receivedStringData.position, clientEndPoint);

                            var pos = new Vector2(receivedStringData.position[0], receivedStringData.position[1]);
                            string buildRequest = GameDataJSON.ServerJSON("a", attackValue, pos);
                            SendResponse(buildRequest, clientEndPoint);

                            string turnJSONP1 = GameDataJSON.ServerJSON("t", 0);
                            SendResponse(turnJSONP1, clientEndPoint);

                            // Ataque IA
                            var IABomb = ServerGameplay.GenerateBombIA();
                            var position = new Vector2(IABomb.X, IABomb.Y);

                            string bombIAJSON = GameDataJSON.ServerJSON("a",(int)IABomb.Z, position);
                            SendResponse(bombIAJSON, clientEndPoint);

                            string turnJSON = GameDataJSON.ServerJSON("t", 1);
                            SendResponse(turnJSON, clientEndPoint);
                        }
                        if (!ServerGameplay.SinglePlayer)
                        {
                            var attackValue = ServerGameplay.attackPosition(receivedStringData.position, clientEndPoint);
                            if (ServerGameplay.Players[0].ToString() == clientEndPoint.ToString())
                            {
                                var pos = new Vector2(receivedStringData.position[0], receivedStringData.position[1]);
                                string posP1JSON = GameDataJSON.ServerJSON("a", attackValue, pos);
                                SendResponse(posP1JSON, clientEndPoint);


                                var atkValP2 = ServerGameplay.attackPosition(receivedStringData.position,
                                                                              ServerGameplay.Players[1]);

                                string posP2JSON = GameDataJSON.ServerJSON("a", atkValP2, pos);
                                SendResponse(posP2JSON, ServerGameplay.Players[1]);

                                // Turnos
                                string turnJSON = GameDataJSON.ServerJSON("t", 0);
                                SendResponse(turnJSON, ServerGameplay.Players[0]);

                                string turnJSON2 = GameDataJSON.ServerJSON("t", 1);
                                SendResponse(turnJSON2, ServerGameplay.Players[1]);
                            }else if(ServerGameplay.Players[1].ToString() == clientEndPoint.ToString())
                            {
                                var pos = new Vector2(receivedStringData.position[0], receivedStringData.position[1]);
                                string posP1JSON = GameDataJSON.ServerJSON("a", attackValue, pos);
                                SendResponse(posP1JSON, clientEndPoint);

                                var atkValP2 = ServerGameplay.attackPosition(receivedStringData.position,
                                                  ServerGameplay.Players[0]);
                                string posP2JSON = GameDataJSON.ServerJSON("a", atkValP2, pos);
                                SendResponse(posP2JSON, ServerGameplay.Players[0]);

                                // Turnos
                                string turnJSON = GameDataJSON.ServerJSON("t", 0);
                                SendResponse(turnJSON, ServerGameplay.Players[1]);

                                string turnJSON2 = GameDataJSON.ServerJSON("t", 1);
                                SendResponse(turnJSON2, ServerGameplay.Players[0]);
                            }

                        }
                        Console.WriteLine(ServerGameplay.PlayerOneCountShips);
                        Console.WriteLine(ServerGameplay.PlayerTwoCountShips);
                    }

                    if(receivedStringData.action == "d")
                    {
                        
                        string turnJSON2 = GameDataJSON.ServerJSON("d", 1);
                        SendResponse(turnJSON2, clientEndPoint);

                        Console.WriteLine("[ Server ] IA WIN");


                        //Si el jugador se desconecta manda win al otro
                        if(!ServerGameplay.SinglePlayer)
                        {
                            if (clientEndPoint.ToString() == ServerGameplay.Players[0].ToString())
                            {
                                string win = GameDataJSON.ServerJSON("w", 1);
                                SendResponse(turnJSON2, ServerGameplay.Players[1]);
                            }else if(clientEndPoint.ToString() == ServerGameplay.Players[1].ToString())
                            {
                                string win = GameDataJSON.ServerJSON("w", 1);
                                SendResponse(turnJSON2, ServerGameplay.Players[0]);
                            }
                        }

                        // Reiniciar el servidor
                        ServerGameplay = new ServerGameplay();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SendResponse(string response, IPEndPoint clientEndPoint)
        {
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            udpServer.Send(responseBytes, responseBytes.Length, clientEndPoint);
            Console.WriteLine("Respuesta enviada al cliente {0}:{1}: {2}", clientEndPoint.Address, clientEndPoint.Port, response);
        }

        public bool canWin = true;
        public void Update()
        {
            if(canWin && ServerGameplay.PlayerOneCountShips == 0)
            {
                string win = GameDataJSON.ServerJSON("w", 1);
                SendResponse(win, ServerGameplay.Players[1]);

                string win2 = GameDataJSON.ServerJSON("w", 0);
                SendResponse(win2, ServerGameplay.Players[0]);

                canWin = false;
            }

            if (canWin && ServerGameplay.PlayerTwoCountShips == 0)
            {
                string win = GameDataJSON.ServerJSON("w", 0);
                SendResponse(win, ServerGameplay.Players[1]);

                string win2 = GameDataJSON.ServerJSON("w", 1);
                SendResponse(win2, ServerGameplay.Players[0]);

                canWin = false;
            }


        }
    }

}