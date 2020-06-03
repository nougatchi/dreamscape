using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiscUtils;
using dreamhost.lua;
using Microsoft.VisualBasic;
using System.IO;
using DiscUtils.Raw;
using DiscUtils.Partitions;
using DiscUtils.Fat;
using System.Runtime.InteropServices;

namespace dreamcaster
{
    public partial class Form1 : Form
    {
        bool fileLoaded;
        FatFileSystem fs;
        FatFileSystem mfs;
        Disk disk;
        string cfile;
        List<LuaFile> lfiles;

        public Form1()
        {
            
            fileLoaded = false;
            InitializeComponent();
            lfiles = new List<LuaFile>();
            m_file.Text = @" ____  _____ _____ _____ _____ _____ _____ _____ _____ _____ _____ 
|    \| __  |   __|  _  |     |     |  _  |   __|_   _|   __| __  |
|  |  |    -|   __|     | | | |   --|     |__   | | | |   __|    -|
|____/|__|__|_____|__|__|_|_|_|_____|__|__|_____| |_| |_____|__|__|
      Dreamcaster                                   v1.0.0.0                

Designed for DHP1

By Ryelow

Dreamcaster
This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain. We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors. We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <https://unlicense.org>


Dreamhost
Copyright 2020-2021 Ryelow & Psychosis Interactive

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the 'Software'), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

The Software shall be used for Good, not Evil.

THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Dreamscapeshared.DLL
Copyright 2020-2021 Ryelow & Psychosis Interactive

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the 'Software'), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

DiscUtils (Dreamhost and Dreamcaster)
Copyright (c) 2008-2011, Kenneth Bell
Copyright (c) 2014, Quamotion

Permission is hereby granted, free of charge, to any person obtaining a
copy of this software and associated documentation files (the 'Software'),
to deal in the Software without restriction, including without limitation
the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/ or sell copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
DEALINGS IN THE SOFTWARE.

NLua and KeraLua (Dreamhost)
Copyright (c) 2020 Vinicius Jarina (viniciusjarina@gmail.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the 'Software'), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/ or sell
 copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:

            The above copyright notice and this permission notice shall be included in all
            copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!fileLoaded)
            {
                MessageBox.Show("Dreamcast v1.0.0.0\nDesigned for DHP1", "Dreamcaster");
            } else
            {
                MessageBox.Show("Dreamcast v1.0.0.0\nDesigned for DHP1\nMain tsect: " + fs.TotalSectors + "\nMeta tsect: " + mfs.TotalSectors + "\nParts: " + disk.Partitions.Count, "Dreamcaster");
            }
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fileLoaded)
            {
                string fname = Interaction.InputBox("Enter file name.", "Dreamcaster", "file.lua");
                LuaFile file = new LuaFile(fname, "-- " + fname);
                SparseStream strm = fs.OpenFile(fname, FileMode.Create);
                byte[] data = Encoding.ASCII.GetBytes(file.data);
                strm.Write(data, 0, data.Length);
                strm.Close();
                UpdateListbox();
            }
        }

        private void NewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Creating new project");
            fileLoaded = true;
            string flasize = Interaction.InputBox("Enter lua & asset size in megabytes.", "Dreamcaster", "30");
            long asSize = int.Parse(flasize) * 1024 * 1024;
            string fmdsize = Interaction.InputBox("Enter meta size in megabytes.", "Dreamcaster", "5");
            long mdSize = int.Parse(fmdsize) * 1024 * 1024;
            string fname = Interaction.InputBox("Enter project name.", "Dreamcaster", "project.bin");
            FileStream fst = File.Create(fname);
            Console.WriteLine("Asset Dat Size: " + asSize);
            Console.WriteLine("Media Dat Size: " + mdSize);
            Console.WriteLine("Total Dat Size: " + (asSize + mdSize));
            disk = Disk.Initialize(fst, Ownership.None, asSize + mdSize);
            BiosPartitionTable table = BiosPartitionTable.Initialize(disk);
            table.Create(asSize, WellKnownPartitionType.WindowsFat, false); // main assets
            table.Create(mdSize, WellKnownPartitionType.WindowsFat, false); // meta
            fs = FatFileSystem.FormatPartition(disk, 0, "DHP1-LA"); // lua and assets
            mfs = FatFileSystem.FormatPartition(disk, 1, "DHP1-MD");  // metadata
            fs.CreateDirectory("lua");
            mfs.CreateDirectory("mdlg");
            mfs.CreateDirectory("gpst");
            mfs.CreateDirectory(@"gpst\readonly");
            SparseStream mstream = mfs.OpenFile(@"mdlg\licn.mdt", FileMode.OpenOrCreate);
            SparseStream stream = fs.OpenFile(@"lua\main.lua", FileMode.Create);
            string defaultstr = @"local key = '' -- this is the key that people will use to enter your Server
-- if the key specified here is not the same as the key people use to join(e.g.the key is bivkoi and the user is joining with a key of "")
-- then the connection will be denied, and if the key is blank and someone joins with a key the connection will be denied aswell.
-- you can manually change how it accepts requests by changing the function peerhandle
local game = DataModel:C('Factories'):C('GameLibrary')
local characterfactory = DataModel:C('Factories'):C('CharacterFactory')
local math = game.math
local random = game:GenerateRandom(game:FindEntropy())
resolution = { 433, 426}

