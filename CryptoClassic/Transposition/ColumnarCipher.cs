using CryptoClassic.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptoClassic.Core.Transposition
{
    public class ColumnarCipher : ICipher
    {
        public string Name => "Transposición Columnar";

        public string Encrypt(string plaintext, object key)
        {
            int[] order = BuildOrder(key);
            int cols = order.Length;
            int rows = (int)Math.Ceiling(plaintext.Length / (double)cols);

            // 1️⃣ Rellena matriz por filas
            char[,] grid = new char[rows, cols];
            int k = 0;
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    grid[r, c] = k < plaintext.Length ? plaintext[k++] : 'X';

            // 2️⃣ Lee columnas en orden de la clave
            var sb = new StringBuilder(rows * cols);
            foreach (int c in order)
                for (int r = 0; r < rows; r++)
                    sb.Append(grid[r, c]);

            return sb.ToString();
        }

        public string Decrypt(string ciphertext, object key)
        {
            int[] order = BuildOrder(key);
            int cols = order.Length;
            int rows = ciphertext.Length / cols;

            // 1️⃣ Crea rejilla vacía
            char[,] grid = new char[rows, cols];
            int k = 0;

            // 2️⃣ Rellena columna por columna
            foreach (int c in order)
                for (int r = 0; r < rows; r++)
                    grid[r, c] = ciphertext[k++];

            // 3️⃣ Lee por filas
            var sb = new StringBuilder(ciphertext.Length);
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    sb.Append(grid[r, c]);

            return sb.ToString().TrimEnd('X');           // quita relleno
        }

        /* ---------- helpers ---------- */

        private static int[] BuildOrder(object keyObj)
        {
            string key = keyObj?.ToString()
                               .ToUpper()
                               .Where(char.IsLetter)
                               .Aggregate("", (a, c) => a + c);

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("La clave debe tener al menos una letra.");

            // Asigna orden alfabético estable a las posiciones
            var indexed = key.Select((ch, idx) => (ch, idx)).ToList();
            var ordered = indexed.OrderBy(p => p.ch)
                                 .ThenBy(p => p.idx)
                                 .Select(p => p.idx)
                                 .ToArray();
            return ordered;
        }
    }
}
