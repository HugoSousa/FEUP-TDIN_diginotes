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

namespace Client_UI
{
    public partial class MainForm : Form
    {
        DiginoteManager dm;
        public int ClientId { get; set; }

        public MainForm(int clientId)
        {
            InitializeComponent();
            dm = new DiginoteManager();
            ClientId = clientId;
            UpdateQuotation();
            UpdateDiginotes();            
            FullSaleOrderRepeater repeater = new FullSaleOrderRepeater();
            SaleAlarm sa = new SaleAlarm(this, ClientId);
            repeater.fullSaleOrder += sa.WarnSale;
            dm.saleOrder += repeater.Repeater;
        }

        private void UpdateQuotation()
        {
            quotationBox.Text = dm.GetQuotation().ToString();
        }

        private void UpdateDiginotes()
        {
            diginotesBox.Text = dm.GetDiginotes(ClientId).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            PurchaseOrderDialog pod = new PurchaseOrderDialog(this);
            pod.ClientId = ClientId;
            pod.Show();
        }

        public void addDiginotes(int quantity)
        {
            diginotesBox.Text = (Int32.Parse(diginotesBox.Text) + quantity).ToString();
        }

        public void removeDiginotes(int quantity)
        {
            diginotesBox.Text = (Int32.Parse(diginotesBox.Text) - quantity).ToString();
        }

    }

    class SaleAlarm : MarshalByRefObject
    {
        private MainForm mainForm;

        public SaleAlarm(MainForm mainForm, int clientId)
        {
            this.mainForm = mainForm;
        }

        public void WarnSale(FullSaleOrderArgs param)
        {
            Console.WriteLine("BUYER: " + param.Buyer + " \tSELLER: " + param.Seller);
            /*
            if (mainForm.ClientId == param.Buyer)
                mainForm.addDiginotes(param.Quantity);
            else if (mainForm.ClientId == param.Seller)
                mainForm.removeDiginotes(param.Quantity);
            */
        }
    }
}
