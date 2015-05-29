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
    public partial class Login : Form
    {
        public static Person[] person = new Person[6];
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            int i;
            for (i = 1; i <= 5; i++)
                person[i] = new Person();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("尚有信息没有填写完毕！");
            }
            else
            {
                person[1].Name = textBox1.Text;
                person[2].Name = textBox2.Text;
                person[3].Name = textBox3.Text;
                person[4].Name = textBox4.Text;
                person[5].Name = textBox5.Text;
                int i;
                for (i = 1; i <= 5; i++)
                {
                    person[i].Position = i;
                    person[i].state = State.Alive;
                }
                string sendStr = "message|Tostatewindow|" + textBox1.Text + "|" + textBox2.Text + "|" + textBox3.Text + "|" + textBox4.Text + "|" + textBox5.Text + "|" + "1|1|1|1|1|1|2";
                for (i = 1; i <= Welcom.N; i++)
                {
                    byte[] bs = Encoding.Unicode.GetBytes(sendStr);
                    Welcom.temp[i].socket.Send(bs, bs.Length, 0);  //返回信息给客户端
                }

                Close();
                new StateWindow().Show();

            }
            
        }
    }
}
