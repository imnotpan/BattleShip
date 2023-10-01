using System;
using LiteNetLib;
using System.Text;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Linq;
using Battleship.src.Controllers;

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

        public bool shipInPositionPeerOne;
        public bool shipInPositionPeerTwo;

        public NetPeer PeerOne;
        public NetPeer PeerTwo;
        private int connectedPeerCount = 0; 


        GameControllers GameControllers;
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
                        Console.WriteLine("[ SERVER ]MAX PLAYER REACHED, GAME STARTING");
                        var JSON = GameControllers.GameDataJSON.ClientJSON("c", 0);
                        foreach(var peer in server.ConnectedPeerList)
                        {
                            SendDataToClient(peer, JSON);

                        }

                    }
                }
                else
                {
                    request.Reject();

                }
            };

            listener.PeerConnectedEvent += peer =>
            {

                Console.WriteLine("We got connection: {0}", peer.EndPoint);
                var TextComponent = GameControllers.MainMenuController.ClientStateText._textComponent;
                TextComponent.Text = TextComponent.Text + "\n"+ peer.EndPoint;

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
                    // RECEPCION PAQUETE SI FUNCIONA :')
                    byte[] receivedBytes = new byte[dataReader.AvailableBytes];
                    dataReader.GetBytes(receivedBytes, dataReader.AvailableBytes);
                    string receivedJsonString = Encoding.UTF8.GetString(receivedBytes);
                    myData receivedStringData = JsonConvert.DeserializeObject<myData>(receivedJsonString);


                    if (receivedStringData.action == "c")
                    {

                        var JSON = GameControllers.GameDataJSON.ServerJSON("c", 1);
                        SendDataToClient(peer, JSON);
                    }
                    

                    if (receivedStringData.action == "b")
                    {
                        // SET PLAYER ONE SHIPS POSITIONS 

                        // SET PLAYER TWO SHIPS POSITIONS 

                        GameControllers.GameStatesSystem.multiplayerSavePosition(receivedStringData.ships.b);
                        GameControllers.GameStatesSystem.multiplayerSavePosition(receivedStringData.ships.p);
                        GameControllers.GameStatesSystem.multiplayerSavePosition(receivedStringData.ships.s);
                        GameControllers.GameStatesSystem.isShipClientPositions = true;

                        Console.WriteLine("[ SERVER ] Ships Recibed");
                        
                    }


                    Console.WriteLine("[ SERVER ] Packet Recibe" + receivedJsonString);

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
