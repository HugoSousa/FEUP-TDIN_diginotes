namespace Client_UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.quotationBox = new System.Windows.Forms.TextBox();
            this.addPurchase = new System.Windows.Forms.Button();
            this.addSale = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.diginotesBox = new System.Windows.Forms.TextBox();
            this.notificationsBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.removePurchaseButton = new System.Windows.Forms.Button();
            this.keepPurchaseButton = new System.Windows.Forms.Button();
            this.changePurchase = new System.Windows.Forms.NumericUpDown();
            this.purchasesBox = new System.Windows.Forms.TextBox();
            this.salesBox = new System.Windows.Forms.TextBox();
            this.changeSales = new System.Windows.Forms.NumericUpDown();
            this.keepSalesButton = new System.Windows.Forms.Button();
            this.removeSalesButton = new System.Windows.Forms.Button();
            this.purchaseGroupBox = new System.Windows.Forms.GroupBox();
            this.purchaseTimerLabel = new System.Windows.Forms.Label();
            this.salesGroupBox = new System.Windows.Forms.GroupBox();
            this.salesTimerLabel = new System.Windows.Forms.Label();
            this.purchaseQuantity = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.saleQuantity = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.changePurchase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.changeSales)).BeginInit();
            this.purchaseGroupBox.SuspendLayout();
            this.salesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseQuantity)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saleQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(309, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cotação";
            // 
            // quotationBox
            // 
            this.quotationBox.Enabled = false;
            this.quotationBox.Location = new System.Drawing.Point(303, 84);
            this.quotationBox.Name = "quotationBox";
            this.quotationBox.Size = new System.Drawing.Size(133, 22);
            this.quotationBox.TabIndex = 1;
            this.quotationBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // addPurchase
            // 
            this.addPurchase.Location = new System.Drawing.Point(84, 238);
            this.addPurchase.Name = "addPurchase";
            this.addPurchase.Size = new System.Drawing.Size(133, 61);
            this.addPurchase.TabIndex = 2;
            this.addPurchase.Text = "Adicionar Ordem de Compra";
            this.addPurchase.UseVisualStyleBackColor = true;
            this.addPurchase.Click += new System.EventHandler(this.button1_Click);
            // 
            // addSale
            // 
            this.addSale.Location = new System.Drawing.Point(303, 238);
            this.addSale.Name = "addSale";
            this.addSale.Size = new System.Drawing.Size(133, 61);
            this.addSale.TabIndex = 3;
            this.addSale.Text = "Adicionar Ordem de Venda";
            this.addSale.UseVisualStyleBackColor = true;
            this.addSale.Click += new System.EventHandler(this.addSale_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(66, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "My Diginotes";
            // 
            // diginotesBox
            // 
            this.diginotesBox.Enabled = false;
            this.diginotesBox.Location = new System.Drawing.Point(84, 84);
            this.diginotesBox.Name = "diginotesBox";
            this.diginotesBox.Size = new System.Drawing.Size(133, 22);
            this.diginotesBox.TabIndex = 5;
            this.diginotesBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // notificationsBox
            // 
            this.notificationsBox.Enabled = false;
            this.notificationsBox.ForeColor = System.Drawing.Color.Red;
            this.notificationsBox.Location = new System.Drawing.Point(84, 365);
            this.notificationsBox.Multiline = true;
            this.notificationsBox.Name = "notificationsBox";
            this.notificationsBox.Size = new System.Drawing.Size(352, 43);
            this.notificationsBox.TabIndex = 6;
            this.notificationsBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 345);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Notifications";
            // 
            // removePurchaseButton
            // 
            this.removePurchaseButton.Location = new System.Drawing.Point(561, 99);
            this.removePurchaseButton.Name = "removePurchaseButton";
            this.removePurchaseButton.Size = new System.Drawing.Size(104, 86);
            this.removePurchaseButton.TabIndex = 9;
            this.removePurchaseButton.Text = "Remover Ordem de Compra";
            this.removePurchaseButton.UseVisualStyleBackColor = true;
            this.removePurchaseButton.Visible = false;
            this.removePurchaseButton.Click += new System.EventHandler(this.removePurchaseButton_Click);
            // 
            // keepPurchaseButton
            // 
            this.keepPurchaseButton.Location = new System.Drawing.Point(192, 119);
            this.keepPurchaseButton.Name = "keepPurchaseButton";
            this.keepPurchaseButton.Size = new System.Drawing.Size(107, 45);
            this.keepPurchaseButton.TabIndex = 10;
            this.keepPurchaseButton.Text = "Alterar/Manter Cotação";
            this.keepPurchaseButton.UseVisualStyleBackColor = true;
            this.keepPurchaseButton.Visible = false;
            this.keepPurchaseButton.Click += new System.EventHandler(this.keepPurchaseButton_Click);
            // 
            // changePurchase
            // 
            this.changePurchase.DecimalPlaces = 1;
            this.changePurchase.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.changePurchase.Location = new System.Drawing.Point(192, 78);
            this.changePurchase.Name = "changePurchase";
            this.changePurchase.Size = new System.Drawing.Size(107, 22);
            this.changePurchase.TabIndex = 11;
            this.changePurchase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.changePurchase.Visible = false;
            // 
            // purchasesBox
            // 
            this.purchasesBox.Enabled = false;
            this.purchasesBox.Location = new System.Drawing.Point(119, 28);
            this.purchasesBox.Name = "purchasesBox";
            this.purchasesBox.Size = new System.Drawing.Size(100, 22);
            this.purchasesBox.TabIndex = 12;
            this.purchasesBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // salesBox
            // 
            this.salesBox.Enabled = false;
            this.salesBox.Location = new System.Drawing.Point(122, 31);
            this.salesBox.Name = "salesBox";
            this.salesBox.Size = new System.Drawing.Size(100, 22);
            this.salesBox.TabIndex = 17;
            this.salesBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // changeSales
            // 
            this.changeSales.DecimalPlaces = 1;
            this.changeSales.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.changeSales.Location = new System.Drawing.Point(192, 81);
            this.changeSales.Name = "changeSales";
            this.changeSales.Size = new System.Drawing.Size(107, 22);
            this.changeSales.TabIndex = 16;
            this.changeSales.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.changeSales.Visible = false;
            // 
            // keepSalesButton
            // 
            this.keepSalesButton.Location = new System.Drawing.Point(192, 122);
            this.keepSalesButton.Name = "keepSalesButton";
            this.keepSalesButton.Size = new System.Drawing.Size(107, 45);
            this.keepSalesButton.TabIndex = 15;
            this.keepSalesButton.Text = "Alterar/Manter Cotação";
            this.keepSalesButton.UseVisualStyleBackColor = true;
            this.keepSalesButton.Visible = false;
            // 
            // removeSalesButton
            // 
            this.removeSalesButton.Location = new System.Drawing.Point(46, 81);
            this.removeSalesButton.Name = "removeSalesButton";
            this.removeSalesButton.Size = new System.Drawing.Size(104, 86);
            this.removeSalesButton.TabIndex = 14;
            this.removeSalesButton.Text = "Remover Ordem de Venda";
            this.removeSalesButton.UseVisualStyleBackColor = true;
            this.removeSalesButton.Visible = false;
            // 
            // purchaseGroupBox
            // 
            this.purchaseGroupBox.Controls.Add(this.purchaseTimerLabel);
            this.purchaseGroupBox.Controls.Add(this.changePurchase);
            this.purchaseGroupBox.Controls.Add(this.keepPurchaseButton);
            this.purchaseGroupBox.Controls.Add(this.purchasesBox);
            this.purchaseGroupBox.Location = new System.Drawing.Point(525, 21);
            this.purchaseGroupBox.Name = "purchaseGroupBox";
            this.purchaseGroupBox.Size = new System.Drawing.Size(347, 188);
            this.purchaseGroupBox.TabIndex = 18;
            this.purchaseGroupBox.TabStop = false;
            this.purchaseGroupBox.Text = "Ordem de Compra";
            // 
            // purchaseTimerLabel
            // 
            this.purchaseTimerLabel.Location = new System.Drawing.Point(237, 28);
            this.purchaseTimerLabel.Name = "purchaseTimerLabel";
            this.purchaseTimerLabel.Size = new System.Drawing.Size(62, 23);
            this.purchaseTimerLabel.TabIndex = 13;
            this.purchaseTimerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // salesGroupBox
            // 
            this.salesGroupBox.Controls.Add(this.salesTimerLabel);
            this.salesGroupBox.Controls.Add(this.changeSales);
            this.salesGroupBox.Controls.Add(this.salesBox);
            this.salesGroupBox.Controls.Add(this.removeSalesButton);
            this.salesGroupBox.Controls.Add(this.keepSalesButton);
            this.salesGroupBox.Location = new System.Drawing.Point(525, 226);
            this.salesGroupBox.Name = "salesGroupBox";
            this.salesGroupBox.Size = new System.Drawing.Size(347, 182);
            this.salesGroupBox.TabIndex = 19;
            this.salesGroupBox.TabStop = false;
            this.salesGroupBox.Text = "Ordem de Venda";
            // 
            // salesTimerLabel
            // 
            this.salesTimerLabel.Location = new System.Drawing.Point(237, 31);
            this.salesTimerLabel.Name = "salesTimerLabel";
            this.salesTimerLabel.Size = new System.Drawing.Size(62, 23);
            this.salesTimerLabel.TabIndex = 18;
            this.salesTimerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // purchaseQuantity
            // 
            this.purchaseQuantity.Location = new System.Drawing.Point(84, 201);
            this.purchaseQuantity.Name = "purchaseQuantity";
            this.purchaseQuantity.Size = new System.Drawing.Size(133, 22);
            this.purchaseQuantity.TabIndex = 20;
            this.purchaseQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(57, 165);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(188, 164);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.saleQuantity);
            this.groupBox4.Location = new System.Drawing.Point(269, 165);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(188, 164);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            // 
            // saleQuantity
            // 
            this.saleQuantity.Location = new System.Drawing.Point(34, 36);
            this.saleQuantity.Name = "saleQuantity";
            this.saleQuantity.Size = new System.Drawing.Size(133, 22);
            this.saleQuantity.TabIndex = 0;
            this.saleQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 452);
            this.Controls.Add(this.purchaseQuantity);
            this.Controls.Add(this.removePurchaseButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.notificationsBox);
            this.Controls.Add(this.diginotesBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.addSale);
            this.Controls.Add(this.addPurchase);
            this.Controls.Add(this.quotationBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.purchaseGroupBox);
            this.Controls.Add(this.salesGroupBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Name = "MainForm";
            this.Text = "Title";
            ((System.ComponentModel.ISupportInitialize)(this.changePurchase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.changeSales)).EndInit();
            this.purchaseGroupBox.ResumeLayout(false);
            this.purchaseGroupBox.PerformLayout();
            this.salesGroupBox.ResumeLayout(false);
            this.salesGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseQuantity)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.saleQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox quotationBox;
        private System.Windows.Forms.Button addPurchase;
        private System.Windows.Forms.Button addSale;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox diginotesBox;
        private System.Windows.Forms.TextBox notificationsBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button removePurchaseButton;
        private System.Windows.Forms.Button keepPurchaseButton;
        private System.Windows.Forms.NumericUpDown changePurchase;
        private System.Windows.Forms.TextBox purchasesBox;
        private System.Windows.Forms.TextBox salesBox;
        private System.Windows.Forms.NumericUpDown changeSales;
        private System.Windows.Forms.Button keepSalesButton;
        private System.Windows.Forms.Button removeSalesButton;
        private System.Windows.Forms.GroupBox purchaseGroupBox;
        private System.Windows.Forms.GroupBox salesGroupBox;
        private System.Windows.Forms.NumericUpDown purchaseQuantity;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown saleQuantity;
        private System.Windows.Forms.Label purchaseTimerLabel;
        private System.Windows.Forms.Label salesTimerLabel;
    }
}

