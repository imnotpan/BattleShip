using System;
using LiteNetLib;
using System.Text;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Linq;
using Battleship.src.Controllers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Battleship.src.Networking
{
    public class Ship
    {

        public int[] p { get; set; }
        public int[] b { get; set; }
        public int[] s { get; set; }
    }


    public class myData
    {
        public int gameID { get; set; }
        public string action { get; set; }
        public int bot { get; set; }
        public Ship ships { get; set; }
        public int[] position { get; set; }

    }

    public class Server
    {
        public ServerListener ServerListener;    
        public NetManager server;

        public bool isClientOneReady;
        public bool isClientTwoReady;

        public bool isShipPlayerOneRedy;
        public bool isShipPlayerTwoRedy;

        public NetPeer PeerOne;
        public NetPeer PeerTwo;


        public bool isPlayerOneAttackReady;
        public bool isPlayerTwoAttackReady;
        public Vector2 attackPlayerOne;
        public Vector2 attackPlayerTwo;

        public string TempPeerOne;
        public string TempPeerTwo;

        GameControllers GameControllers;

        // Multiple Sessions


        public Server(GameControllers GameControllers)
        {
            this.GameControllers = GameControllers;
            
        }

        public void ServerStart(int PORT)
        {
            ServerListener = new ServerListener();
            var listener = new EventBasedNetListener();
            server = new NetManager(listener);
            server.Start(PORT); // -> Set Port
            GameControllers.GameStatesSystem.SIDE = "CLIENT";


            listener.ConnectionRequestEvent += request =>
            {

                if(server.ConnectedPeersCount <= 2)
                {
                    request.Accept();
                    if(server.ConnectedPeersCount == 2)
                    {
                        /*
                        Console.WriteLine("[ SERVER ]MAX PLAYER REACHED, GAME STARTING");
                        var JSON = GameControllers.GameDataJSON.ClientJSON("c", 1);
                        foreach(var peer in server.ConnectedPeerList)
                        {
                            SendDataToClient(peer, JSON);

                        }
                        */

                    }
                }
                else
                {
                    request.Reject();

                }
            };

            listener.PeerConnectedEvent += peer =>
            {
                if (PeerOne == null)
                {
                    PeerOne = peer;
                    Console.WriteLine("PeerOne connected: {0}", peer.EndPoint);
                }
                // Si ya tienes a PeerOne almacenado, almacena a PeerTwo
                else if (PeerTwo == null)
                {
                    PeerTwo = peer;
                    Console.WriteLine("PeerTwo connected: {0}", peer.EndPoint);
                }
                var TextComponent = GameControllers.MainMenuController.ClientStateText._textComponent;
                TextComponent.Text = TextComponent.Text + "\n" + peer.EndPoint;
            };

            listener.PeerDisconnectedEvent += (peer, disconnectInfo) =>
            {
                Console.WriteLine("We got connection: {0}", peer.EndPoint);
                var TextComponent = GameControllers.MainMenuController.ClientStateText._textComponent;
                TextComponent.Text = "User Connected: ";
            };

            listener.NetworkReceiveEvent += ( peer, dataReader, channel, delivery) =>
            {
                try
                {
                    byte[] receivedBytes = new byte[dataReader.AvailableBytes];
                    dataReader.GetBytes(receivedBytes, dataReader.AvailableBytes);
                    string receivedJsonString = Encoding.UTF8.GetString(receivedBytes);
                    myData receivedStringData = JsonConvert.DeserializeObject<myData>(receivedJsonString);



                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while processing received JSON: " + ex.Message);
                }
                finally
                {
                    dataReader.Recycle();
                }
            };

            System.Threading.Thread.Sleep(100);

            Console.WriteLine(" [ NETWORK ] Server is waiting for connections");
            GameControllers.MainMenuController.HostMenuInitialize();

        }

        public void Update()
        {
            if (server != null)
            {
                server.PollEvents();
                if(GameControllers.playerCountShips == 0)
                {
                    var JSONPT = GameControllers.GameDataJSON.ServerJSON("l", 0);
                    var JSONPO = GameControllers.GameDataJSON.ServerJSON("l", 1);


                    GameControllers.enemyCountShips = -999;

                    SendDataToClient(PeerTwo, JSONPT);
                    SendDataToClient(PeerTwo, JSONPO);
                    ServerStop();

                }
                else if(GameControllers.enemyCountShips  == 0)
                {
                    var JSONPT = GameControllers.GameDataJSON.ServerJSON("l", 1);  // main player
                    var JSONPO = GameControllers.GameDataJSON.ServerJSON("l", 0);  // enemy player

                    GameControllers.enemyCountShips = -999;
                    GameControllers.playerCountShips = -999;

                    SendDataToClient(PeerTwo, JSONPT);
                    SendDataToClient(PeerTwo, JSONPO);
                    ServerStop();

                }
            }

        }

        public void ServerStop()
        {
            if(server != null)
            {
                server.Stop();
                Console.WriteLine(" [ NETWORK ] Server stop");

            }
        }

        public void SendDataToClient(NetPeer clientPeer, string data)
        {
            try
            {
                byte[] jsonBytes = Encoding.UTF8.GetBytes(data);
                clientPeer.Send(jsonBytes, DeliveryMethod.ReliableOrdered);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar datos al cliente: " + ex.Message);
            }
            Console.WriteLine("[ DATA SEND ]: " + data);

        }
    }

}

