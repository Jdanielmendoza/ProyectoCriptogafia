namespace CryptoClassic.Demo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbCipher = new System.Windows.Forms.ComboBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmbCipher
            // 
            this.cmbCipher.AccessibleName = "cmbCipher";
            this.cmbCipher.FormattingEnabled = true;
            this.cmbCipher.Location = new System.Drawing.Point(316, 129);
            this.cmbCipher.Name = "cmbCipher";
            this.cmbCipher.Size = new System.Drawing.Size(151, 28);
            this.cmbCipher.TabIndex = 0;
            // 
            // txtInput
            // 
            this.txtInput.AccessibleName = "txtInput";
            this.txtInput.Location = new System.Drawing.Point(171, 216);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(125, 27);
            this.txtInput.TabIndex = 1;
            // 
            // txtKey
            // 
            this.txtKey.AccessibleName = "txtOutput";
            this.txtKey.Location = new System.Drawing.Point(342, 216);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(125, 27);
            this.txtKey.TabIndex = 2;
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(523, 216);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(94, 29);
            this.btnEncrypt.TabIndex = 3;
            this.btnEncrypt.Text = "Cifrar";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(523, 276);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(94, 29);
            this.btnDecrypt.TabIndex = 4;
            this.btnDecrypt.Text = "Descifrar";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(171, 278);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(296, 27);
            this.txtOutput.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.cmbCipher);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox cmbCipher;
        private TextBox txtInput;
        private TextBox txtKey;
        private Button btnEncrypt;
        private Button btnDecrypt;
        private TextBox txtOutput;
    }
}