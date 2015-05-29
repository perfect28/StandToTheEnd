using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 一站到底服务端
{
    public partial class StateWindow : Form
    {
        public static Person[] person = Login.person;
        public PictureBox[] picturebox = new PictureBox[6];
        int num1,num2;
        public StateWindow()
        {
            InitializeComponent();
        }

        private void StateWindow_Load(object sender, EventArgs e)
        {
            if (PK.replace == -1)
            {
                Close();
                new Fighter().Show();
            }
            else
            {
                int i;
                picturebox[1] = pictureBox1;
                picturebox[2] = pictureBox2;
                picturebox[3] = pictureBox3;
                picturebox[4] = pictureBox4;
                picturebox[5] = pictureBox5;
                label1.Text = person[1].Name;
                label2.Text = person[2].Name;
                label3.Text = person[3].Name;
                label4.Text = person[4].Name;
                label5.Text = person[5].Name;
                for (i = 1; i <= 5; i++)
                {
                    if (person[i].state == State.Alive)
                        picturebox[i].Image = Properties.Resources.Alive;
                    else
                        picturebox[i].Image = Properties.Resources.Dead;
                }
                if (PK.replace == 0 && PK.win == 0)
                {
                    label7.Text = person[1].Name;
                    label8.Text = person[2].Name;
                    num1 = 1;
                    num2 = 2;
                }
                else
                {
                    label7.Text = person[PK.win].Name;
                    label8.Text = person[PK.replace].Name;
                    num1 = PK.win;
                    num2 = PK.replace;
                }
            }
            //pictureBox1.Image = Properties.Resources.Alive;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sendStr = "message|ToPK|" + person[num1].Name + "|" + person[num2].Name;
            for (int i = 1; i <= Welcom.N; i++)
            {
                byte[] bs = Encoding.Unicode.GetBytes(sendStr);
                Welcom.temp[i].socket.Send(bs, bs.Length, 0);  //返回信息给客户端
            }

            Close();
            new PK(num1, num2).Show();
        }

 
    }
}
