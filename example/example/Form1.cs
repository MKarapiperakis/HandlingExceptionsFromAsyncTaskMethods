using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace example
{
    public partial class HandlingExceptions : Form
    {
        public int a = 10, b = 2;
        public int result;
        public HandlingExceptions()
        {
            
            InitializeComponent();
        }
        static void BigTask()
        {
            Thread.Sleep(1500);
        }

        static void SmallTask()
        {
            Thread.Sleep(200);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button4.Visible = false;
            await Task.Run(new Action(BigTask));

            try
            {
                int result =  await Task<int>.Run(() => { return Division(a,b); });
                label1.Text = "a/b = " + Convert.ToString(result);
                button4.Visible = true;
            }
            catch (DivideByZeroException)
            {
                label1.Text = "ERROR!!!\nDivided by zero!!!";
                button4.Visible = true;
            }
        }

        public int Division(int a,int b)
        {
            result = a / b;
            return result;
        }
        


        private async void button3_Click(object sender, EventArgs e)
        {
            
            await Task.Run(new Action(SmallTask));
            b++;
            label3.Text = "b = " + Convert.ToString(b);
        }

        private async void button2_Click(object sender, EventArgs e)
        {

            await Task.Run(new Action(SmallTask));
            b--;
            label3.Text = "b = " + Convert.ToString(b);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            b = 2;
            label2.Text = "a = " + Convert.ToString(a);
            label3.Text = "b = " + Convert.ToString(b);
            label1.Text = "a/b = ";
            button4.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = "a = " + Convert.ToString(a);
            label3.Text = "b = " + Convert.ToString(b);
        }

       

       

    }
}
