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
            string username = usernameText.Text;
            string password = passwordText.Text;

            if (username == "" || password == "")
            {
                warningLabel.ForeColor = Color.Red;
                warningLabel.Text = "Existem campos por preencher.";
                return;
            }

            int addUserReturn = dm.Login(username, password);

            if (addUserReturn == 0)
            {
                this.Hide();
                int user = dm.getClientId(username);
                string nickname = dm.GetNickname(user);
                MainForm mf = new MainForm(user);          
                mf.Text += nickname;
                mf.Show();
            }
            else if(addUserReturn == -1)
            {
                warningLabel.ForeColor = Color.Red;
                warningLabel.Text = "O username não existe.";
            }
            else if(addUserReturn == -2)
            {
                warningLabel.ForeColor = Color.Red;
                warningLabel.Text = "Password errada.";
            }

        }

        public void WarnSucessfullRegister(string nickname)
        {
            warningLabel.ForeColor = Color.Green;
            warningLabel.Text = "Bem-vindo " + nickname + ", a tua conta foi criada com sucesso.";
        }
    }
}
