using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Web;

namespace VirtualNoticeBoard
{
    public partial class Form2 : Form
    {
        private static string email;

        public Form2(string user)
        {
            InitializeComponent();
            email = user;
            MessageBox.Show("Logged In As: " + user);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Please enter all details!");
                return;
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost/vnb/desktop/action.php");
            request.Method = "POST";

            String formContent = "subject=" + textBox1.Text +
                "&message=" + textBox2.Text + "&admin=" + email + "&year=" + comboBox1.Text;

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
            MessageBox.Show("Notice has been posted.");
            reader.Close();
            dataStream.Close();
            response.Close();
        }
    }
}
