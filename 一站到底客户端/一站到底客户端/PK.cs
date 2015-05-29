using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 一站到底客户端
{
    public partial class PK : Form
    {
        public string gm1_name;
        public string gm2_name;
        public int time;
        public int turn;
        static public List<Problem> problem = Welcom.problem;
        public TextBox textbox;
        public RichTextBox richtextbox;
        public Label []label=new Label[6];
        public PK()
        {
            InitializeComponent();
            Hashtable ht = new Hashtable();//使用哈希表存放位置以及控件在容器中的位置比例
            ht.Add("width", Size.Width);
            ht.Add("height", Size.Height);
            foreach (Control ctrl in this.Controls)
            {
                ht.Add(ctrl.Name + "X", ctrl.Location.X / (double)Size.Width);//存储控件在容器中的相对比例
                ht.Add(ctrl.Name + "Y", ctrl.Location.Y / (double)Size.Height);//存储控件在容器中的相对比例
                ctrl.Tag = ctrl.Size;
            }
            Tag = ht;
        }

        private void PK_Load(object sender, EventArgs e)
        {
            label1.Text = gm1_name;
            label2.Text = gm2_name;
            label[1] = label1;
            label[2] = label2;
            label[3] = label5;
            label[4] = label6;
            label[5] = label7;
            textbox = textBox1;
            richtextbox = richTextBox1;

            label5.Visible = false;
            label6.Visible = false;

            label8.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time-- == 0)
            {
                timer1.Stop();
                //MessageBox.Show("不好意思，时间到了~");
                label8.Text = "不好意思，时间到了~";
                //timer2.Start();
            }
            if(time>=0)
            if (turn == 0)
            {
                label5.Text = "0:" + time.ToString();
            }
            else
            {
                label6.Text = "0:" + time.ToString();
            }
        }

        private void PK_Resize(object sender, EventArgs e)
        {
            Hashtable scale = (Hashtable)Tag;
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Left = (int)(Size.Width * (double)scale[ctrl.Name + "X"]);
                ctrl.Top = (int)(Size.Height * (double)scale[ctrl.Name + "Y"]);
                ctrl.Width = (int)(Size.Width * 1.0f / (int)scale["width"] * ((Size)ctrl.Tag).Width);
                ctrl.Height = (int)(Size.Height * 1.0f / (int)scale["height"] * ((Size)ctrl.Tag).Height);
                //每次使用的都是最初始的控件大小，保证准确无误。
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }
    }
}
