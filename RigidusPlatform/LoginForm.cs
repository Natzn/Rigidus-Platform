using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace Rigidus.Platform
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("No username or password!");
            }
            if (checkInternet())
            {
                new MainForm().ShowDialog();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private bool checkInternet()
        {
            try
            {
                using (WebClient Client = new WebClient())
                using (var stream = Client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
