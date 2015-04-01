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
    public partial class PurchaseOrderDialog : Form
    {
        private DiginoteManager dm;
        private MainForm mainForm;

        public int ClientId {get;set; }

        public PurchaseOrderDialog(MainForm m)
        {
            InitializeComponent();
            dm = new DiginoteManager();
            mainForm = m;
        }

        private void buyButton_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("Buy Diginotes");
            //returns the number of diginotes that weren't bought
            int missing = dm.BuyDiginotes(ClientId, (int)quantity.Value);

            if(missing == 0)
            {
                this.Hide();
                mainForm.Show();
            }
            else
            {
                //user tem de manter ou baixar a cotacao
                //alertar os outros users da alteracao da cotacao
            }
        }
    }
}
