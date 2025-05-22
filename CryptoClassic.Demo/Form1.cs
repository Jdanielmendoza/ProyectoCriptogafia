namespace CryptoClassic.Demo
{
    using CryptoClassic.Core.Interfaces;
    using CryptoClassic.Core.Caesar;   // ← importar los que se vaya creando
    using CryptoClassic.Core.Keyword;
    using CryptoClassic.Core.Substitution;
    using CryptoClassic.Core.Transposition;
    using CryptoClassic.Core.Hill;
    using CryptoClassic.Core.Attacks;
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
            new CaesarCipher(),
             new KeywordCipher(),
             new AffineCipher() ,
             new PolyAlphabeticCipher(),
             new ZigZagCipher() ,
             new ColumnarCipher(),
             new PlayfairCipher() ,
             new HillCipher(),
             new KasiskiAttack(),
             new FilaCipher(),
             new SerieCipher(),
             new GrupoCipher(),
            // Ej.: new KeywordCipher(), new AffineCipher()…
        };

            //Placeholders 
            cmbCipher.SelectedIndexChanged += (s, e) =>
            {
                txtKey.Enabled = !(cmbCipher.SelectedItem is KasiskiAttack);
                txtKey.PlaceholderText = cmbCipher.SelectedItem switch
                {
                    KasiskiAttack => "(sin clave)",
                    HillCipher => "Matriz 2×2 o 3×3",
                    PlayfairCipher => "Palabra clave…",
                    ColumnarCipher => "Palabra clave columnas…",
                    FilaCipher => "Número de columnas...",
                    SerieCipher => "Serie(ej. 2 1 3)",
                    GrupoCipher => "Número de caracteres a agrupar",
                    ZigZagCipher => "Número de rieles (≥2)",
                    PolyAlphabeticCipher => "Palabra clave…",
                    AffineCipher => "a,b  (ej. 5,8)",
                    _ => "Clave…"
                };
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmbCipher_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}
