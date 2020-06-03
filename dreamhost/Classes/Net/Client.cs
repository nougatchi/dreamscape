using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dreamscape;
using LiteNetLib;
using LiteNetLib.Utils;

namespace dreamhost.lua
{
    public class Client : NetObject
    {
        public NetPeer peer;
        public int Ping {
            get
            {
                return peer.Ping;
            }
        }
        public long RemoteTime
        {
            get {
                return peer.RemoteTimeDelta;
            }
        }
        public int Mtu
        {
            get
            {
                return peer.Mtu;
            }
        }
        public Client(string name, NetPeer peer) : base(name)
        {
            this.peer = peer;
        }
        public override void Drop()
        {
            Drop("Your connection has been dropped.");
        }
        public void Drop(string reason)
        {
            NetDataWriter writer = new NetDataWriter();
            writer.Put((byte)NetTypes.Goodbye);
            writer.Put(reason);
            peer.Send(writer, DeliveryMethod.ReliableOrdered);
            peer.Disconnect();
        }
    }
}
