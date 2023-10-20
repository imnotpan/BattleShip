using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Battleship.src.Controllers.Enemy;
using Battleship.src.Controllers.Grids;
using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using Newtonsoft.Json; // Asegúrate de tener instalado el paquete Newtonsoft.Json para trabajar con JSON


namespace Battleship.src.Networking
{

    public class datagramServer
    {
        public string action { get; set; }
        public int status { get; set; }
        public int[] position { get; set; }
        public string reserva1 {  get; set; }
        public string reserva2 { get; set; }
    }


    public class clientSocket
    {
        string IP;
        int Port;
        public UdpClient client;
        private Thread receiveThread; // Hilo para el servidor
        GameDataJSON GameDataJSON;
        IPEndPoint serverEndPoint;
        GameControllers GameControllers;

        private bool isMyTurn = false;
        private bool canBuild = true;

        public clientSocket(GameDataJSON GameDataJSON, GameControllers GameControllers) {
            this.GameDataJSON = GameDataJSON;
            this.GameControllers = GameControllers;
        }

        public void Connect(string IP, int Port)
        {
            this.IP = IP;
            this.Port = Port;
            Console.WriteLine("Trying to connect: {0}:{1}", IP, Port);
            // Crear un objeto JSON
            var jsonObject = new
            {
                action = "c",
            };
            string jsonStr = JsonConvert.SerializeObject(jsonObject);

            try
            {
                client = new UdpClient();
                serverEndPoint = new IPEndPoint(IPAddress.Parse(IP), Port);

                var connectionMSG = GameDataJSON.ClientJSON("c", 0);
                sendData(connectionMSG);


                receiveThread = new Thread(ReceiveData);
                receiveThread.Start();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void sendData(string msg)
        {
            byte[] jsonBytes = Encoding.UTF8.GetBytes(msg);
            client.Send(jsonBytes, jsonBytes.Length, serverEndPoint);
            Console.WriteLine(msg);
            Console.WriteLine("Mensaje enviado al servidor UDP.");

        }

        private void ReceiveData()
        {
            bool isConnected = true; 

            while (isConnected)
            {
                try
                {
                    IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
                    byte[] receivedData = client.Receive(ref serverEndPoint);
                    string receivedMessage = Encoding.UTF8.GetString(receivedData);
                    datagramServer receivedStringData = JsonConvert.DeserializeObject<datagramServer>(receivedMessage);


                    Console.WriteLine("Respuesta del servidor: " + receivedMessage);
                    if(receivedStringData.action == "c" && receivedStringData.status == 0)
                    {
                        Disconnect();
                    }

                    if(receivedStringData.action == "s" && receivedStringData.status == 1)
                    {
                        GameControllers.MainMenuController.StartGame();
                        GameControllers.GameStatesSystem.StartGame();
                        GameControllers.Board.setGridsState(false);
                    }



                    if (receivedStringData.action == "b" && receivedStringData.status == 1 && canBuild)
                    {
                        GameControllers.GameHud.Initialize();
                        GameControllers.GameHud.setRedyViewHUD();
                        canBuild = false;
                    }



                    if (receivedStringData.action == "t" && receivedStringData.status == 1)
                    {
                        GameControllers.GameStatesSystem.StartAttackTurn();
                        isMyTurn = true;
                    }

                    if (receivedStringData.action == "a" && !isMyTurn)
                    {
                        Console.WriteLine("IS MY TURN + " + receivedStringData.position[0] + " " + receivedStringData.position[1]);
                        var relPos = new Vector2(receivedStringData.position[0], receivedStringData.position[1]);
                        foreach(GridTiny grid in GameControllers.tinyBoardGrids)
                        {
                            if(grid._relativePosition == relPos)
                            {
                                grid.currentColor = Color.Blue;
                            }
                        }

                    }

                    if (receivedStringData.action == "a" && isMyTurn)
                    {
                        isMyTurn = false;
                        foreach (Grid grid in GameControllers.GridsList)
                        {
                            if (grid._relativePosition == new Vector2(receivedStringData.position[0], receivedStringData.position[1]))
                            {
                                grid.currentColor = Color.Red;
                                grid.isDestroy = true;

                                if (receivedStringData.status == 1)
                                {
                                    var flagPosition = grid.Position;
                                    var flagEntity = new Flag(GameControllers.TextureLoader._gameTextures["Flag"], flagPosition);
                                    GameControllers.Scene.AddEntity(flagEntity);
                                    GameControllers.enemyCountShips--;
                                }
                            }
                        }
                    }

                    if (receivedStringData.action == "d")
                    {

                        Console.WriteLine("Desconexión exitosa.");
                        GameControllers.GameStatesSystem.BackToMainMenu();
                        client.Close();
                        client = null;


                        isConnected = false;
                        receiveThread.Join();
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al recibir datos del servidor: " + ex.Message);
                }
            }
        }
        public void Disconnect()
        {


        }
    }
}
