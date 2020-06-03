using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dreamscape;
using LiteNetLib;
using System.Reflection;
using LiteNetLib.Utils;

namespace dreamhost.lua
{
    public static class CharacterCollection
    {

    }
    public class Character
    {
        private bool root;
        NetPeer player;
        public Character CharacterFromPeer(object peer)
        {
            if(root)
            {
                Character chara = new Character(false);
                chara.player = (NetPeer)peer;
                chara.root = false;
                return chara;
            } else
            {
                return null;
            }
        }
        public Character(bool root)
        {
            this.root = root;
        }
        public void LogEvent(string log)
        {
            if(!root)
            {
                NetDataWriter writer = new NetDataWriter();
                writer.Put((byte)NetTypes.Text);
                writer.Put((byte)ModifyTypes.AddSet);
                writer.Put("\n" + log);
                player.Send(writer, DeliveryMethod.ReliableOrdered);
            }
        }
        public void AddBrush(byte r, byte g, byte b)
        {
            if(!root)
            {
                NetDataWriter writer = new NetDataWriter();
                writer.Put((byte)NetTypes.Graphics);
                writer.Put((byte)DrawTypes.NewBrush);
                writer.Put(r);
                writer.Put(g);
                writer.Put(b);
                player.Send(writer, DeliveryMethod.ReliableOrdered);
            }
        }
        public void DrawTriangle(int brush, int x1, int x2, int x3, int y1, int y2, int y3)
        {
            if (!root)
            {
                NetDataWriter writer = new NetDataWriter();
                writer.Put((byte)NetTypes.Graphics);
                writer.Put((byte)DrawTypes.Triangle);
                writer.Put(brush);
                writer.Put(x1);
                writer.Put(y1);
                writer.Put(x2);
                writer.Put(y2);
                writer.Put(x3);
                writer.Put(y3);
                player.Send(writer, DeliveryMethod.ReliableOrdered);
            }
        }
        public void DrawLine(int brush, int x1, int x2, int y1, int y2)
        {
            if(!root)
            {
                NetDataWriter writer = new NetDataWriter();
                writer.Put((byte)NetTypes.Graphics);
                writer.Put((byte)DrawTypes.Line);
                writer.Put(brush);
                writer.Put(x1);
                writer.Put(x2);
                writer.Put(y1);
                writer.Put(y2);
                player.Send(writer, DeliveryMethod.ReliableOrdered);
            }
        }
        public void Fill(byte r, byte g, byte b)
        {
            if (!root)
            {
                NetDataWriter writer = new NetDataWriter();
                writer.Put((byte)NetTypes.Graphics);
                writer.Put((byte)DrawTypes.Clear);
                writer.Put(r);
                writer.Put(g);
                writer.Put(b);
                player.Send(writer, DeliveryMethod.ReliableOrdered);
            }
        }
        public void DrawText(byte brush, float x, float y, string text)
        {
            if(!root)
            {
                NetDataWriter writer = new NetDataWriter();
                writer.Put((byte)NetTypes.Graphics);
                writer.Put((byte)NetTypes.Text);
                writer.Put(brush);
                writer.Put(x);
                writer.Put(y);
                writer.Put(text);
            }
        }
    }
    public class LuaMath
    {
        public double Sin(double x)
        {
            return Math.Sin(x);
        }
        public double Cos(double x)
        {
            return Math.Cos(x);
        }
        public double Tan(double x)
        {
            return Math.Tan(x);
        }
        public double Pow(double x, double y)
        {
            return Math.Pow(x, y);
        }
        public double Round(double x)
        {
            return Math.Round(x);
        }
        public double Pi()
        {
            return Math.PI;
        }
        public double E()
        {
            return Math.E;
        }
        public double Min(double x, double y)
        {
            return Math.Min(x, y);
        }
        public double Max(double x, double y)
        {
            return Math.Max(x, y);
        }
        public Vector NewVector(double x, params double[] d)
        {
            int dimensions = d.Length;
            switch(dimensions)
            {
                case 2:
                    return new Vector3D(x, d[0], d[1]);
                case 1:
                    return new Vector2D(x, d[0]);
                default:
                    return new Vector(x,1);
            }
        }
    }
    public class Vector
    {
        public double x;
        public int dimensions;

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.x + b.x, 1);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.x - b.x, 1);
        }

        public static Vector operator *(Vector a, Vector b)
        {
            return new Vector(a.x * b.x, 1);
        }

        public static Vector operator *(Vector a, int b)
        {
            return new Vector(a.x * b, 1);
        }

        public static Vector operator /(Vector a, Vector b)
        {
            return new Vector(a.x / b.x, 1);
        }

        public static Vector operator /(Vector a, int b)
        {
            return new Vector(a.x / b, 1);
        }

        public Vector(double x, int dimensions)
        {
            this.x = x;
            this.dimensions = dimensions;
        }
    }
    public class Vector2D : Vector
    {
        public double y;

        public Vector2D(double x, double y) : base(x,2)
        {
            this.y = y;
        }

        public static Vector2D operator +(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.x + b.x, a.y + b.y);
        }

        public static Vector2D operator -(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.x - b.x, a.y - b.y);
        }

        public static Vector2D operator *(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.x * b.x, a.y * b.y);
        }

        public static Vector2D operator *(Vector2D a, int b)
        {
            return new Vector2D(a.x * b, a.y * b);
        }

        public static Vector2D operator /(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.x / b.x, a.y / b.y);
        }

        public static Vector2D operator /(Vector2D a, int b)
        {
            return new Vector2D(a.x / b, a.y / b);
        }
    }
    public class Vector3D : Vector
    {
        public double y;
        public double z;

        public Vector3D(double x, double y, double z) : base(x, 2)
        {
            this.y = y;
            this.z = z;
        }

        public static Vector3D operator +(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3D operator -(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3D operator *(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Vector3D operator *(Vector3D a, int b)
        {
            return new Vector3D(a.x * b, a.y * b, a.z * b);
        }

        public static Vector3D operator /(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.x / b.x, a.y / b.y, a.z / b.z);
        }

        public static Vector3D operator /(Vector3D a, int b)
        {
            return new Vector3D(a.x / b, a.y / b, a.z / b);
        }
    }
}
