using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;
using System.Threading.Tasks;

namespace dreamhost.lua
{
    public class GameLibrary : Factory
    {
        public LuaMath math;
        public GameLibrary(string name) : base(name)
        {
            math = new LuaMath();
        }
        public Random GenerateRandom(int seed)
        {
            Random random = new Random(seed);
            return random;
        }
        public int FindEntropy()
        {
            return Guid.NewGuid().GetHashCode();
        }
        public void Iterate(object[] table,LuaFunction function)
        {
            foreach(object i in table)
            {
                function.Call(i);
            }
        }
        public void IterateList(List<object> table, LuaFunction function)
        {
            foreach(object i in table)
            {
                function.Call(i);
            }
        }
        public void Debug(string text)
        {
            Console.WriteLine("[DEBUG] " + text);
        }
    }
}
