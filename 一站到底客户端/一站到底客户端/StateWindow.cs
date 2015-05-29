using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 一站到底客户端
{
    public enum State
    {
        Alive,
        Dead
    }
    public partial class StateWindow : Form
    {
        
        public static string []names=new string[6];
        public static State[] states = new State[6];
        public int gm1, gm2;

        PictureBox[] pb = new PictureBox[6];
        public StateWindow()
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
          
        private void StateWindow_Load(object sender, EventArgs e)
        {
            label1.Text = names[1];
            label2.Text = names[2];
            label3.Text = names[3];
            label4.Text = names[4];
            label5.Text = names[5];
            pb[1] = pictureBox1;
            pb[2] = pictureBox2;
            pb[3] = pictureBox3;
            pb[4] = pictureBox4;
            pb[5] = pictureBox5;
            int i;
            for(i=1;i<=5;i++)
            {
                if(states[i]==State.Alive)
                    pb[i].Image=Properties.Resources.Alive;
                else
                    pb[i].Image=Properties.Resources.Dead;
            }
            label7.Text = names[gm1];
            label8.Text = names[gm2];
        }

        private void StateWindow_Resize(object sender, EventArgs e)
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
