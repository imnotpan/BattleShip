using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json; // Asegúrate de tener instalado el paquete Newtonsoft.Json para trabajar con JSON


namespace Battleship.src.Networking
{
    public class clientSocket
    {
        public clientSocket() {
        }

        public void Connect(GameControllers GameControllers)
        {
            // IPAddress IP = IPAddress.Parse("127.0.0.1"); // Replace with the desired IP address
            string IP = "25.48.213.201";
            int Port = 20001;        // Replace with the server's port number
            Console.WriteLine("Trying to connect: {0}:{1}", IP, Port);
            // Crear un objeto JSON
            var jsonObject = new
            {
                action = "c",
            };
            string jsonStr = JsonConvert.SerializeObject(jsonObject);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonStr);

            try
            {
                UdpClient client = new UdpClient();
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
                client.Send(jsonBytes, jsonBytes.Length, serverEndPoint);
                Console.WriteLine("Mensaje enviado al servidor UDP.");

                byte[] respuestaBytes = client.Receive(ref serverEndPoint);
                string respuesta = Encoding.UTF8.GetString(respuestaBytes);

                Console.WriteLine("Respuesta del servidor UDP: " + respuesta);

                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }


    }
}
