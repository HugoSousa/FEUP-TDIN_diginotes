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
        private Timer salesTimer, purchaseTimer;
        private int salesCounter, purchaseCounter;

        public static MainForm instance;
        DiginoteManager dm;
        public delegate void ChangeDiginotesDelegate(int param);
        public delegate void ChangeQuotationDelegate(double oldQuotation, double newQuotation, int changer);
        
        public int ClientId { get; set; }

        public MainForm(int clientId)
        {
            InitializeComponent();
            dm = new DiginoteManager();
            ClientId = clientId;
            UpdateQuotation();
            UpdateDiginotes();
            UpdatePurchasesAndSales();
            SaleOrderRepeater salesRepeater = new SaleOrderRepeater();
            ChangeQuotationRepeater quotationRepeater = new ChangeQuotationRepeater();
            //SaleAlarm sa = new SaleAlarm(this, ClientId);
            salesRepeater.fullSaleOrder += new SaleOrderHandler(ChangeDiginotes);
            dm.saleOrder += new SaleOrderHandler(salesRepeater.Repeater);

            quotationRepeater.changeQuotation += new ChangeQuotationHandler(UpdateQuotation);
            dm.changeQuotation += new ChangeQuotationHandler(quotationRepeater.Repeater);

            purchaseTimer = new System.Windows.Forms.Timer();
            purchaseTimer.Tick += new EventHandler(purchaseTimerTick);
            purchaseTimer.Interval = 1000; // 1 second

            instance = this;
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void ChangeDiginotes(SaleOrderArgs param)
        {
            Console.WriteLine("BUYER: " + param.Buyer + " \tSELLER: " + param.Seller);

            ChangeDiginotesDelegate cdd;

            if (ClientId == param.Buyer)
            {
                cdd = new ChangeDiginotesDelegate(addDiginotes);
                Invoke(cdd, new object[] { param.Quantity });
            }
            else if (ClientId == param.Seller)
            {
                cdd = new ChangeDiginotesDelegate(removeDiginotes);
                Invoke(cdd, new object[] { param.Quantity });
            }
        }

        public void UpdateQuotation(ChangeQuotationArgs param)
        {
            ChangeQuotationDelegate cqd;
            cqd = new ChangeQuotationDelegate(changeQuotation);
            Invoke(cqd, new object[] { param.OldQuotation, param.NewQuotation, param.Changer });
        }

        private void UpdateQuotation()
        {
            quotationBox.Text = dm.GetQuotation().ToString();
        }

        private void UpdateDiginotes()
        {
            diginotesBox.Text = dm.GetDiginotes(ClientId).ToString();
        }

        public void UpdatePurchasesAndSales()
        {
            int purchases = dm.GetPurchases(ClientId);
            int sales = dm.GetSales(ClientId);

            if(purchases > 0)
            {
                changePurchase.Show();
                keepPurchaseButton.Show();
            }

            if(sales > 0)
            {
                changeSales.Show();
                keepSalesButton.Show();
            }

            purchasesBox.Text = purchases.ToString();
            salesBox.Text = sales.ToString();

            decimal quotation = (decimal)dm.GetQuotation();
            changePurchase.Value = quotation;
            changePurchase.Maximum = Decimal.MaxValue;
            changePurchase.Minimum = quotation;

            changeSales.Value = quotation;
            changeSales.Maximum = quotation;
            changeSales.Minimum = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //returns the number of diginotes that weren't bought
            int missing = dm.BuyDiginotes(ClientId, (int)purchaseQuantity.Value);

           /* if (missing > 0)
            {
                this.Hide();
                ChangeQuotationForm cqf = new ChangeQuotationForm(true, dm.GetQuotation());
                cqf.Show();
                //user tem de manter ou aumentar a cotacao
                //alertar os outros users da alteracao da cotacao
            }*/

            purchasesBox.Text = purchaseQuantity.Value.ToString();
            purchaseQuantity.Value = 0;
            UpdatePurchasesAndSales();
        }

        public void addDiginotes(int quantity)
        {
            //diginotesBox.Text = (Int32.Parse(diginotesBox.Text) + quantity).ToString();
            UpdateDiginotes();
            notificationsBox.AppendText("Compraste " + quantity + " diginotes com sucesso.\n");
            UpdatePurchasesAndSales();
        }

        public void removeDiginotes(int quantity)
        {
            //diginotesBox.Text = (Int32.Parse(diginotesBox.Text) - quantity).ToString();
            UpdateDiginotes();
            notificationsBox.AppendText("Vendeste " + quantity + " diginotes com sucesso.\n");
            UpdatePurchasesAndSales();
        }

        public void changeQuotation(double oldQuotation, double newQuotation, int changer)
        {
            quotationBox.Text = newQuotation.ToString();

            if (oldQuotation != newQuotation)
                notificationsBox.AppendText("A cotação atual foi alterada de " + oldQuotation + " para " + newQuotation + ".\n");

            if (MainForm.instance.ClientId != changer)
            {
                if (oldQuotation > newQuotation)
                {
                    if (dm.HasSales(ClientId))
                    {
                        //bloquear 1 minuto e esperar confirmacao
                        salesCounter = 60;

                        salesTimer = new System.Windows.Forms.Timer();
                        salesTimer.Tick += new EventHandler(salesTimerTick);
                        salesTimer.Interval = 1000; // 1 second
                        salesTimer.Start();

                        notificationsBox.AppendText("Tem 1 minuto para decidir se aceita a nova cotação.\n");


                        removeSalesButton.Show();
                        keepSalesButton.Show();
                        changeSales.Value = (decimal)newQuotation;
                        changePurchase.Minimum = 0;
                        changePurchase.Maximum = (decimal)newQuotation;
                        changeSales.Show();
                    }
                }
                else if (oldQuotation < newQuotation)
                {
                    if (dm.HasPurchases(ClientId))
                    {
                        purchaseCounter = 60;

                        dm.SetPurchaseBusy(ClientId, true);

                        purchaseTimer.Start();
                        addPurchase.Enabled = false;
                        
                        removePurchaseButton.Show();
                        keepPurchaseButton.Show();
                        changePurchase.Value = (decimal)newQuotation;
                        changePurchase.Minimum = (decimal)newQuotation;
                        changePurchase.Maximum = Decimal.MaxValue;
                        changePurchase.Show();

                        notificationsBox.AppendText("Tem 1 minuto para decidir se aceita a nova cotação.\n");
                    }
                }
            }
        }

        private void salesTimerTick(object sender, EventArgs e)
        {
            salesCounter--;
            if (salesCounter == 0)
            {
                salesTimerLabel.Text = "";
                hideSalesInfo();
                salesTimer.Stop();
                return;
            }

            salesTimerLabel.Text = salesCounter.ToString();
        }

        private void purchaseTimerTick(object sender, EventArgs e)
        {
            purchaseCounter--;
            if (purchaseCounter == 0)
            {
                hidePurchaseInfo();
                purchaseTimer.Stop();
                dm.SetPurchaseBusy(ClientId, false);
                return;
            }

            purchaseTimerLabel.Text = purchaseCounter.ToString();
        }

        private void removePurchaseButton_Click(object sender, EventArgs e)
        {
            dm.DeletePurchase(ClientId);
            hidePurchaseInfo();
            purchasesBox.Text = "0";
            purchaseTimer.Stop();
            dm.SetPurchaseBusy(ClientId, false);
            addPurchase.Enabled = true;
        }

        private void hidePurchaseInfo()
        {
            purchaseTimerLabel.Text = "";
            removePurchaseButton.Hide();
            //keepPurchaseButton.Hide();
            //changePurchase.Hide();
        }

        private void hideSalesInfo()
        {
            salesTimerLabel.Text = "";
            removeSalesButton.Hide();
            keepSalesButton.Hide();
            changeSales.Hide();
        }

        private void keepPurchaseButton_Click(object sender, EventArgs e)
        {
            dm.ChangeQuotation((double)changePurchase.Minimum, (double)changePurchase.Value, ClientId);
            addPurchase.Enabled = true;
            hidePurchaseInfo();
            purchaseTimer.Stop();
            dm.SetPurchaseBusy(ClientId, false);
        }

        private void addSale_Click(object sender, EventArgs e)
        {
            //returns the number of diginotes that weren't bought
            int missing = dm.SellDiginotes(ClientId, (int)saleQuantity.Value);

            if (missing > 0)
            {
                this.Hide();
                ChangeQuotationForm cqf = new ChangeQuotationForm(true, dm.GetQuotation());
                cqf.Show();
                //user tem de manter ou aumentar a cotacao
                //alertar os outros users da alteracao da cotacao
            }

            //purchasesBox.Text = purchaseQuantity.Value.ToString();
            //purchaseQuantity.Value = 0;
        }
    }
}
