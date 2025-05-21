using CryptoClassic.Core.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace CryptoClassic.Core.Keyword
{
    public class KeywordCipher : ICipher
    {
        public string Name => "Cifrado con Palabra Clave";
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        // Construye el alfabeto cifrado a partir de la clave
        private string BuildCipherAlphabet(string keyword)
        {
            keyword = new string(keyword
                                  .ToUpper()
                                  .Where(char.IsLetter)
                                  .ToArray());

            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentException("La clave no puede estar vacía.");

            // Quitar duplicados manteniendo orden
            var unique = new StringBuilder();
            foreach (char c in keyword)
                if (!unique.ToString().Contains(c))
                    unique.Append(c);

            // Completar con el resto de letras
            foreach (char c in Alphabet)
                if (!unique.ToString().Contains(c))
                    unique.Append(c);

            return unique.ToString(); // longitud 26
        }

        public string Encrypt(string plaintext, object key)
        {
            string cipherAlphabet = BuildCipherAlphabet(key.ToString());
            return Translate(plaintext, Alphabet, cipherAlphabet);
        }

        public string Decrypt(string ciphertext, object key)
        {
            string cipherAlphabet = BuildCipherAlphabet(key.ToString());
            return Translate(ciphertext, cipherAlphabet, Alphabet);
        }

        private string Translate(string text, string fromAlphabet, string toAlphabet)
        {
            var sb = new StringBuilder(text.Length);
            foreach (char ch in text)
            {
                bool upper = char.IsUpper(ch);
                char c = char.ToUpper(ch);

                int idx = fromAlphabet.IndexOf(c);
                if (idx >= 0) // letra encontrada
                {
                    char mapped = toAlphabet[idx];
                    sb.Append(upper ? mapped : char.ToLower(mapped));
                }
                else
                {
                    sb.Append(ch); // signos, números, espacios…
                }
            }
            return sb.ToString();
        }
    }
}
