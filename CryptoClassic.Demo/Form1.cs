namespace CryptoClassic.Demo
{
    using CryptoClassic.Core.Interfaces;
    using CryptoClassic.Core.Caesar;   // ← importa los que vayas creando
    using System.Collections.Generic;

    public partial class Form1 : Form
    {
        private readonly List<ICipher> _ciphers;
        public Form1()
        {
            InitializeComponent();

            // 1️⃣  Instancia la lista (por ahora solo César)
            _ciphers = new List<ICipher>
        {
            new CaesarCipher()
            // Ej.: new KeywordCipher(), new AffineCipher()…
        };

            // 2️⃣  Enlázala al ComboBox
            cmbCipher.DataSource = _ciphers;
            cmbCipher.DisplayMember = "Name";
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (cmbCipher.SelectedItem is ICipher cipher)
            {
                try
                {
                    txtOutput.Text = cipher.Encrypt(txtInput.Text, txtKey.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Clave inválida o error interno:\n{ex.Message}");
                }
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (cmbCipher.SelectedItem is ICipher cipher)
            {
                try
                {
                    txtOutput.Text = cipher.Decrypt(txtInput.Text, txtKey.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Clave inválida o error interno:\n{ex.Message}");
                }
            }
        }
    }

}
