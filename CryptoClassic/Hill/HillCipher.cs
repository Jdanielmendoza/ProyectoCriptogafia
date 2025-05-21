using CryptoClassic.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptoClassic.Core.Hill
{
    public class HillCipher : ICipher
    {
        public string Name => "Hill (2×2 / 3×3)";
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const int Mod = 26;

        /* ----------  API  ---------- */

        public string Encrypt(string plaintext, object key)
        {
            var K = ParseMatrix(key);
            var inv = InvertMatrix(K);           // valida inversibilidad
            return Transform(plaintext, K);
        }

        public string Decrypt(string ciphertext, object key)
        {
            var K = ParseMatrix(key);
            var inv = InvertMatrix(K);
            return Transform(ciphertext, inv);
        }

        /* ----------  núcleo  ---------- */

        private string Transform(string text, int[,] M)
        {
            int n = M.GetLength(0);
            var clean = text.ToUpper()
                            .Where(char.IsLetter)
                            .Select(ch => Alphabet.IndexOf(ch))
                            .ToList();

            // Relleno con X (23) para múltiplos de n
            while (clean.Count % n != 0) clean.Add(Alphabet.IndexOf('X'));

            var sb = new StringBuilder(clean.Count);
            for (int i = 0; i < clean.Count; i += n)
            {
                int[] block = clean.Skip(i).Take(n).ToArray();
                int[] res = Multiply(M, block);

                foreach (int val in res)
                    sb.Append(Alphabet[val]);
            }
            return sb.ToString();
        }

        /* ----------  matrices  ---------- */

        private static int[] Multiply(int[,] A, int[] v)
        {
            int n = A.GetLength(0);
            var res = new int[n];
            for (int r = 0; r < n; r++)
            {
                int sum = 0;
                for (int c = 0; c < n; c++)
                    sum += A[r, c] * v[c];
                res[r] = ((sum % Mod) + Mod) % Mod;
            }
            return res;
        }

        private static int[,] ParseMatrix(object keyObj)
        {
            /* Formato clave ejemplo:
               "6 24 1; 13 16 10; 20 17 15"   (3×3)
               "3 3; 2 5"                      (2×2)
            */
            string raw = keyObj?.ToString() ?? "";
            var rows = raw.Split(new[] { ';', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int n = rows.Length;
            if (n != 2 && n != 3) throw new ArgumentException("Solo soporta matrices 2×2 o 3×3.");

            var M = new int[n, n];
            for (int r = 0; r < n; r++)
            {
                var cols = rows[r].Trim().Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (cols.Length != n) throw new ArgumentException("La matriz debe ser cuadrada.");
                for (int c = 0; c < n; c++)
                    M[r, c] = ((int.Parse(cols[c]) % Mod) + Mod) % Mod;
            }
            return M;
        }

        private static int[,] InvertMatrix(int[,] M)
        {
            int n = M.GetLength(0);
            int det = Determinant(M);
            int detInv = ModInverse(det, Mod);

            if (det == 0 || detInv == -1)
                throw new ArgumentException("La matriz no es invertible módulo 26.");

            int[,] adj = (n == 2) ? Adjugate2(M) : Adjugate3(M);
            var inv = new int[n, n];
            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    inv[r, c] = ((adj[r, c] * detInv) % Mod + Mod) % Mod;
            return inv;
        }

        /* ---- determinante y adjunta ---- */

        private static int Determinant(int[,] M) =>
            (M.GetLength(0) == 2) ?         // 2×2
                (M[0, 0] * M[1, 1] - M[0, 1] * M[1, 0]) :
                (                             // 3×3
                  M[0, 0] * (M[1, 1] * M[2, 2] - M[1, 2] * M[2, 1])
                - M[0, 1] * (M[1, 0] * M[2, 2] - M[1, 2] * M[2, 0])
                + M[0, 2] * (M[1, 0] * M[2, 1] - M[1, 1] * M[2, 0]));

        private static int[,] Adjugate2(int[,] M) => new int[,]
        {
            {  M[1,1], -M[0,1] },
            { -M[1,0],  M[0,0] }
        };

        private static int[,] Adjugate3(int[,] m)
        {
            var a = new int[3, 3];
            a[0, 0] = (m[1, 1] * m[2, 2] - m[1, 2] * m[2, 1]);
            a[0, 1] = -(m[1, 0] * m[2, 2] - m[1, 2] * m[2, 0]);
            a[0, 2] = (m[1, 0] * m[2, 1] - m[1, 1] * m[2, 0]);

            a[1, 0] = -(m[0, 1] * m[2, 2] - m[0, 2] * m[2, 1]);
            a[1, 1] = (m[0, 0] * m[2, 2] - m[0, 2] * m[2, 0]);
            a[1, 2] = -(m[0, 0] * m[2, 1] - m[0, 1] * m[2, 0]);

            a[2, 0] = (m[0, 1] * m[1, 2] - m[0, 2] * m[1, 1]);
            a[2, 1] = -(m[0, 0] * m[1, 2] - m[0, 2] * m[1, 0]);
            a[2, 2] = (m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0]);
            return a;
        }

        /* ---- aritmética modular ---- */

        private static int ModInverse(int a, int m)
        {
            a = ((a % m) + m) % m;
            for (int x = 1; x < m; x++)
                if ((a * x) % m == 1) return x;
            return -1; // no inverso
        }
    }
}
