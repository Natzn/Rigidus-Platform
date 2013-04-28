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
        private void button_Click(object sender, EventArgs e)
        {
            Button comp = (Button)sender;
            if (comp.Name == "btnLogin")
            {
                if (txtUsername.Text == "" && txtPass.Text == "")
                {
                    MessageBox.Show("No username or password!");
                }
                else
                {
                    if (checkInternet())
                    {
                        new MainForm().ShowDialog();
                    }
                }
            }
            else if (comp.Name == "btnExit")
            {
                Application.Exit();
            }
        }

        private bool checkInternet()
        {
            try
            {
                using (WebClient client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
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
