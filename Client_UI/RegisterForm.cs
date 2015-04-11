using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Manager;

namespace Client_UI
{
    public partial class RegisterForm : Form
    {
        private DiginoteManager dm;

        public RegisterForm()
        {
            InitializeComponent();
            dm = new DiginoteManager();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = usernameBox.Text;
            string password = passwordBox.Text;
            string nickname = nicknameBox.Text;

            if (username == "" || password == "" || nickname == "")
            {
                warningLabel.Text = "Existem campos por preencher.";
                return;
            }

            int registerReturn = dm.Register(username, nickname, password);

            if (registerReturn == 0)
            {
                this.Hide();
                LoginForm lf = new LoginForm();
                lf.WarnSucessfullRegister(nickname);
                lf.Show();
            }
            else if(registerReturn == -1)
            {
                warningLabel.Text = "O username já existe.";
            }
            else
            {
                warningLabel.Text = "Erro da base de dados.";
            }

        }

        private void cancelLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            LoginForm lf = new LoginForm();
            lf.Show();
        }
    }
}
