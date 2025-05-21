using CryptoClassic.Core.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace CryptoClassic.Core.Substitution
{
    public class PolyAlphabeticCipher : ICipher
    {
        public string Name => "Cifra Vigenère";
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const int Mod = 26;

        public string Encrypt(string plaintext, object key)
        {
            int[] k = BuildKeyStream(key, plaintext.Length);
            return Transform(plaintext, (p, ki) => (p + ki) % Mod, k);
        }

        public string Decrypt(string ciphertext, object key)
        {
            int[] k = BuildKeyStream(key, ciphertext.Length);
            return Transform(ciphertext, (c, ki) => (c - ki + Mod) % Mod, k);
        }

        /* ---------- helpers ---------- */

        private int[] BuildKeyStream(object keyObj, int length)
        {
            string keyword = keyObj?.ToString().ToUpper()
                                       .Where(char.IsLetter)
                                       .Aggregate("", (a, c) => a + c);

            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentException("La clave debe contener letras A-Z.");

            // Repite o recorta la clave para que coincida con la longitud del texto
            var keyStream = new int[length];
            for (int i = 0; i < length; i++)
            {
                char k = keyword[i % keyword.Length];
                keyStream[i] = Alphabet.IndexOf(k);
            }
            return keyStream;
        }

        private string Transform(string text,
                                 Func<int, int, int> op,
                                 int[] keyStream)
        {
            var sb = new StringBuilder(text.Length);
            int j = 0;   // índice sobre la clave (ignorando no-letras)

            foreach (char ch in text)
            {
                bool upper = char.IsUpper(ch);
                char c = char.ToUpper(ch);
                int idx = Alphabet.IndexOf(c);

                if (idx >= 0)   // letra válida
                {
                    int res = op(idx, keyStream[j++]);
                    char mapped = Alphabet[res];
                    sb.Append(upper ? mapped : char.ToLower(mapped));
                }
                else
                {  
                    sb.Append(ch);      // espacios, números, etc.
                }
            }
            return sb.ToString();
        }
    }
}
