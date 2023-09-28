
using LiteNetLib;
using LiteNetLib.Utils;
using System;
using System.Threading;

namespace Battleship.src.Networking
{
    public class Client
    {
        public void Connection()
        {
            EventBasedNetListener listener = new EventBasedNetListener();
            NetManager client = new NetManager(listener);
            client.Start();
            try
            {
                client.Connect("localhost", 9050, "SomeConnectionKey");
                Console.WriteLine("Conectado al servidor.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error de conexión: " + ex.Message);
                return; // Salir del método si la conexión falla
            }

            listener.NetworkReceiveEvent += (fromPeer, dataReader, channel, deliveryMethod) =>
            {
                Console.WriteLine("We got: {0}", dataReader.GetString(100 /* max length of string */));
                dataReader.Recycle();
            };

            while (!Console.KeyAvailable)
            {
                client.PollEvents();
                Thread.Sleep(15);
            }

            client.Stop();
        }

    }
}

