using CryptoClassic.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptoClassic.Core.Substitution
{
    public class PlayfairCipher : ICipher
    {
        public string Name => "Playfair (Matriz 5×5)";
        private const string Alphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ"; // J≡I

        /* ---------- API ---------- */

        public string Encrypt(string plaintext, object key) =>
            Process(plaintext, key, encrypt: true);

        public string Decrypt(string ciphertext, object key) =>
            Process(ciphertext, key, encrypt: false);

        /* ---------- núcleo ---------- */

        private string Process(string text, object keyObj, bool encrypt)
        {
            // 1. Cuadrado y diccionario inverso
            var square = BuildSquare(keyObj?.ToString() ?? "");
            var pos = new Dictionary<char, (int r, int c)>();
            for (int r = 0; r < 5; r++)
                for (int c = 0; c < 5; c++)
                    pos[square[r, c]] = (r, c);

            // 2. Dígrafos
            var pairs = PrepareDigraphs(text);

            // 3. Recorre cada par
            var sb = new StringBuilder(pairs.Count * 2);
            int dir = encrypt ? 1 : -1;

            foreach ((char a, char b) in pairs)
            {
                var (ra, ca) = pos[a];
                var (rb, cb) = pos[b];

                if (ra == rb)                     // misma fila
                {
                    ca = Mod(ca + dir, 5);
                    cb = Mod(cb + dir, 5);
                }
                else if (ca == cb)                // misma columna
                {
                    ra = Mod(ra + dir, 5);
                    rb = Mod(rb + dir, 5);
                }
                else                              // rectángulo
                {
                    (ca, cb) = (cb, ca);
                }
                sb.Append(square[ra, ca]);
                sb.Append(square[rb, cb]);
            }
            return sb.ToString();
        }

        /* ---------- helpers ---------- */

        private static int Mod(int n, int m) => (n % m + m) % m;

        private static char[,] BuildSquare(string keyword)
        {
            keyword = keyword.ToUpper()
                             .Replace("J", "I")
                             .Where(char.IsLetter)
                             .Aggregate("", (acc, ch) => acc.Contains(ch) ? acc : acc + ch);

            string full = (keyword + Alphabet)
                         .Aggregate("", (acc, ch) => acc.Contains(ch) ? acc : acc + ch);

            var sq = new char[5, 5];
            for (int i = 0; i < 25; i++)
                sq[i / 5, i % 5] = full[i];
            return sq;
        }

        private static List<(char, char)> PrepareDigraphs(string input)
        {
            string cleaned = input.ToUpper()
                                  .Replace("J", "I")
                                  .Where(char.IsLetter)
                                  .Aggregate("", (a, c) => a + c);

            var pairs = new List<(char, char)>();
            int i = 0;
            while (i < cleaned.Length)
            {
                char a = cleaned[i++];
                char b = (i < cleaned.Length) ? cleaned[i] : 'X';

                if (a == b)                       // duplicado en par
                    b = 'X';
                else
                    i++;

                pairs.Add((a, b));
            }
            if (pairs.Count > 0 && pairs[^1].Item2 == '\0') // seguridad
                pairs[^1] = (pairs[^1].Item1, 'X');

            return pairs;
        }
    }
}
