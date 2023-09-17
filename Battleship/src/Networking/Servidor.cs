using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Battleship.src.Networking.NetworkShared;
using System.Threading;
using LiteNetLib;
using LiteNetLib.Utils;
using System.Diagnostics;

namespace Battleship.src.Networking
{
    public class Servidor
    {
        public void Start()
        {
            EventBasedNetListener listener = new EventBasedNetListener();

            NetManager server = new NetManager(listener);
            server.Start(9050); // -> Set Port

            listener.ConnectionRequestEvent += request =>
            {
                if (server.ConnectedPeersCount < 10 /* max connections */)
                    request.AcceptIfKey("SomeConnectionKey");
                else
                    request.Reject();
            };

            listener.PeerConnectedEvent += peer =>
            {
                Console.WriteLine("We got connection: {0}", peer.EndPoint); // Show peer ip
                NetDataWriter writer = new NetDataWriter();                 // Create writer class
                writer.Put("Hello client!");                                // Put some string
                peer.Send(writer, DeliveryMethod.ReliableOrdered);             // Send with reliability
            };

            while (!Console.KeyAvailable)
            {
                server.PollEvents();
                Thread.Sleep(15);
            }

            server.Stop();
        }
       
    }
    
}
