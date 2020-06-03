using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dreamhost.lua;
using System.Threading.Tasks;
using LiteNetLib;
using LiteNetLib.Utils;
using dreamscape;

namespace dreamhost.lua
{
    public class Player : Client
    {
        private Character character = new Character(false);
        public Player(string name, NetPeer peer) : base(name, peer)
        {
            Character root = new Character(true);
            character = root.CharacterFromPeer(peer);
        }
        public Character GetChar()
        {
            return character;
        }
        public void SetGameTitle(string title)
        {
            NetDataWriter writer = new NetDataWriter();
            writer.Put((byte)NetTypes.Info);
            writer.Put(title);
            peer.Send(writer, DeliveryMethod.ReliableOrdered);
        }
    }
}
