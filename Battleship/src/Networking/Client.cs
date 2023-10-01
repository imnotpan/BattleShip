
using Battleship.src.Controllers;
using LiteNetLib;
using LiteNetLib.Utils;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;

namespace Battleship.src.Networking
{

    public class datagramServer
    {
        public string action { get; set; }
        public int status { get; set; }
        public int[] position { get; set; }

    }

    public class Client
    {

        NetManager client;
        EventBasedNetListener listener;
        GameControllers GameControllers;
        public Client(GameControllers GameControllers)
        {
            this.GameControllers = GameControllers;
        }

        public void Connection(string IP, int PORT)
        {
            //Controllers
            var MainMenu = GameControllers.MainMenuController;
            var GameSystem = GameControllers.GameStatesSystem;


            listener = new EventBasedNetListener();
            client = new NetManager(listener);
            client.Start();
            try
            {
                client.Connect(IP, PORT, "BATTLESHIP");
                GameControllers.MainMenuController.ClientMenuInitialize();
                GameControllers.GameStatesSystem.SIDE = "CLIENT";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error de conexión: " + ex.Message);
                return; 
            }

            listener.NetworkReceiveEvent += (fromPeer, dataReader, channel, deliveryMethod) =>
            {

                try
                {
     
                    byte[] receivedBytes = new byte[dataReader.AvailableBytes];
                    dataReader.GetBytes(receivedBytes, dataReader.AvailableBytes);
                    string receivedJsonString = Encoding.UTF8.GetString(receivedBytes);
                    datagramServer receivedStringData = JsonConvert.DeserializeObject<datagramServer>(receivedJsonString);

                    if(receivedStringData.action == "c")
                    {
                        GameControllers.GameStatesSystem.StartGame();
                        GameControllers.MainMenuController.StartGame();
                        Console.WriteLine("[ Client ] StartingGame");
                    }

                    if (receivedStringData.action == "b")
                    {
                        GameControllers.GameStatesSystem.StartGame();
                        GameControllers.MainMenuController.StartGame();
                        Console.WriteLine("[ Client ] StartingGame");
                    }



                    Console.WriteLine("[ CLIENT ] Packet receive: " + receivedJsonString);


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


        public void Disconnect()
        {
            if (client != null && client.IsRunning)
            {
                client.Stop();
                Console.WriteLine("[ NETWORK ] Disconnect from servers");
            }
        }
    }
}

