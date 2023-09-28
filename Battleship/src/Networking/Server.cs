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
    public class Server
    {
        public ServerListener ServerListener;    
        public NetManager server;
        public void Start()
        {
            ServerListener = new ServerListener();

            EventBasedNetListener listener = new EventBasedNetListener();

            server = new NetManager(listener);


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

        }

        public void Update()
        {
            server.PollEvents();
            if (server.ConnectedPeersCount < 1)
            {
                Console.WriteLine("Waiting For Connections");
            }
            Thread.Sleep(15);
        }

        public void ServerStop()
        {
            server.Stop();
        }
       
    }
    
}
