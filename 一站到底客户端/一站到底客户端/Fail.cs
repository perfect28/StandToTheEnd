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
using System.Collections;

namespace 一站到底客户端
{
    public partial class Fail : Form
    {
        public Fail()
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

        private void Fail_Load(object sender, EventArgs e)
        {
            SoundPlayer sp = new SoundPlayer(@"fail.wav");
            sp.Play();
        }

        private void Fail_Resize(object sender, EventArgs e)
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
    }
}
