using LiteNetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dreamhost.lua
{
    public class CharacterFactory : Factory
    {
        public CharacterFactory(string name) : base(name)
        {

        }

        public Player NewPlayer(string name, NetPeer peer)
        {
            Player player = new Player(name, peer);
            player.Parent = this;
            return player;
        }

        public Client NewClient(string name, NetPeer peer)
        {
            Client client = new Client(name, peer);
            return client;
        }
    }
}
