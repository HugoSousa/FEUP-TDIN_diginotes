namespace Client_UI
{
    partial class ChangeQuotationForm
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
            this.infoBox = new System.Windows.Forms.Label();
            this.newQuotation = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.newQuotation)).BeginInit();
            this.SuspendLayout();
            // 
            // infoBox
            // 
            this.infoBox.Location = new System.Drawing.Point(12, 29);
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(258, 37);
            this.infoBox.TabIndex = 1;
            // 
            // newQuotation
            // 
            this.newQuotation.DecimalPlaces = 1;
            this.newQuotation.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.newQuotation.Location = new System.Drawing.Point(76, 84);
            this.newQuotation.Name = "newQuotation";
            this.newQuotation.Size = new System.Drawing.Size(127, 22);
            this.newQuotation.TabIndex = 2;
            this.newQuotation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(92, 124);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 41);
            this.button1.TabIndex = 3;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ChangeQuotationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 192);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.newQuotation);
            this.Controls.Add(this.infoBox);
            this.Name = "ChangeQuotationForm";
            this.Text = "ChangeQuotationForm";
            ((System.ComponentModel.ISupportInitialize)(this.newQuotation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label infoBox;
        private System.Windows.Forms.NumericUpDown newQuotation;
        private System.Windows.Forms.Button button1;

    }
}