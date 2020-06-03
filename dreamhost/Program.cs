using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dreamscape;
using LiteNetLib;
using LiteNetLib.Utils;
using NLua;
using System.Threading;
using LiteNetLib.Layers;
using dreamhost.lua;

namespace dreamhost
{
    class Program
    {
        Lua lua;
        EventBasedNetListener netListener;
        NetManager client;
        EventBasedNetListener inetListener;
        NetManager iclient;
        Instance gameInstance;
        long svtime;
        private CharacterFactory characterFactory;

        static void Main(string[] args)
        {
            Console.Title = "Dreamhost";
            if (!File.Exists("eula.toggle"))
            {
                Console.WriteLine("DREAMHOST LICENSE");
                Console.WriteLine("Copyright 2020-2021 Ryelow & Psychosis Interactive\n\nPermission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the 'Software'), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:\n\nThe above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.\n\nThe Software shall be used for Good, not Evil.\n\nTHE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                Console.WriteLine();
                File.WriteAllText("eula.toggle", "DO NOT DELETE THIS FILE\nunless if you want to see the license again");
            }
            Console.WriteLine("Dreamhost\nDesigned for DHP1");
            Program program = new Program();
            program.MainC(args);
        }
        void MainC(string[] args)
        {
            svtime = DateTime.Now.Ticks;
            Console.WriteLine("Starting...");
            netListener = new EventBasedNetListener();
            client = new NetManager(netListener, new XorEncryptLayer("dreamscape"));
            inetListener = new EventBasedNetListener();
            iclient = new NetManager(inetListener);
            lua = new Lua();
            gameInstance = new Instance("DataModel");
            Factory factoryContainer = new Factory("Factories");
            factoryContainer.Parent = gameInstance;
            CharacterFactory characterFactory = new CharacterFactory("CharacterFactory");
            characterFactory.Parent = factoryContainer;
            GameLibrary gameLibrary = new GameLibrary("GameLibrary");
            gameLibrary.Parent = factoryContainer;

            lua.LoadCLRPackage();
            lua.DebugHook += Lua_DebugHook;
            string code = File.ReadAllText("server.lua");
            lua["netwriter"] = new NetDataWriter();
            lua["netserver"] = client;
            lua["DataModel"] = gameInstance;
            lua.DoString("import('dreamhost','dreamscape','System','NLua','LiteNetLib','LiteNetLib.Utils','dreamhost.lua')\n"+code,svtime.ToString());
            
            this.characterFactory = characterFactory;
            client.Start(6698);
            Console.WriteLine("Server ready! Hosting on UDP port 6698.");
            client.AutoRecycle = true;
            netListener.ConnectionRequestEvent += (request) =>
            {
                Console.WriteLine("Connection");
                LuaFunction func = lua["peerhandle"] as LuaFunction;
                try
                {
                    func.Call(request);
                } catch(Exception e)
                {
                    request.Accept();
                    Console.WriteLine(e);
                }
            };
            netListener.PeerDisconnectedEvent += (peer, info) =>
            {
                Player plr = null;
                foreach (Player i in characterFactory.Children)
                {
                    if (i.peer == peer)
                    {
                        plr = i;
                    }
                }
                Console.WriteLine("Disconnect");
                LuaFunction func = lua["peerdisconnect"] as LuaFunction;
                try
                {
                    func.Call(peer, info, plr);
                } catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            };
            netListener.PeerConnectedEvent += (peer) =>
            {
                Player player = new Player(gameLibrary.FindEntropy().ToString(), peer);
                player.Parent = characterFactory;
                Console.WriteLine("Successful Connect");
                LuaFunction func = lua["peerconnect"] as LuaFunction;
                try
                {
                    func.Call(peer, player);
                } catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            };
            netListener.NetworkReceiveEvent += NetworkReceive;
            Console.WriteLine("Starting info server...");
            inetListener.PeerConnectedEvent += (peer) =>
            {
                Console.WriteLine("Sending peer info");
                NetDataWriter writer = new NetDataWriter();
                LuaFunction func = lua["getinfo"] as LuaFunction;
                try
                {
                    writer.Put(func.Call(peer).First() as string);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    writer.Put("Please contact the <b>SYSOP</b> for this server.\n" + e);
                }
                peer.Send(writer, DeliveryMethod.ReliableSequenced);
            };
            inetListener.ConnectionRequestEvent += (request) =>
            {
                Console.WriteLine("Connected to info");
                request.Accept();
            };
            iclient.Start(6699);
            iclient.AutoRecycle = true;
            Console.WriteLine("Info server ready! Hosting on UDP port 6699.");
            while (true)
            {
                client.PollEvents();
                Thread.Sleep(15);
            }
        }

        private void Lua_DebugHook(object sender, NLua.Event.DebugHookEventArgs e)
        {

        }

        private void NetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            try
            {
                Player plr = null;
                foreach (Player i in characterFactory.Children)
                {
                    if (i.peer == peer)
                    {
                        plr = i;
                    }
                }
                Console.WriteLine("Data recv.");
                switch ((ClientSend)reader.GetByte())
                {
                    case ClientSend.Act:
                        LuaFunction actfunc = lua["act"] as LuaFunction;
                        actfunc.Call(reader.GetString(), peer, plr);
                        break;
                }
            }catch(Exception e)
            {
                NetDataWriter writer = new NetDataWriter();
                writer.Put((byte)NetTypes.Text);
                writer.Put((byte)ModifyTypes.AddSet);
                writer.Put("\nAn internal server error occoured.\n" + e + "\n");
                Console.WriteLine(e);
            }
        }
    }
}
