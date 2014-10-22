using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.IO;

namespace VirtualNoticeBoard
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String url = "http://localhost/vnb/desktop/login.php";
            //String url = "http://ieeedtu.com/sagnik/vnb/desktop/login.php";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            String formContent = "email=" + textBox1.Text +
                "&password=" + textBox2.Text;

            byte[] byteArray = Encoding.UTF8.GetBytes(formContent);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = HttpUtility.UrlDecode(reader.ReadToEnd());
            //You may need HttpUtility.HtmlDecode depending on the response
            if (responseFromServer.Equals("A"))
            {
                Form2 form = new Form2(textBox1.Text);
                form.StartPosition = FormStartPosition.WindowsDefaultLocation;
                this.Hide();
                form.ShowDialog();
                this.Close();
            }
            else
                MessageBox.Show("Incorrect Email ID/Password");

            reader.Close();
            dataStream.Close();
            response.Close();
        }
    }
}
