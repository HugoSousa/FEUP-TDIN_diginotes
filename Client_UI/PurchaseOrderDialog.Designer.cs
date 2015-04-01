namespace Client_UI
{
    partial class PurchaseOrderDialog
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
            this.quantity = new System.Windows.Forms.NumericUpDown();
            this.buyButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.quantity)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "How many diginotes you want to buy?";
            // 
            // quantity
            // 
            this.quantity.Location = new System.Drawing.Point(80, 57);
            this.quantity.Name = "quantity";
            this.quantity.Size = new System.Drawing.Size(120, 22);
            this.quantity.TabIndex = 1;
            this.quantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buyButton
            // 
            this.buyButton.Location = new System.Drawing.Point(101, 85);
            this.buyButton.Name = "buyButton";
            this.buyButton.Size = new System.Drawing.Size(75, 30);
            this.buyButton.TabIndex = 2;
            this.buyButton.Text = "Buy";
            this.buyButton.UseVisualStyleBackColor = true;
            this.buyButton.Click += new System.EventHandler(this.buyButton_Click);
            // 
            // PurchaseOrderDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 132);
            this.Controls.Add(this.buyButton);
            this.Controls.Add(this.quantity);
            this.Controls.Add(this.label1);
            this.Name = "PurchaseOrderDialog";
            this.Text = "PurchaseOrderDialog";
            ((System.ComponentModel.ISupportInitialize)(this.quantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown quantity;
        private System.Windows.Forms.Button buyButton;
    }
}