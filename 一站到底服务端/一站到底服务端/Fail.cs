using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;

namespace 一站到底服务端
{
    public partial class Fail : Form
    {
        static public Person[] person = Login.person;
        static public int N;
        public Fail()
        {
            InitializeComponent();
            N = Welcom.N;
        }

        private void Fail_Load(object sender, EventArgs e)
        {
            //SoundPlayer sp = new SoundPlayer(@"fail.wav");
            //sp.Play();
            //Thread.Sleep(5000);
            
            //new StateWindow().Show();
            //Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            new StateWindow().Show();
            Close();
        }

        private void Fail_FormClosing(object sender, FormClosingEventArgs e)
        {
            int i;
            if (PK.replace == -1)
            {
                string sendStr = "message|To fighter|"+PK.winner_name;
                for (i = 1; i <= N; i++)
                {
                    byte[] bs = Encoding.Unicode.GetBytes(sendStr);
                    Welcom.temp[i].socket.Send(bs, bs.Length, 0);  //返回信息给客户端
                }
            }
            else
            {
                string sendStr = "message|Tostatewindow|";
                for (i = 1; i <= 5; i++)
                {
                    sendStr += (person[i].Name + "|");
                }

                for (i = 1; i <= 5; i++)
                {
                    if (person[i].state == State.Alive)
                        sendStr += "1|";
                    else
                        sendStr += "0|";
                }
                sendStr += PK.win + "|" + PK.replace;
                for (i = 1; i <= N; i++)
                {
                    byte[] bs = Encoding.Unicode.GetBytes(sendStr);
                    Welcom.temp[i].socket.Send(bs, bs.Length, 0);  //返回信息给客户端
                }
            }
        }
    }
}
