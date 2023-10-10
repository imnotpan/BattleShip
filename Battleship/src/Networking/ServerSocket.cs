
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace Battleship.src.Networking
{
    public class ServerSocket
    {
        private UdpClient udpServer;
        private int serverPort;
        private Thread serverThread; // Hilo para el servidor


        public ServerSocket()
        {
            // Set up the server IP address and port
            IPAddress ipAddress = IPAddress.Parse("25.52.6.44"); // Use your desired IP address
            int port = 20001; // Choose a port number

            serverPort = port;
            udpServer = new UdpClient(serverPort);
        }

        public void Start()
        {
            serverThread = new Thread(ListenForClients);
            serverThread.Start();
        }

        public void Stop()
        {
            // Detener el servidor y el hilo
            udpServer.Close();
            serverThread.Join();
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
                    Console.WriteLine("Mensaje recibido del cliente {0}:{1}: {2}", clientEndPoint.Address, clientEndPoint.Port, receivedMessage);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
      
    }
}
