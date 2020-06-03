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
    public class NetObject : Instance
    {
        public EventBasedNetListener listener;
        public NetManager manager;
        public NetObject(string name) : base(name)
        {
            listener = new EventBasedNetListener();
            manager = new NetManager(listener);
        }
        public virtual void Drop()
        {
            throw new NotImplementedException();
        }
        public override void Destroy()
        {
            Drop();
            base.Destroy();
        }
    }
}
