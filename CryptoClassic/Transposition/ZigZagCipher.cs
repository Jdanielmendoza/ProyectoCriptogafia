using CryptoClassic.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoClassic.Core.Transposition
{
    public class ZigZagCipher : ICipher
    {
        public string Name => "Transposición Zig-Zag (Rail Fence)";

        public string Encrypt(string plaintext, object key)
        {
            int rails = ParseRails(key);
            var railsArr = new StringBuilder[rails];
            for (int i = 0; i < rails; i++) railsArr[i] = new StringBuilder();

            int row = 0, dir = 1;        // dir: +1 bajando, -1 subiendo
            foreach (char ch in plaintext)
            {
                railsArr[row].Append(ch);
                if (row == 0) dir = 1;
                if (row == rails - 1) dir = -1;
                row += dir;
            }

            var result = new StringBuilder(plaintext.Length);
            foreach (var sb in railsArr) result.Append(sb);
            return result.ToString();
        }

        public string Decrypt(string ciphertext, object key)
        {
            int rails = ParseRails(key);
            // 1️⃣ Calcula la ruta del zig-zag para saber cuántos
            //    caracteres van en cada riel.
            int[] lengths = new int[rails];
            int row = 0, dir = 1;
            foreach (var _ in ciphertext)
            {
                lengths[row]++;
                if (row == 0) dir = 1;
                if (row == rails - 1) dir = -1;
                row += dir;
            }

            // 2️. Extrae segmentos del cifrado y rellena los rieles.
            var railsArr = new Queue<char>[rails];
            int idx = 0;
            for (int r = 0; r < rails; r++)
            {
                railsArr[r] = new Queue<char>();
                for (int k = 0; k < lengths[r]; k++)
                    railsArr[r].Enqueue(ciphertext[idx++]);
            }

            // 3️. Recorrer de nuevo la ruta para reconstruir el texto.
            var result = new StringBuilder(ciphertext.Length);
            row = 0; dir = 1;
            foreach (var _ in ciphertext)
            {
                result.Append(railsArr[row].Dequeue());
                if (row == 0) dir = 1;
                if (row == rails - 1) dir = -1;
                row += dir;
            }
            return result.ToString();
        }

        /* -------- helpers -------- */

        private static int ParseRails(object keyObj)
        {
            if (!int.TryParse(keyObj?.ToString(), out int rails) || rails < 2)
                throw new ArgumentException("La clave debe ser un número entero ≥ 2 (cantidad de rieles).");
            return rails;
        }
    }
}
