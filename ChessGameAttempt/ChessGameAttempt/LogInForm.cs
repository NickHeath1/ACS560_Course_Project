using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGameAttempt
{
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
        }

        private void logInButton_Click(object sender, EventArgs e)
        {
            // Empty username and/or password
            if (usernameText.Text == "" || passwordText.Text == "")
            {
                // Empty username error
                if (usernameText.Text == "")
                {
                    error1.Show();
                }
                else
                {
                    error1.Hide();
                }

                // Empty password error
                if (passwordText.Text == "")
                {
                    error2.Show();
                }
                else
                {
                    error2.Hide();
                }
            }
            else
            {
                error1.Hide();
                error2.Hide();
                // TODO: Send a request to the server to log in
                // TODO: Show error3 if invalid username or password
                // TODO: show the interface for the lobby if correct combination
                /// MessageBox.Show("You just entered the password: " + passwordText.Text, "Message123");

                if (Verify_Login(usernameText.Text, passwordText.Text))
                {
                    Hide();
                    LobbyForm lobby = new LobbyForm(usernameText.Text);
                    lobby.ShowDialog();
                    Show();
                }
                else
                {
                    error3.Show();
                }
            }
        }

        private bool Verify_Login(string username, string password)
        {
            User user = new User(username, password);
            string json = JsonConvert.SerializeObject(user);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:2345/AuthenticateUser");
            request.Method = "POST";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(json);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            try
            {
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return true;
                    }

                    else
                    {
                        return false;
                    }
                }
            }
            catch (WebException ex)
            {
                var response = (HttpWebResponse)ex.Response;
                if (response != null)
                {
                    return false;
                }
                else
                {
                    MessageBox.Show("Error communicating with server. Please try again later.", "Server error");
                    Close();
                    return false;
                }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void createAccountButton_Click(object sender, EventArgs e)
        {
            Hide();
            CreateUserForm cuf = new CreateUserForm();
            cuf.ShowDialog();
            Show();
        }

        private void usernameText_TextChanged(object sender, EventArgs e)
        {
            error3.Hide();
        }

        private void passwordText_TextChanged(object sender, EventArgs e)
        {
            error3.Hide();
        }
    }
}
