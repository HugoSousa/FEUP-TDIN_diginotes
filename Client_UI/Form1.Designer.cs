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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.diginotesBox = new System.Windows.Forms.TextBox();
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(84, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 61);
            this.button1.TabIndex = 2;
            this.button1.Text = "Adicionar Ordem de Compra";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(303, 156);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(133, 61);
            this.button2.TabIndex = 3;
            this.button2.Text = "Adicionar Ordem de Venda";
            this.button2.UseVisualStyleBackColor = true;
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 264);
            this.Controls.Add(this.diginotesBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.quotationBox);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "Title";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox quotationBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox diginotesBox;
    }
}

