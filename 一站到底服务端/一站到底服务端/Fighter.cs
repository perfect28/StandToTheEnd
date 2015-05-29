using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 一站到底服务端
{
    public partial class Fighter : Form
    {
        public Welcom welcom;
        public Fighter()
        {
            InitializeComponent();
            label1.Text = "战神 " + PK.winner_name + " 诞生！！";
        }

        private void Fighter_Load(object sender, EventArgs e)
        {

        }

        private void Fighter_FormClosed(object sender, FormClosedEventArgs e)
        {
            FileStream fs = new FileStream("order.txt", FileMode.Open);
            //获得字节数组  

            int order = Welcom.now_pro;
            byte[] data = new UTF8Encoding().GetBytes(order.ToString());
            //开始写入  
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流  
            fs.Flush();
            fs.Close(); 

            int i;
            for (i = 1; i <= Welcom.N; i++)
                Welcom.temp[i].socket.Close();
                System.Environment.Exit(0);
        }
    }
}
