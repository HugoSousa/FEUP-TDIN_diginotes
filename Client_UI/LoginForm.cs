﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Manager;
using Shared;
using System.Runtime.Remoting;

namespace Client_UI
{
    public partial class LoginForm : Form
    {
        DiginoteManager dm;

        public LoginForm()
        {
            InitializeComponent();
            dm = new DiginoteManager();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm register = new RegisterForm();
            register.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (usernameText.Text == "" || passwordText.Text == "")
            {
                warningLabel.Text = "Missing Field";
                return;
            }

            int addUserReturn = dm.Login(usernameText.Text, passwordText.Text);

            if (addUserReturn == 0)
            {
                this.Hide();
                MainForm mf = new MainForm();
                mf.Show();
            }
            else if(addUserReturn == -1)
            {
                warningLabel.Text = "That username doesn't exist.";
            }
            else if(addUserReturn == -2)
            {
                warningLabel.Text = "Wrong password.";
            }

        }

    }
}
