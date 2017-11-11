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
    public partial class CreateUserForm : Form
    {
        bool isUsernameValid = true;//false;
        bool doPasswordsMatch = false;

        public CreateUserForm()
        {
            InitializeComponent();
        }

        private void usernameText_TextChanged(object sender, EventArgs e)
        {
            if (usernameText.Text == "")
            {
                isUsernameValid = false;
                usernameAvailableLabel.Hide();
                usernameTakenLabel.Hide();
                noUsernameErrorLabel.Show();
            }

            // Compare to the usernames stored in the database
            else
            {
                noUsernameErrorLabel.Hide();

                if (!IsUserInDb(usernameText.Text))
                {
                    isUsernameValid = true;
                    usernameTakenLabel.Hide();
                    usernameAvailableLabel.Show();
                }
                else
                {
                    isUsernameValid = false;
                    usernameTakenLabel.Show();
                    usernameAvailableLabel.Hide();
                }
            }

            createButton.Enabled = (isUsernameValid && doPasswordsMatch);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void passwordText_TextChanged(object sender, EventArgs e)
        {
            if (passwordText.Text == "")
            {
                passwordRequiredErrorLabel.Show();
                doPasswordsMatch = false;
            }

            else
            {
                passwordRequiredErrorLabel.Hide();
                if (passwordText.Text != confirmPasswordText.Text)
                {
                    passwordNoMatchLabel.Show();
                    doPasswordsMatch = false;
                }

                else
                {
                    passwordNoMatchLabel.Hide();
                    doPasswordsMatch = true;
                }
            }

            createButton.Enabled = (isUsernameValid && doPasswordsMatch);
        }

        private void confirmPasswordText_TextChanged(object sender, EventArgs e)
        {
            if (confirmPasswordText.Text != passwordText.Text)
            {
                passwordNoMatchLabel.Show();
                doPasswordsMatch = false;
            }

            else
            {
                passwordNoMatchLabel.Hide();
                doPasswordsMatch = true;
            }

            createButton.Enabled = (isUsernameValid && doPasswordsMatch);
        }

        // Add user request
        bool AddUser(string jsonContent)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:2345/AddUser");
            request.Method = "POST";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(jsonContent);

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
                        MessageBox.Show("Error creating user. Server may be down.", "Server error");
                        return false;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error creating user. Server may be down.", "Server error");
                return false;
            }
        }

        bool IsUserInDb(string jsonContent)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:2345/CheckUserAvailability/" + jsonContent);
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return false;
                    }

                    else
                    {
                        MessageBox.Show("Unable to determine username availability.\nServer may be down.", "Username not available");
                        return true;
                    }
                }
            }
            catch (WebException ex)
            {
                var response = (HttpWebResponse)ex.Response;
                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.Conflict)
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error creating user. Server may be down.", "Server error");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Error communicating with server. Please try again later.", "Server error");
                    Close();
                    return false;
                }
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            User user = new User(usernameText.Text, passwordText.Text);
            string json = JsonConvert.SerializeObject(user);

            if(AddUser(json))
            {
                Close();
            }
        }
    }
}
