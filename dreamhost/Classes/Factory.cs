using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dreamhost.lua
{
    public class Factory : Instance
    {
        public Factory(string name) : base(name)
        {
            
        }

        public override void Destroy()
        {
            throw new InvalidOperationException("Not allowed to destroy Factory");
        }
    }
}
