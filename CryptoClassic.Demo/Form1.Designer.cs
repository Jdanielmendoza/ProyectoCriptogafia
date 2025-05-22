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
            cmbCipher = new ComboBox();
            txtInput = new TextBox();
            txtKey = new TextBox();
            btnEncrypt = new Button();
            btnDecrypt = new Button();
            txtOutput = new TextBox();
            panel1 = new Panel();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // cmbCipher
            // 
            cmbCipher.AccessibleName = "cmbCipher";
            cmbCipher.FormattingEnabled = true;
            cmbCipher.Location = new Point(154, 96);
            cmbCipher.Name = "cmbCipher";
            cmbCipher.Size = new Size(293, 25);
            cmbCipher.TabIndex = 0;
            cmbCipher.SelectedIndexChanged += cmbCipher_SelectedIndexChanged;
            // 
            // txtInput
            // 
            txtInput.AccessibleName = "txtInput";
            txtInput.Location = new Point(23, 210);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(676, 25);
            txtInput.TabIndex = 1;
            // 
            // txtKey
            // 
            txtKey.AccessibleName = "txtOutput";
            txtKey.Location = new Point(154, 265);
            txtKey.Name = "txtKey";
            txtKey.Size = new Size(293, 25);
            txtKey.TabIndex = 2;
            // 
            // btnEncrypt
            // 
            btnEncrypt.Location = new Point(404, 342);
            btnEncrypt.Name = "btnEncrypt";
            btnEncrypt.Size = new Size(141, 59);
            btnEncrypt.TabIndex = 3;
            btnEncrypt.Text = "Cifrar";
            btnEncrypt.UseVisualStyleBackColor = true;
            btnEncrypt.Click += btnEncrypt_Click;
            // 
            // btnDecrypt
            // 
            btnDecrypt.Location = new Point(560, 342);
            btnDecrypt.Name = "btnDecrypt";
            btnDecrypt.Size = new Size(139, 59);
            btnDecrypt.TabIndex = 4;
            btnDecrypt.Text = "Descifrar";
            btnDecrypt.UseVisualStyleBackColor = true;
            btnDecrypt.Click += btnDecrypt_Click;
            // 
            // txtOutput
            // 
            txtOutput.Location = new Point(23, 475);
            txtOutput.Name = "txtOutput";
            txtOutput.Size = new Size(676, 25);
            txtOutput.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(cmbCipher);
            panel1.Controls.Add(btnDecrypt);
            panel1.Controls.Add(txtOutput);
            panel1.Controls.Add(txtInput);
            panel1.Controls.Add(txtKey);
            panel1.Controls.Add(btnEncrypt);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(20);
            panel1.Size = new Size(722, 702);
            panel1.TabIndex = 6;
            panel1.Paint += panel1_Paint;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(23, 437);
            label5.Name = "label5";
            label5.Size = new Size(69, 19);
            label5.TabIndex = 10;
            label5.Text = "Resultado";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(23, 268);
            label4.Name = "label4";
            label4.Size = new Size(45, 19);
            label4.TabIndex = 9;
            label4.Text = "Clave:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 174);
            label3.Name = "label3";
            label3.Size = new Size(114, 19);
            label3.TabIndex = 8;
            label3.Text = "Texto de entrada:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(20, 85);
            label2.Name = "label2";
            label2.Size = new Size(100, 25);
            label2.TabIndex = 7;
            label2.Text = "Algoritmo:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(20, 20);
            label1.MinimumSize = new Size(750, 0);
            label1.Name = "label1";
            label1.Padding = new Padding(0, 0, 0, 40);
            label1.Size = new Size(750, 65);
            label1.TabIndex = 6;
            label1.Text = "Algoritmos Criptográficos Clásicos  ";
            label1.Click += label1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(722, 702);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CryptoClassic Demo";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private ComboBox cmbCipher;
        private TextBox txtInput;
        private TextBox txtKey;
        private Button btnEncrypt;
        private Button btnDecrypt;
        private TextBox txtOutput;
        private Panel panel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label4;
    }
}