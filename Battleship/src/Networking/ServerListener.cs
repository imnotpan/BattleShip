using LiteNetLib;
using LiteNetLib.Utils;
using System;
using System.Net;
using System.Net.Sockets;

namespace Battleship.src.Networking
{
    public class ServerListener : INetEventListener
    {
        public void OnConnectionRequest(ConnectionRequest request)
        {
            request.AcceptIfKey("SomeConnectionKey");

        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {
            Console.WriteLine("Error de red en " + endPoint + ", Código de error: " + socketError);

        }

        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {
            throw new NotImplementedException();

        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, byte channelNumber, DeliveryMethod deliveryMethod)
        {
            throw new NotImplementedException();
        }

        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {
            throw new NotImplementedException();
        }

        public void OnPeerConnected(NetPeer peer)
        {
            Console.WriteLine("Cliente conectado: " + peer.EndPoint);
            NetDataWriter writer = new NetDataWriter();                 
            writer.Put("Hello client!");
            peer.Send(writer, DeliveryMethod.ReliableOrdered);

        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            Console.WriteLine("Cliente desconectado: " + peer.EndPoint + ", Razón: " + disconnectInfo.Reason);

        }
    }
}
