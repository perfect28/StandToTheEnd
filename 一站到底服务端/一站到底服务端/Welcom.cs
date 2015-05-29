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
using System.IO;

namespace 一站到底服务端
{
    
    public partial class Welcom : Form
    {
        public static MySocket[] temp =new  MySocket[3];
        public static List<Problem> problem = new List<Problem>();
        public static int now_pro;
        public static int N = 1;
        public Welcom()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Visible = false;
            new Login().Show();
        }

        private void Welcom_Load(object sender, EventArgs e)
        {
            readproblems("problem.txt");
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

            FileStream fs2 = new FileStream("order.txt", FileMode.Open);
            StreamReader sr2 = new StreamReader(fs2, Encoding.Default);
            string order = sr2.ReadLine();
            sr2.Close();
            fs2.Close();

            now_pro = int.Parse(order);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("成功发起连接,请等待！");
            link();
        }
        void link()
        {
            int port = 3000;
            

            //MessageBox.Show(Dns.GetHostName().ToString());
            //IPHostEntry local = Dns.GetHostByName(Dns.GetHostName());
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
                    MessageBox.Show(local.AddressList[k].ToString());
                    break;
                }
            }

            ip = local.AddressList[k];
            IPEndPoint ipe = new IPEndPoint(ip, port);

            //创建Socket并开始监听

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 
            //创建一个Socket对象，如果用UDP协议，则要用SocketTyype.Dgram类型的套接字
            s.Bind(ipe);    //绑定EndPoint对象(2000端口和ip地址)
            s.Listen(0);    //开始监听

            for (int i = 1; i <= N; i++)
            {
                temp[i] = new MySocket();
                label1.Text = "等待客户端连接";
                temp[i].socket = s.Accept();   //为新建立的连接创建新的Socket
                label1.Text = "连接成功";

                string recvStr = "";
                byte[] recvBytes = new byte[1024];
                int bytes;
                bytes = temp[i].socket.Receive(recvBytes, recvBytes.Length, 0); //从客户端接受消息
                recvStr += Encoding.Unicode.GetString(recvBytes, 0, bytes);
                string[] messages = recvStr.Split(new char[] { '|' });
                label1.Text = messages[1]+"号客户端成功连接!";
                temp[i].number = int.Parse(messages[1]);
            }
        }
    }
}