function peerhandle(request)
    request:AcceptIfKey(key)
end

function peerdisconnect(peer, info, player)
    player:Destroy()
end

function peerconnect(peer, player)
    game:Debug('New Connection')
    player = characterfactory:NewPlayer('newpeer', peer)
    char = player:GetChar()
    char:Fill(0, 0, 0)
    player:SetGameTitle('Dreamscape')
    char:AddBrush(0, 0, 0)-- brush 0 black
    char:AddBrush(255, 0, 0)-- brush 1 red
    char:AddBrush(0, 255, 0)-- brush 2 green
    char:AddBrush(0, 0, 255)-- brush 3 blue
    char:AddBrush(255, 0, 255)-- brush 4 red + blue
    char:AddBrush(255, 255, 0)-- brush 5 red + green
    char:AddBrush(0, 255, 255)-- brush 6 green + blue
    char:AddBrush(255, 255, 255)-- brush 7 white
    char:AddBrush(127, 127, 127)-- brush 8 gray
end

function act(action, peer, player)

end

function getinfo()
    return '<head><style>body { font-family: arial; }</style></head><body><h1>Generic Dreamscape Server</h1></body>'
end";
            byte[] defautlbyt = Encoding.ASCII.GetBytes(defaultstr);
            stream.Write(defautlbyt, 0, defautlbyt.Length);
            defaultstr = m_file.Text;
            defautlbyt = Encoding.ASCII.GetBytes(defaultstr);
            mstream.Write(defautlbyt, 0, defautlbyt.Length);
            stream.Close();
            mstream.Close();
            UpdateListbox();
            Text = "Dreamcaster -- " + fname;
            Console.WriteLine("Done");
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateListbox()
        {
            Console.WriteLine("Updating listbox");
            if(fileLoaded)
            {
                lfiles.Clear();
                foreach(string i in fs.GetFiles("lua"))
                {
                    Console.WriteLine("Adding " + i);
                    SparseStream strm = fs.OpenFile(i, FileMode.Open);
                    byte[] data = new byte[strm.Length];
                    strm.Read(data, 0, data.Length);
                    strm.Close();
                    string sdata = Encoding.ASCII.GetString(data);
                    LuaFile newfile = new LuaFile(i, sdata);
                    lfiles.Add(newfile);
                }
                m_luaFiles.Items.Clear();
                foreach(LuaFile file in lfiles)
                {
                    m_luaFiles.Items.Add(file);
                }
            }
        }

        private void CompileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fileLoaded)
            {
                UpdateListbox();
                fs.DeleteDirectory("lua",true);
                fs.CreateDirectory("lua");
                foreach(LuaFile file in lfiles)
                {
                    Console.WriteLine("Writing " + file.name);
                    SparseStream stream = fs.OpenFile(file.name, FileMode.Create);
                    stream.Write(Encoding.ASCII.GetBytes(file.data), 0, file.data.Length);
                    stream.Close();
                    Console.WriteLine("Wrote");
                }
                MessageBox.Show("Finished saving.", "Dreamcaster");
            }
        }

        private void M_luaFiles_SelectedValueChanged(object sender, EventArgs e)
        {
            if(fileLoaded)
            {
                LuaFile file = ((LuaFile)m_luaFiles.Items[m_luaFiles.SelectedIndex]);
                cfile = file.name;
                m_file.Text = file.data;
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileLoaded)
            {
                LuaFile file = m_luaFiles.SelectedItem as LuaFile;
                Console.WriteLine("Saving " + file.name);
                SparseStream strm = fs.OpenFile(file.name, FileMode.Create, FileAccess.Write);
                byte[] data = Encoding.ASCII.GetBytes(file.data);
                strm.Write(data, 0, data.Length);
                strm.Close();
                Console.WriteLine("Saved");
                UpdateListbox();
            }
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Opening new project");
            fileLoaded = true;
            string fname = Interaction.InputBox("Enter project name.", "Dreamcaster", "project.bin");
            disk = new Disk(fname);
            PartitionTable partitions = disk.Partitions;
            PartitionInfo fs_pi = partitions[0];
            fs = new FatFileSystem(fs_pi.Open());
            PartitionInfo mfs_pi = partitions[1];
            mfs = new FatFileSystem(mfs_pi.Open());
            UpdateListbox();
            Text = "Dreamcaster -- " + fname;
            Console.WriteLine("Done");
        }

        private void UnloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fileLoaded)
            {
                lfiles = new List<LuaFile>();
                fs.Dispose();
                mfs.Dispose();
                disk.Dispose();
                m_luaFiles.Items.Clear();
                m_file.Text = "Project unloaded";
                Text = "Dreamcaster";
                fileLoaded = false;
            }
        }
    }

    public class LuaFile
    {
        public string name;
        public string data;
        public LuaFile(string name, string data)
        {
            this.name = name;
            this.data = data;
        }
        public override string ToString()
        {
            return name + " " + base.ToString();
        }
    }
}
