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
    public partial class ChangeQuotationForm : Form
    {
        private bool isIncrement;
        private double actualQuotation;
        private DiginoteManager dm;

        public ChangeQuotationForm(bool isIncrement, double actualQuotation)
        {
            InitializeComponent();
            this.isIncrement = isIncrement;
            this.actualQuotation = actualQuotation;
            dm = new DiginoteManager();

            if (isIncrement)
            {
                infoBox.Text = "Não há oferta suficiente. Tem de aumentar ou manter a cotação atual.";
                newQuotation.Minimum = (decimal)actualQuotation;
                newQuotation.Maximum = decimal.MaxValue;
            }
            else
            {
                infoBox.Text = "Não há procura suficiente. Tem de diminuir ou manter a cotação atual.";
                newQuotation.Maximum = (decimal)actualQuotation;
                newQuotation.Minimum = 0;
            }

            newQuotation.Value = (decimal)actualQuotation;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double oldQuotation = actualQuotation;

            dm.ChangeQuotation(oldQuotation, (double)newQuotation.Value, MainForm.instance.ClientId);
            this.Hide();
            MainForm.instance.Show();

        }
    }
}
