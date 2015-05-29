using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace 一站到底服务端
{
    public partial class PK : Form
    {
        public static List<Problem> problem = Welcom.problem;
        public static int win = 0;
        public static int replace = 0;
        public static String winner_name;
        public static Person[] person = Login.person;
        int turn;
        int time;
        int order= Welcom.now_pro;
        int gm1,gm2;
        public PK()
        {
            InitializeComponent();
        }
        public PK(int num1,int num2)
        {
            gm1 = num1;
            gm2 = num2;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stop();
        }


        void stop()
        {
            if (turn == 0)
            {
                win = gm2;
                person[gm1].state = State.Dead;
            }
            else
            {
                win = gm1;
                person[gm2].state = State.Dead;
            }
            int i;
            for (i = 1; i <= 5; i++)
                if (i != win && person[i].state == State.Alive)
                {
                    replace = i;
                    break;
                }
            if (i == 6)
            {
                replace = -1;
                winner_name = person[win].Name;
            }
            Close();

            Welcom.now_pro = order;
            new Fail().Show();

            string sendStr = "message|To finished";
            for (i = 1; i <= Welcom.N; i++)
            {
                byte[] bs = Encoding.Unicode.GetBytes(sendStr);
                Welcom.temp[i].socket.Send(bs, bs.Length, 0);  //返回信息给客户端
            }
        }

        private void PK_Load(object sender, EventArgs e)
        {
            label1.Text = person[gm1].Name;
            label2.Text = person[gm2].Name;
            turn = 1;
            label5.Text = "";
            label6.Text = "";
            label5.Visible = false;
            label6.Visible = false;
            //readproblems("problem.txt");
            
        }


        
        private void button1_Click(object sender, EventArgs e)
        {

            if (order == Welcom.now_pro)
            {
                button3.Enabled = true;
                button2.Enabled = true;
                button4.Enabled = true;
            }
            order++;

            if (order > problem.Count-1)
                order = 1;

            textBox1.Text = "";
            int len=problem[order].Contents.Length;
            for (int i = 0; i <len ; i++)
            {
                textBox1.Text += problem[order].Contents[i];
                if (i < len - 2)
                    if (problem[order].Contents[i + 1] >= 'A' && problem[order].Contents[i + 1] <= 'D' && problem[order].Contents[i + 2] == '.')
                        textBox1.Text += "\r\n";
            }
            richTextBox1.Text = problem[order].Answer;
            richTextBox1.Visible = false;

            turn = 1 - turn;
            if (turn==0)
            {
                label5.Visible = true;
                label6.Visible = false;
                label7.Text = "当前答题人：" + person[gm1].Name;
                label5.Text = "0:" + problem[order].Time;
            }
            else
            {
                label5.Visible = false;
                label6.Visible = true;
                label7.Text = "当前答题人：" + person[gm2].Name;
                label6.Text = "0:" + problem[order].Time;
            }

            string sendStr = "PK|New Pro|" + order.ToString() + "|" + turn.ToString();
            for (int i = 1; i <= Welcom.N; i++)
            {
                byte[] bs = Encoding.Unicode.GetBytes(sendStr);
                Welcom.temp[i].socket.Send(bs, bs.Length, 0);  //返回信息给客户端
            }


            time = problem[order].Time;
            timer1.Interval = 1000;
            timer1.Start();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = true;
            string sendStr = "PK|Show answer";
            for (int i = 1; i <= Welcom.N; i++)
            {
                byte[] bs = Encoding.Unicode.GetBytes(sendStr);
                Welcom.temp[i].socket.Send(bs, bs.Length, 0);  //返回信息给客户端
            }
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time--==0)
            {
                timer1.Stop();
                MessageBox.Show("不好意思，时间到了~");
                //stop();
            }
            if (time>=0)
            if (turn==0)
            {
                label5.Text = "0:" + time.ToString();
            }
            else
            {
                label6.Text = "0:" + time.ToString();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = problem[order].Contents;
            richTextBox1.Text = problem[order].Answer;
            richTextBox1.Visible = false;

            turn = 1 - turn;
            if (turn == 0)
            {
                label5.Visible = true;
                label6.Visible = false;
                label7.Text = "当前答题人：" + person[gm1].Name;
                label5.Text = "0:" + problem[order].Time;
            }
            else
            {
                label5.Visible = false;
                label6.Visible = true;
                label7.Text = "当前答题人：" + person[gm2].Name;
                label6.Text = "0:" + problem[order].Time;
            }

            string sendStr = "PK|Exchange|" + order.ToString()+ "|"+ turn.ToString();
            for (int i = 1; i <= Welcom.N; i++)
            {
                byte[] bs = Encoding.Unicode.GetBytes(sendStr);
                Welcom.temp[i].socket.Send(bs, bs.Length, 0);  //返回信息给客户端
            }

            time = problem[order].Time;
            timer1.Interval = 1000;
            timer1.Start();
        }
    }
}