/*
 * 
                    if (receivedStringData.action == "c")
                    {

                        var JSON = GameControllers.GameDataJSON.ServerJSON("c", 1);
                        SendDataToClient(peer, JSON);
                    }
                    

                    if (receivedStringData.action == "b")
                    {
                        // SET PLAYER TWO SHIPS POSITIONS 
                        if (peer == PeerOne)
                        {
                            GameControllers.GameStatesSystem.playerOneSetPositions(receivedStringData.ships.p);
                            GameControllers.GameStatesSystem.playerOneSetPositions(receivedStringData.ships.b);
                            GameControllers.GameStatesSystem.playerOneSetPositions(receivedStringData.ships.s);
                            isShipPlayerOneRedy = true;
                            Console.WriteLine("[ SERVER ] Ships Recibed PEER ONE");
                            GameControllers.PrintMatrix(GameControllers.enemyMatrix);

                        }
                        if (peer == PeerTwo)
                        {
                            GameControllers.GameStatesSystem.playerTwoSetPositions(receivedStringData.ships.p);
                            GameControllers.GameStatesSystem.playerTwoSetPositions(receivedStringData.ships.b);
                            GameControllers.GameStatesSystem.playerTwoSetPositions(receivedStringData.ships.s);
                            isShipPlayerTwoRedy = true;
                            Console.WriteLine("[ SERVER ] Ships Recibed PEER TWO");
                            GameControllers.PrintMatrix(GameControllers.playerMatrix);
                        }
                        if(isShipPlayerOneRedy && isShipPlayerTwoRedy)
                        {

                            GameControllers.enemyCountShips = GameControllers.enemyShipsPositions.Count;
                            GameControllers.playerCountShips = GameControllers.playerShipsPositions.Count;

                            var JSON = GameControllers.GameDataJSON.ServerJSON("b", 1);
                            foreach (var p in server.ConnectedPeerList)
                            {
                                SendDataToClient(p, JSON);

                            }
                            Console.WriteLine("Two ships are ready ");
                            Console.WriteLine("[ SERVER ] Player Ship Count" + GameControllers.playerCountShips + " Enemy Ship Count " + GameControllers.enemyCountShips);


                        }
                    }
                    if (receivedStringData.action == "a")
                    {

                        if(peer == PeerOne)
                        {
                            attackPlayerOne = new Vector2(receivedStringData.position[0], receivedStringData.position[1]);
                            if (GameControllers.playerMatrix[receivedStringData.position[0], receivedStringData.position[1]] == 2)
                            {
                                TempPeerOne = GameControllers.GameDataJSON.ServerJSON("a", 1, attackPlayerOne);
                                GameControllers.playerCountShips--;

                            }
                            else
                            {
                                TempPeerOne = GameControllers.GameDataJSON.ServerJSON("a", 0, attackPlayerOne);

                            }
                            isPlayerOneAttackReady = true;

                        }
                        if (peer == PeerTwo)
                        {
                            attackPlayerTwo = new Vector2(receivedStringData.position[0], receivedStringData.position[1]);

                            if (GameControllers.enemyMatrix[receivedStringData.position[0], receivedStringData.position[1]] == 2)
                            {
                                TempPeerTwo = GameControllers.GameDataJSON.ServerJSON("a", 1, attackPlayerTwo);
                                GameControllers.enemyCountShips--;


                            }
                            else
                            {
                                TempPeerTwo = GameControllers.GameDataJSON.ServerJSON("a", 0, attackPlayerTwo);

                            }
                            isPlayerTwoAttackReady = true;

                        }
                        if (isPlayerOneAttackReady && isPlayerTwoAttackReady)
                        {
                            SendDataToClient(PeerTwo, TempPeerTwo);
                            SendDataToClient(PeerOne, TempPeerOne);
                            isPlayerOneAttackReady = false;
                            isPlayerTwoAttackReady =false;
                            TempPeerOne = "";
                            TempPeerTwo = "";
                        }
                    }
*/