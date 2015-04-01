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

        public int ClientId {get;set; }

        public PurchaseOrderDialog()
        {
            InitializeComponent();
            dm = new DiginoteManager();
        }

        private void buyButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Buy Diginotes");
            dm.BuyDiginotes(ClientId, (int)quantity.Value);
        }
    }
}
