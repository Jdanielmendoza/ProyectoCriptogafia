using CryptoClassic.Core.Interfaces;
using System;
using System.Text;

namespace CryptoClassic.Core.Caesar
{
    public class CaesarCipher : ICipher
    {
        public string Name => "Cifrado César";
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public string Encrypt(string plaintext, object key)
        {
            int shift = ParseKey(key);
            return Translate(plaintext.ToUpper(), shift);
        }

        public string Decrypt(string ciphertext, object key)
        {
            int shift = Alphabet.Length - ParseKey(key);
            return Translate(ciphertext.ToUpper(), shift);
        }

        private int ParseKey(object key) =>
            Math.Abs(Convert.ToInt32(key)) % Alphabet.Length;

        private string Translate(string text, int shift)
        {
            var sb = new StringBuilder(text.Length);
            foreach (char c in text)
            {
                int idx = Alphabet.IndexOf(c);
                sb.Append(idx >= 0 ? Alphabet[(idx + shift) % Alphabet.Length] : c);
            }
            return sb.ToString();
        }
    }
}
