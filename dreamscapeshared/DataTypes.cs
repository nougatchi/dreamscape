using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dreamscape
{
    public enum ClientSend : byte
    {
        Act
    }
    public enum NetTypes : byte
    {
        Unknown,
        UIChange,
        Text,
        Graphics,
        Goodbye,
        Info,
    }
    public enum DrawTypes : byte
    {
        Clear,
        Fill,
        Line,
        Rect,
        Triangle,
        Text,
        NewBrush,
    }
    public enum UITypes : byte
    {
        Tab,
        Log,
    }
    public enum ModifyTypes : byte
    {
        AddSet,
        Remove,
    }
}
