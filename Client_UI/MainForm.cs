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
            purchasesBox.Text = dm.GetPurchases(ClientId).ToString();
            salesBox.Text = dm.GetSales(ClientId).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //returns the number of diginotes that weren't bought
            int missing = dm.BuyDiginotes(ClientId, (int)purchaseQuantity.Value);

            if (missing == 0)
            {
                this.Hide();
                MainForm.instance.Show();
            }
            else
            {
                this.Hide();
                ChangeQuotationForm cqf = new ChangeQuotationForm(true, dm.GetQuotation());
                cqf.Show();
                //user tem de manter ou aumentar a cotacao
                //alertar os outros users da alteracao da cotacao
            }
        }

        public void addDiginotes(int quantity)
        {
            diginotesBox.Text = (Int32.Parse(diginotesBox.Text) + quantity).ToString();
            notificationsBox.AppendText("Compraste " + quantity + " diginotes com sucesso.\n");
        }

        public void removeDiginotes(int quantity)
        {
            diginotesBox.Text = (Int32.Parse(diginotesBox.Text) - quantity).ToString();
            notificationsBox.AppendText("Vendeste " + quantity + " diginotes com sucesso.\n");
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

                        notificationsBox.AppendText("BLOCK 1 MINUTE - SALES\n");


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

                        purchaseTimer = new System.Windows.Forms.Timer();
                        purchaseTimer.Tick += new EventHandler(purchaseTimerTick);
                        purchaseTimer.Interval = 1000; // 1 second
                        purchaseTimer.Start();
                        
                        removePurchaseButton.Show();
                        keepPurchaseButton.Show();
                        changePurchase.Value = (decimal)newQuotation;
                        changePurchase.Minimum = (decimal)newQuotation;
                        changePurchase.Maximum = Decimal.MaxValue;
                        changePurchase.Show();

                        notificationsBox.AppendText("BLOCK 1 MINUTE - PURCHASES\n");
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
                purchaseTimerLabel.Text = "";
                purchaseTimer.Stop();
                return;
            }

            purchaseTimerLabel.Text = purchaseCounter.ToString();
        }
    }
}
