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
            PurchaseOrderRepeater purchaseRepeater = new PurchaseOrderRepeater();
            ChangeQuotationRepeater quotationRepeater = new ChangeQuotationRepeater();
            //SaleAlarm sa = new SaleAlarm(this, ClientId);
            salesRepeater.fullSaleOrder += new SaleOrderHandler(ChangeDiginotes);
            dm.saleOrder += new SaleOrderHandler(salesRepeater.Repeater);

            purchaseRepeater.fullPurchaseOrder += new PurchaseOrderHandler(ChangeDiginotes);
            dm.purchaseOrder += new PurchaseOrderHandler(purchaseRepeater.Repeater);

            quotationRepeater.changeQuotation += new ChangeQuotationHandler(UpdateQuotation);
            dm.changeQuotation += new ChangeQuotationHandler(quotationRepeater.Repeater);

            purchaseTimer = new System.Windows.Forms.Timer();
            purchaseTimer.Tick += new EventHandler(purchaseTimerTick);
            purchaseTimer.Interval = 1000; // 1 second

            salesTimer = new System.Windows.Forms.Timer();
            salesTimer.Tick += new EventHandler(salesTimerTick);
            salesTimer.Interval = 1000; // 1 second

            instance = this;
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void ChangeDiginotes(SaleOrderArgs param)
        {
            //Console.WriteLine("BUYER: " + param.Buyer + " \tSELLER: " + param.Seller);

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

        public void ChangeDiginotes(PurchaseOrderArgs param)
        {
            //Console.WriteLine("SELLER: " + param.Seller + "BUYER: " + param.Buyer + " \t");

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
            int myDiginotes = (int)dm.GetDiginotes(ClientId);
            diginotesBox.Text = myDiginotes.ToString();
            saleQuantity.Value = 0;
            saleQuantity.Maximum = (decimal)myDiginotes;
            UpdateDiginoteSerials();
        }

        private void UpdateDiginoteSerials()
        {
            serialsBox.Items.Clear();
            List<string> serials = dm.GetDiginoteSerials(ClientId);
            for (int i = 0; i < serials.Count; i++)
            {
                serialsBox.Items.Add(serials.ElementAt(i));
            }
        }

        public void UpdatePurchasesAndSales()
        {
            int purchases = dm.GetPurchases(ClientId);
            int sales = dm.GetSales(ClientId);

            if (purchases > 0)
            {
                DateTime purchaseTime = dm.GetPurchaseTime(ClientId);
                timePurchaseOrder.Text = purchaseTime.AddHours(1).ToString();
                changePurchase.Show();
                keepPurchaseButton.Show();
            }
            else
            {
                changePurchase.Hide();
                keepPurchaseButton.Hide();
            }

            if (sales > 0)
            {
                DateTime salesTime = dm.GetSalesTime(ClientId);
                timeSaleOrder.Text = salesTime.AddHours(1).ToString();
                changeSales.Show();
                keepSalesButton.Show();
            }
            else
            {
                changeSales.Hide();
                keepSalesButton.Hide();
            }

            purchasesBox.Text = purchases.ToString();
            salesBox.Text = sales.ToString();

            decimal quotation = (decimal)dm.GetQuotation();
            changePurchase.Maximum = Decimal.MaxValue;
            changePurchase.Minimum = quotation;
            changePurchase.Value = quotation;

            changeSales.Maximum = quotation;
            changeSales.Minimum = 0;
            changeSales.Value = quotation;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //returns the number of diginotes that weren't bought
            int missing = dm.BuyDiginotes(ClientId, (int)purchaseQuantity.Value);

            if (missing > 0)
            {
                timePurchaseOrder.Text = DateTime.Now.ToString();
            }
            else
            {
                //update purchases to 0
                timePurchaseOrder.Text = "";
            }

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
            notificationsBox.AppendText("Vendeste " + quantity + " diginotes com sucesso.\n");
            UpdatePurchasesAndSales();
            UpdateDiginotes();
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

                        dm.SetSalesBusy(ClientId, true);

                        salesTimer.Start();
                        addSale.Enabled = false;

                        removeSalesButton.Show();
                        keepSalesButton.Show();
                        changeSales.Minimum = 0;
                        changeSales.Maximum = (decimal)newQuotation;
                        changeSales.Value = (decimal)newQuotation;
                        changeSales.Show();

                        notificationsBox.AppendText("Tem 1 minuto para decidir se aceita a nova cotação.\n");
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
                        changePurchase.Minimum = (decimal)newQuotation;
                        changePurchase.Maximum = Decimal.MaxValue;
                        changePurchase.Value = (decimal)newQuotation;
                        changePurchase.Show();

                        notificationsBox.AppendText("Tem 1 minuto para decidir se aceita a nova cotação.\n");
                    }
                }
            }
            else
            {

                if (dm.HasSales(ClientId))
                {
                    changeSales.Minimum = 0;
                    changeSales.Maximum = (decimal)newQuotation;
                    changeSales.Value = (decimal)newQuotation;
                }

                if (dm.HasPurchases(ClientId))
                {
                    changePurchase.Minimum = (decimal)newQuotation;
                    changePurchase.Maximum = Decimal.MaxValue;
                    changePurchase.Value = (decimal)newQuotation;
                }
            }
        }

        private void salesTimerTick(object sender, EventArgs e)
        {
            salesCounter--;
            if (salesCounter == 0)
            {
                hideSalesInfo(false);
                salesTimer.Stop();
                dm.SetSalesBusy(ClientId, false);
                return;
            }

            salesTimerLabel.Text = salesCounter.ToString();
        }

        private void purchaseTimerTick(object sender, EventArgs e)
        {
            purchaseCounter--;
            if (purchaseCounter == 0)
            {
                hidePurchaseInfo(false);
                purchaseTimer.Stop();
                dm.SetPurchaseBusy(ClientId, false);
                return;
            }

            purchaseTimerLabel.Text = purchaseCounter.ToString();
        }

        private void removePurchaseButton_Click(object sender, EventArgs e)
        {
            dm.DeletePurchase(ClientId);
            hidePurchaseInfo(true);
            purchasesBox.Text = "0";
            purchaseTimer.Stop();
            dm.SetPurchaseBusy(ClientId, false);
            addPurchase.Enabled = true;
        }

        private void hidePurchaseInfo(bool hide_all)
        {
            purchaseTimerLabel.Text = "";
            removePurchaseButton.Hide();
            if (hide_all)
            {
                keepPurchaseButton.Hide();
                changePurchase.Hide();
            }
        }

        private void hideSalesInfo(bool hide_all)
        {
            salesTimerLabel.Text = "";
            removeSalesButton.Hide();
            if (hide_all)
            {
                keepSalesButton.Hide();
                changeSales.Hide();
            }
        }

        private void keepPurchaseButton_Click(object sender, EventArgs e)
        {
            double oldQuotation = (double)changePurchase.Minimum;
            double newQuotation = (double)changePurchase.Value;
            if (oldQuotation <= newQuotation)
            {
                if (oldQuotation < newQuotation)
                    dm.ChangeQuotation(oldQuotation, newQuotation, ClientId);
                changePurchase.Minimum = (decimal)newQuotation;
                addPurchase.Enabled = true;
                hidePurchaseInfo(false);
                purchaseTimer.Stop();
                dm.SetPurchaseBusy(ClientId, false);
            }
        }

        private void keepSalesButton_Click(object sender, EventArgs e)
        {
            double oldQuotation = (double)changeSales.Maximum;
            double newQuotation = (double)changeSales.Value;

            if (oldQuotation >= newQuotation)
            {
                if (oldQuotation > newQuotation)
                    dm.ChangeQuotation(oldQuotation, newQuotation, ClientId);
                changeSales.Maximum = (decimal)newQuotation;
                addSale.Enabled = true;
                hideSalesInfo(false);
                salesTimer.Stop();
                dm.SetSalesBusy(ClientId, false);
            }
        }

        private void addSale_Click(object sender, EventArgs e)
        {
            int quantityToSale = (int)saleQuantity.Value;

            if (quantityToSale <= Int32.Parse(diginotesBox.Text))
            {
                int missing = dm.SellDiginotes(ClientId, quantityToSale);

                if (missing > 0)
                    timeSaleOrder.Text = DateTime.Now.ToString();
                else
                    timeSaleOrder.Text = "";

                salesBox.Text = saleQuantity.Value.ToString();
                saleQuantity.Value = 0;
                UpdatePurchasesAndSales();
            }
            else
            {
                notificationsBox.AppendText("Não podes vender aquilo que não tens. Diminui a quantidade!\n");
            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if(b.Text.Equals("+")){
                b.Text = "-";
                serialsBox.Visible = true;
                this.Height = 660;
            }
            else{
                b.Text = "+";
                serialsBox.Visible = false;
                this.Height = 503;
            }
        }

    }
}
