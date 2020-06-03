using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net.Http;
using LiteNetLib;
using System.Threading;
using System.Net;
using Newtonsoft.Json;

namespace dreamscape
{
    public partial class Hub : Form
    {
        EventBasedNetListener netListener;
        NetManager client;
        List<string> serverslist;
        public Hub()
        {
            netListener = new EventBasedNetListener();
            client = new NetManager(netListener);
            InitializeComponent();
            serverslist = new List<string>();
            using(WebClient c = new WebClient())
            {
                string servers = c.DownloadString("http://atari0.cf/engine/servers.php");
                foreach (string i in servers.Split('|'))
                {
                    ServerItem v = JsonConvert.DeserializeObject<ServerItem>(i);
                    try
                    {
                        m_Games.Items.Add(v.name + " " + v.status);
                        serverslist.Add(i);
                    } catch (Exception)
                    {

                    }
                }
            }
        }

        private void M_Connect_Click(object sender, EventArgs e)
        {
            Game game = new Game(m_ipAddr.Text, 6698, m_Key.Text);
            game.Show();
            this.Visible = false;
            game.FormClosing += (send, args) =>
            {
                this.Visible = true;
            };
            game.FormClosed += (send, args) =>
            {
                game.Dispose();
            };
        }

        private void M_Games_SelectedValueChanged(object sender, EventArgs e)
        {
            string item = (string)m_Games.SelectedItem;
            foreach(string i in serverslist)
            {
                ServerItem v = JsonConvert.DeserializeObject<ServerItem>(i);
                if(item == v.name + " " + v.status)
                {
                    m_ipAddr.Text = v.ip;
                }
            }
        }

        private void Hub_Load(object sender, EventArgs e)
        {

        }
    }
    public class ServerItem
    {
        public string name;
        public string ip;
        public string status;
        public ServerItem(string ip, string status, string name)
        {
            this.ip = ip;
            this.name = name;
            this.status = status;
        }
    }
}
 