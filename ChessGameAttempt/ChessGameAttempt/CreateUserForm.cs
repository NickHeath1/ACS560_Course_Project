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

                if (IsUserInDb(new User(usernameText.Text, "")))
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
        bool AddUser(User user)
        {
            bool success = DataApiController<User>.PostData(ChessUtils.IPAddressWithPort + "AddUser", user);
            if (!success)
            {
                MessageBox.Show("Error while adding user to database.", "Error");
            }

            return success;
        }

        bool IsUserInDb(User user)
        {
            bool success = DataApiController<User>.PostData(ChessUtils.IPAddressWithPort + "CheckUserAvailability/" + user.Username, null);
            return success;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            User user = new User(usernameText.Text, passwordText.Text);
            if(AddUser(user))
            {
                Close();
            }
        }
    }
}
