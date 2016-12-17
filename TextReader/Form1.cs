using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TextReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = textBox2.Text;
            var stream = new StreamReader(filePath, Encoding.UTF8);
            int i = 0;
            while (i++<50)
            {
                var str = stream.ReadLine();
                //if(str.Contains("8DF69"))
                  textBox1.AppendText(str);

            }
        }
    }
}
