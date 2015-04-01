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

        public MainForm()
        {
            InitializeComponent();
            dm = new DiginoteManager();
            UpdateQuotation();
            FullSaleOrderRepeater repeater = new FullSaleOrderRepeater();
            SaleAlarm sa = new SaleAlarm();
            repeater.fullSaleOrder += sa.WarnSale;
            dm.saleOrder += repeater.Repeater;
        }

        private void UpdateQuotation()
        {
            quotationBox.Text = dm.GetQuotation().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            PurchaseOrderDialog pod = new PurchaseOrderDialog();
            pod.ClientId = ClientId;
            pod.Show();
        }

    }

    class SaleAlarm : MarshalByRefObject
    {
        public void WarnSale(FullSaleOrderArgs param)
        {
            Console.WriteLine(param.Buyer + " " + param.Seller);
        }
    }
}
