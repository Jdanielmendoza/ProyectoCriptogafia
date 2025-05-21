using CryptoClassic.Core.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace CryptoClassic.Core.Substitution
{
    public class AffineCipher : ICipher
    {
        public string Name => "Cifrado Afín (a·x + b)";
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const int Mod = 26;   // m

        public string Encrypt(string plaintext, object key)
        {
            (int a, int b) = ParseKey(key);
            return Transform(plaintext, (x) => (a * x + b) % Mod);
        }

        public string Decrypt(string ciphertext, object key)
        {
            (int a, int b) = ParseKey(key);
            int aInv = ModInverse(a, Mod);          // calcula a⁻¹
            return Transform(ciphertext, (x) => (aInv * (x - b + Mod)) % Mod);
        }

        /* ---------- helpers ---------- */

        private string Transform(string text, Func<int, int> fx)
        {
            var sb = new StringBuilder(text.Length);

            foreach (char ch in text)
            {
                bool upper = char.IsUpper(ch);
                char c = char.ToUpper(ch);

                int idx = Alphabet.IndexOf(c);
                if (idx >= 0)          // letra encontrada
                {
                    int res = fx(idx);
                    char mapped = Alphabet[res];
                    sb.Append(upper ? mapped : char.ToLower(mapped));
                }
                else
                {
                    sb.Append(ch);     // números, signos, espacios
                }
            }
            return sb.ToString();
        }

        private (int a, int b) ParseKey(object keyObj)
        {
            string key = keyObj?.ToString() ?? "";
            var parts = key.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2 ||
                !int.TryParse(parts[0], out int a) ||
                !int.TryParse(parts[1], out int b))
                throw new ArgumentException("Clave inválida. Usa el formato \"a,b\" (ej. 5,8).");

            a = ((a % Mod) + Mod) % Mod;
            b = ((b % Mod) + Mod) % Mod;

            if (Gcd(a, Mod) != 1)
                throw new ArgumentException($"El valor 'a' debe ser coprimo con {Mod}. Ej. 5,7,11…");

            return (a, b);
        }

        private static int Gcd(int a, int b) => b == 0 ? a : Gcd(b, a % b);

        // Extended Euclidean Algorithm
        private static int ModInverse(int a, int m)
        {
            int m0 = m, x0 = 0, x1 = 1;
            while (a > 1)
            {
                int q = a / m;
                (a, m) = (m, a % m);
                (x0, x1) = (x1 - q * x0, x0);
            }
            if (x1 < 0) x1 += m0;
            return x1;
        }
    }
}
