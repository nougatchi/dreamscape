using LiteNetLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using dreamscape;
using System.Threading;
using LiteNetLib.Utils;
using LiteNetLib.Layers;

namespace dreamscape
{
    public partial class Game : Form
    {
        EventBasedNetListener netListener;
        NetManager client;
        NetPeer host;
        string ip;
        int port;
        string key;
        Image img_graphics;
        Graphics graphics;
        List<Brush> Brushes;
        public Game(string ip, int port, string key)
        {
            netListener = new EventBasedNetListener();
            client = new NetManager(netListener, new XorEncryptLayer("dreamscape"));
            client.Start();
            InitializeComponent();
            this.ip = ip;
            this.port = port;
            this.key = key;
            Brushes = new List<Brush>(byte.MaxValue);
            m_Graphics.Image = new Bitmap(360, 240);

            graphics = m_Graphics.CreateGraphics();
            graphics.SmoothingMode = SmoothingMode.HighSpeed;
            client.AutoRecycle = true;
            host = client.Connect(ip, port, key);
            netListener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
            {
                Console.WriteLine("Data received!");
                foreach(byte i in dataReader.RawData)
                {
                    Console.Write(i.ToString("X") + " " + (char) i + " " + ((NetTypes) i).ToString() + " " + ((DrawTypes) i).ToString() + " " + ((ModifyTypes) i).ToString() + " ");
                }
                Console.WriteLine();
                switch((NetTypes) dataReader.GetByte())
                {
                    case NetTypes.Info:
                        Invoke(new Action(() =>
                            Text = dataReader.GetString()
                        ));
                        break;
                    case NetTypes.UIChange:

                        break;
                    case NetTypes.Graphics:
                        switch((DrawTypes) dataReader.GetByte())
                        {
                            case DrawTypes.NewBrush:
                                SolidBrush sb_brush = new SolidBrush(Color.FromArgb(dataReader.GetByte(), dataReader.GetByte(), dataReader.GetByte()));
                                Brushes.Add(sb_brush);
                                break;
                            case DrawTypes.Line:
                                int brush_line_num = dataReader.GetInt();
                                graphics.DrawLine(new Pen(Brushes[brush_line_num]), dataReader.GetInt(), dataReader.GetInt(), dataReader.GetInt(), dataReader.GetInt());
                                break;
                            case DrawTypes.Text:
                                int brush_txt_num = dataReader.GetInt();
                                PointF text_string_point = new PointF(dataReader.GetFloat(), dataReader.GetFloat());
                                graphics.DrawString(dataReader.GetString(), Font, Brushes[brush_txt_num], text_string_point);
                                break;
                            case DrawTypes.Triangle:
                                int brush_tri_num = dataReader.GetInt();
                                Point[] brush_tri_points = new Point[3];
                                brush_tri_points[0] = new Point(dataReader.GetInt(), dataReader.GetInt());
                                brush_tri_points[1] = new Point(dataReader.GetInt(), dataReader.GetInt());
                                brush_tri_points[2] = new Point(dataReader.GetInt(), dataReader.GetInt());
                                graphics.DrawPolygon(new Pen(Brushes[brush_tri_num]), brush_tri_points);
                                break;
                            case DrawTypes.Fill:
                                switch((DrawTypes) dataReader.GetByte())
                                {
                                    case DrawTypes.Triangle:
                                        int brush_ftri_num = dataReader.GetInt();
                                        Point[] brush_ftri_points = new Point[3];
                                        brush_ftri_points[0] = new Point(dataReader.GetInt(), dataReader.GetInt());
                                        brush_ftri_points[1] = new Point(dataReader.GetInt(), dataReader.GetInt());
                                        brush_ftri_points[2] = new Point(dataReader.GetInt(), dataReader.GetInt());
                                        graphics.FillPolygon(Brushes[brush_ftri_num], brush_ftri_points);
                                        break;
                                    case DrawTypes.Rect:
                                        graphics.FillRectangle(Brushes[dataReader.GetInt()],Rectangle.FromLTRB(dataReader.GetInt(), dataReader.GetInt(), dataReader.GetInt(), dataReader.GetInt()));
                                        break;
                                }
                                break;
                            case DrawTypes.Clear:
                                graphics.Clear(Color.FromArgb(dataReader.GetByte(), dataReader.GetByte(), dataReader.GetByte()));
                                break;
                        }
                        break;
                    case NetTypes.Text:
                        switch((ModifyTypes) dataReader.GetByte())
                        {
                            case ModifyTypes.AddSet:
                                m_Log.Invoke(new Action(() =>
                                    m_Log.DocumentText += dataReader.GetString()
                                ));
                                break;
                            case ModifyTypes.Remove:
                                m_Log.Invoke(new Action(() =>
                                    m_Log.DocumentText = ""
                                ));
                                break;
                        }
                        break;
                }
                m_toolstrip_ping.Text = "Ping: " + fromPeer.Ping + "ms";
                m_Graphics.Invoke(new Action(() =>
                    m_Graphics.Update()
                )) ;
            };
            bw_Net.RunWorkerAsync(client);
        }

        private void Game_Load(object sender, EventArgs e)
        {
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            NetManager cli = (NetManager)e.Argument;
            while (!bw_Net.CancellationPending)
            {
                if (Visible)
                {
                    cli.PollEvents();
                    m_toolstrip_ping.Text = "Ping: " + cli.FirstPeer.Ping + "ms";
                }
            }
            cli.DisconnectPeer(cli.FirstPeer);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            NetDataWriter writer = new NetDataWriter();
            writer.Put((byte)ClientSend.Act);
            writer.Put(m_TextInput.Text);
            host.Send(writer,0,DeliveryMethod.ReliableOrdered);
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            bw_Net.CancelAsync();
        }
    }
}