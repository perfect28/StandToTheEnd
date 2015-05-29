using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace 一站到底客户端
{
    public partial class Welcom : Form
    {
        Fail fail ;
        Fighter fighter;
        PK pk ;
        StateWindow statewindow ;
        Form f;
        static public List<Problem> problem = new List<Problem>();
        public static String winner_name;

        static string[] messages;
        string recvStr = "";
        //string note = "Welcom";
        public static Socket c;
        public void run()
        {
            while (1==1)
            {
                try
                {
                    //recvStr = "";
                    byte[] recvBytes = new byte[1024];
                    int bytes;
                    bytes = c.Receive(recvBytes, recvBytes.Length, 0);    //从服务器端接受返回信息
                    recvStr += Encoding.Unicode.GetString(recvBytes, 0, bytes);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Socket Exception");
                }
            }
        }


        public Welcom()
        {
            InitializeComponent();
        }

        private void Welcom_Load(object sender, EventArgs e)
        {
            link();
            readproblems("problem.txt");
            //Control.CheckForIllegalCrossThreadCalls = false;
            f = this;

        }
        void link()
        {
            try
            {
                int port = 3000;
                FileStream fs = new FileStream("ip.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                string host = sr.ReadLine();
                sr.Close();
                fs.Close();
                //创建终结点EndPoint
                //IPAddress ip = IPAddress.Parse(host);



                IPHostEntry local = Dns.GetHostEntry("kkpanda-PC");

                //kkpanda-PC
                //创建终结点
                //IPAddress ip = IPAddress.Parse(host);
                IPAddress ip;
                int k;
                for (k = 0; k < local.AddressList.Length; k++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (local.AddressList[k].AddressFamily == AddressFamily.InterNetwork)
                    {
                        //MessageBox.Show(local.AddressList[k].ToString());
                        break;
                    }
                }

                ip = local.AddressList[k];
                IPEndPoint ipe = new IPEndPoint(ip, port);   //把ip和端口转化为IPEndPoint的实例

                //创建Socket并连接到服务器
                c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);   //  创建Socket
                c.Connect(ipe); //连接到服务器

                //向服务器发送信息
                string sendStr = "messages|1|Hello,this is a socket test";
                byte[] bs = Encoding.Unicode.GetBytes(sendStr);   //把字符串编码为字节

                c.Send(bs, bs.Length, 0); //发送信息

                Thread thread = new Thread(new ThreadStart(run));
                thread.Start();
                /*
                //接受从服务器返回的信息
                string recvStr = "";
                byte[] recvBytes = new byte[1024];
                int bytes;
                bytes = c.Receive(recvBytes, recvBytes.Length, 0);    //从服务器端接受返回信息
                recvStr += Encoding.ASCII.GetString(recvBytes, 0, bytes);
                Console.WriteLine("client get message:{0}", recvStr);    //回显服务器的返回信息

                Console.ReadLine();*/
                //一定记着用完Socket后要关闭
                //c.Close();
            }
            catch (ArgumentException e)
            {
                MessageBox.Show("argumentNullException");
                //("argumentNullException:{0}", e1);
            }
            catch (SocketException e)
            {
                MessageBox.Show("SocketException");
                //Console.WriteLine(":{0}", e1);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //link();
        }
        void dealmessage(string[] command)
        {
            if (f != this)
                f.Close();
            else
                f.Visible = false;
            if (command[1] == "To finished")
            {
                fail = new Fail();
                fail.Show();
                f = fail;
            }
            else if(command[1]=="To fighter")
            {
                fighter = new Fighter();
                winner_name = command[2];
                fighter.Show();
                fighter.welcom = this;
                f = fighter;
            }
            else if (command[1] == "ToPK")
            {
                pk = new PK();
                pk.gm1_name = messages[2];
                pk.gm2_name = messages[3];
                pk.Show();
                f = pk;
            }
            else if (command[1] == "Tostatewindow")
            {
                statewindow = new StateWindow();
                int i;
                for (i = 1; i <= 5; i++)
                    StateWindow.names[i] = command[i + 1];
                for (i = 7; i <= 11;i++ )
                {
                    if(command[i].Equals("1"))
                        StateWindow.states[i-6]=State.Alive;
                else if(command[i].Equals("0"))
                        StateWindow.states[i - 6] = State.Dead;
                }
                statewindow.gm1 = int.Parse(command[12]);
                statewindow.gm2 = int.Parse(command[13]);
                //note = "statewindow";
                statewindow.Show();
                f = statewindow;
            }
        }

        private void dealpk(string [] command)
        {
            if (command[1] == "New Pro")
            {
                int num = int.Parse(command[2]);
                pk.richtextbox.Visible = false;
                pk.label8.Text = "";
                pk.textbox.Text = "";
                int len = problem[num].Contents.Length;
                for (int i = 0; i < len; i++)
                {
                    pk.textbox.Text += problem[num].Contents[i];
                    if (i < len - 2)
                        if (problem[num].Contents[i + 1] >= 'A' && problem[num].Contents[i + 1] <= 'D' && problem[num].Contents[i + 2] == '.')
                            pk.textbox.Text += "\r\n";
                }


                pk.richtextbox.Text = problem[num].Answer;
                int n = int.Parse(command[3]);
                if (n == 0)
                {
                    pk.label[5].Text = "当前答题人："+pk.label[1].Text;
                    pk.label[3].Visible = true;
                    pk.label[4].Visible = false;
                    pk.label[3].Text = "0:" + problem[num].Time;
                }
                else
                {
                    pk.label[5].Text = "当前答题人："+pk.label[2].Text;
                    pk.label[3].Visible = false;
                    pk.label[4].Visible = true;
                    pk.label[4].Text = "0:" + problem[num].Time;
                }

                pk.turn = n;
                pk.time = problem[num].Time;
                pk.timer1.Start();
            }
            else if(command[1]=="Show answer")
            {
                pk.richtextbox.Visible = true;
                pk.timer1.Stop();
            }
            else if(command[1]=="Exchange")
            {
                pk.label8.Text = "";
                int n = int.Parse(command[3]);
                int num = int.Parse(command[2]);
                if (n == 0)
                {
                    pk.label[5].Text = pk.label[1].Text;
                    pk.label[3].Visible = true;
                    pk.label[4].Visible = false;
                    pk.label[3].Text = "0:" + problem[num].Time;
                }
                else
                {
                    pk.label[5].Text = pk.label[2].Text;
                    pk.label[3].Visible = false;
                    pk.label[4].Visible = true;
                    pk.label[4].Text = "0:" + problem[num].Time;
                }

                pk.turn = n;
                pk.time = problem[num].Time;
                pk.timer1.Start();
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (recvStr != "")
            {
                messages = recvStr.Split(new char[] { '|' });
                if (messages[0] == "message")
                {
                    dealmessage(messages);
                }
                else if (messages[0] == "PK")
                {
                    dealpk(messages);
                }
                else if (messages[0] == "State")
                {
                }
                recvStr = "";
            }
        }
        private void readproblems(string FileName)
        {
            problem.Add(new Problem());
            FileStream fs = new FileStream(FileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string txt = "";
            txt = sr.ReadLine();
            int n = int.Parse(txt);
            for (int i = 1; i <= n; i++)
            {
                Problem p = new Problem();
                txt = sr.ReadLine();
                p.Number = int.Parse(txt);
                txt = sr.ReadLine();
                p.Time = int.Parse(txt);
                txt = sr.ReadLine();
                p.Contents = txt;
                txt = sr.ReadLine();
                p.Answer = txt;
                problem.Add(p);
            }
            sr.Close();
            fs.Close();
        }


    }
}
