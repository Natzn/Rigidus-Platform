using System;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Text;

namespace Rigidus.Platform
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string checksum = "";
            if (checkInternet())
            {
                // used to build entire input
                StringBuilder sb = new StringBuilder();

                // used on each read operation
                byte[] buf = new byte[8192];

                // prepare the web page we will be asking for
                HttpWebRequest request = (HttpWebRequest)
                    WebRequest.Create("http://rigidus.square7.ch/platform/updates/checksum");

                // execute the request
                HttpWebResponse response = (HttpWebResponse)
                    request.GetResponse();

                // we will read data via the response stream
                Stream resStream = response.GetResponseStream();

                string tempString = null;
                int count = 0;

                do
                {
                    // fill the buffer with data
                    count = resStream.Read(buf, 0, buf.Length);

                    // make sure we read some data
                    if (count != 0)
                    {
                        // translate from bytes to ASCII text
                        tempString = Encoding.ASCII.GetString(buf, 0, count);

                        // continue building the string
                        sb.Append(tempString);
                    }
                }
                while (count > 0); // any more data to read?
                checksum = sb.ToString();
                MD5 md5 = MD5.Create();
                string locChecksum = BitConverter.ToString(
                    md5.ComputeHash(new FileStream(
                        Application.ExecutablePath, FileMode.Open, FileAccess.Read)))
                        .Replace("-", "");
                if (locChecksum.Equals(checksum))
                {
                    Application.Run(new LoginForm());
                }
                else
                {
                    MessageBox.Show(locChecksum);
                    Clipboard.SetText(locChecksum);
                }
            }
            else
            {
                MessageBox.Show("Can't reach application server.");
            }
        }

        private static bool checkInternet()
        {
            try
            {
                using (WebClient client = new WebClient())
                using (var stream = client.OpenRead(
                    "http://rigidus.square7.ch/platform/updates/checksum"))
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
