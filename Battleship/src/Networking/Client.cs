
using Battleship.src.Controllers;
using Battleship.src.Controllers.Grids;
using LiteNetLib;
using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;

namespace Battleship.src.Networking
{


    public class Client
    {

        public NetManager client;
        EventBasedNetListener listener;
        GameControllers GameControllers;
        public int GAMEID { get; set; }
        private System.Timers.Timer disconnectTimer;


        public Client(GameControllers GameControllers)
        {
            this.GameControllers = GameControllers;
            Connection();
        }

        public void Connection()
        {
            //Controllers
            var MainMenu = GameControllers.MainMenuController;
            var GameSystem = GameControllers.GameStatesSystem;

            listener = new EventBasedNetListener();
            client = new NetManager(listener);
            client.Start();


            listener.PeerDisconnectedEvent += (peer, disconnectInfo) =>
            {
                Console.WriteLine($"Cliente desconectado: {peer.EndPoint}");
                client.DisconnectPeer(peer);

                GameControllers.GameStatesSystem.BackToMainMenu();
                Console.WriteLine("[ Client ] Back to main menu");
            };

            listener.NetworkReceiveEvent += (fromPeer, dataReader, channel, deliveryMethod) =>
            {

                try
                {
   
                    byte[] receivedBytes = new byte[dataReader.AvailableBytes];
                    dataReader.GetBytes(receivedBytes, dataReader.AvailableBytes);
                    string receivedJsonString = Encoding.UTF8.GetString(receivedBytes);
                    datagramServer receivedStringData = JsonConvert.DeserializeObject<datagramServer>(receivedJsonString);


                    if (receivedStringData.action == "c")
                    {
                        GameControllers.GameStatesSystem.StartGame();
                        GameControllers.MainMenuController.StartGame();
                        Console.WriteLine("[ Client ] StartingGame");
                    }
                    if (receivedStringData.action == "b")
                    {
                        //GameControllers.GameStatesSystem.StartMultiplayerGame();
                        GameControllers.GameHud.CircleEntityEnemy.AddEntityOnScene(GameControllers.Scene);
                        GameControllers.enemyCountShips = 6;
                    }
                    if (receivedStringData.action == "a")
                    {
                        foreach (Grid grid in GameControllers.GridsList)
                        {
                            if (grid._relativePosition == new Vector2(receivedStringData.position[0], receivedStringData.position[1]))
                            {
                                grid.currentColor = Color.Red;
                                grid.isDestroy = true;

                                //Perfect Attack
                                if (receivedStringData.status == 1)
                                {
                                    var flagPosition = grid.Position;
                                    var flagEntity = new Flag(GameControllers.TextureLoader._gameTextures["Flag"], flagPosition);
                                    GameControllers.Scene.AddEntity(flagEntity);
                                    GameControllers.enemyCountShips--;
                                }
                            }
                        }
                        GameControllers.playerSelectedGrids.Clear();
                        //GameControllers.GameStatesSystem.StartMultiplayerGame();
                        Console.WriteLine("[ CLIENT ] Packet receive: " + receivedJsonString);
                    }

                    if(receivedStringData.action == "w")
                    {
                        GameControllers.GameStatesSystem.BackToMainMenu();
                    }

                    Console.WriteLine(receivedJsonString);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al procesar los datos recibidos: " + ex.Message);

                }
                finally
                {
                    dataReader.Recycle();
                }
            };

        }

        public void Connect(string IP, int PORT, int gameID)
        {

            //var GameNetworking = GameControllers.GameNetworking;

            //try
            //{
            //    GAMEID = gameID;
            //    client.Connect(IP, PORT, "BATTLESHIP");
            //    GameControllers.MainMenuController.HostServerWaiting();

            //    var JSON = GameControllers.GameDataJSON.ClientJSON(GAMEID, "c", 0);
            //    GameNetworking.Client.SendDataToServer(JSON);

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error de conexión: " + ex.Message);
            //    return;
            //}
        }


        public void Disconnect()
        {
            if (client != null)
            {
                //var JSON = GameControllers.GameDataJSON.ClientJSON(GAMEID, "d", 1);
                //GameControllers.GameNetworking.Client.SendDataToServer(JSON);
            }
        }


        public void Update()
        {
            if (client != null)
            {
                client.PollEvents();
            }

        }


        public void SendDataToServer(string data)
        {
            if (client != null && client.IsRunning)
            {
                try
                {

                    byte[] jsonBytes = Encoding.UTF8.GetBytes(data);
                    client.FirstPeer.Send(jsonBytes, DeliveryMethod.ReliableOrdered);


                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error sending JSON to server: " + ex.Message);
                }
            }
        }
    }
}

/*
 * 
                    if(receivedStringData.action == "c")
                    {
                        GameControllers.GameStatesSystem.StartGame();
                        GameControllers.MainMenuController.StartGame();
                        Console.WriteLine("[ Client ] StartingGame");
                    }

                    if (receivedStringData.action == "b")
                    {
                        GameControllers.GameStatesSystem.StartMultiplayerGame();
                        GameControllers.GameHud.CircleEntityEnemy.AddEntityOnScene(GameControllers.Scene);
                        GameControllers.enemyCountShips = 6;
                    }
                    if (receivedStringData.action == "a")
                    {
                        foreach (Grid grid in GameControllers.GridsList)
                        {
                            if (grid._relativePosition == new Vector2(receivedStringData.position[0], receivedStringData.position[1]))
                            {
                                grid.currentColor = Color.Red;
                                grid.isDestroy = true;

                                //Perfect Attack
                                if (receivedStringData.status == 1)
                                {
                                    var flagPosition = grid.Position;
                                    var flagEntity = new Flag(GameControllers.TextureLoader._gameTextures["Flag"], flagPosition);
                                    GameControllers.Scene.AddEntity(flagEntity);
                                    GameControllers.enemyCountShips--;
                                }
                            }
                        }
                        GameControllers.playerSelectedGrids.Clear();
                        GameControllers.GameStatesSystem.StartMultiplayerGame();
                        Console.WriteLine("[ CLIENT ] Packet receive: " + receivedJsonString);
                    }
*/