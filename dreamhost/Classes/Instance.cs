using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NLua;

namespace dreamhost.lua
{
    public class Instance
    {
        private Instance _Parent;
        public Instance Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                try
                {
                    _Parent.Children.Remove(this);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error removing children of old parent in instance " + Name);
                }
                _Parent = value;
                value.Children.Add(this);
            }
        }
        public List<Instance> Children { get; }
        public string Name;

        public Instance(string name)
        {
            Name = name;
            _Parent = null;
            Children = new List<Instance>();
        }

        public virtual Instance New(string name)
        {
            ConstructorInfo constructor = GetType().GetConstructor(Type.EmptyTypes);
            object[] Parameters = new object[]
            {
                name
            };
            Instance instance = constructor.Invoke(Parameters) as Instance;
            instance.Parent = this;
            Children.Add(instance);
            return instance;
        }

        public virtual Instance New(params object[] param)
        {
            ConstructorInfo constructor = GetType().GetConstructor(Type.EmptyTypes);
            Instance instance = constructor.Invoke(param) as Instance;
            instance.Parent = this;
            Children.Add(instance);
            return instance;
        }

        public virtual Instance New(string name, Instance parent)
        {
            ConstructorInfo constructor = GetType().GetConstructor(Type.EmptyTypes);
            object[] Parameters = new object[]
            {
                name
            };
            Instance instance = constructor.Invoke(Parameters) as Instance;
            instance.Parent = parent;
            parent.Children.Add(instance);
            return instance;
        }

        public virtual Instance New(Instance parent, params object[] param)
        {
            ConstructorInfo constructor = GetType().GetConstructor(Type.EmptyTypes);
            Instance instance = constructor.Invoke(param) as Instance;
            instance.Parent = parent;
            parent.Children.Add(instance);
            return instance;
        }

        public virtual void IterateChildren(LuaFunction function)
        {
            foreach(Instance i in Children)
            {
                function.Call(i);
            }
        }

        public virtual Instance FindFirstChild(string name)
        {
            foreach (Instance i in Children)
            {
                if (i.Name == name)
                {
                    return i;
                }
            }
            return null;
        }

        public virtual Instance C(string name)
        {
            return FindFirstChild(name);
        }

        public virtual void Destroy()
        {
            foreach (Instance i in Children)
            {
                i.Destroy();
            }
            Parent.Children.Remove(this);
            Name = "[DISPOSED]";
        }

        public override string ToString()
        {
            return Name;
        }
    }
    public class MetaInstance : Instance
    {
        public string Value;
        public MetaInstance(string name) : base(name)
        {

        }
    }
}
