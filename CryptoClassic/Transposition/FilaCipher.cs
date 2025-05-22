using CryptoClassic.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClassic.Core.Transposition
{
    public class FilaCipher : ICipher
    {
        string ICipher.Name => "Transformación por filas";

        string ICipher.Decrypt(string ciphertext, object key)
        {
            if (!TryGetColumnsFromKey(key, out int columns))
                throw new ArgumentException("La clave debe ser un número entero válido.");


            int rows = (int)Math.Ceiling((double)ciphertext.Length / columns);
            char[,] matrix = new char[rows, columns];

            int index = 0;
            for (int c = 0; c < columns; c++)
            {
                for (int r = 0; r < rows; r++)
                {
                    if (index < ciphertext.Length)
                        matrix[r, c] = ciphertext[index++];
                }
            }

            StringBuilder plaintext = new StringBuilder();
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    plaintext.Append(matrix[r, c]);
                }
            }

            return plaintext.ToString().TrimEnd();
        }

        string ICipher.Encrypt(string plaintext, object key)
        {
            if (!TryGetColumnsFromKey(key, out int columns))
                throw new ArgumentException("La clave debe ser un número entero válido.");

            int rows = (int)Math.Ceiling((double)plaintext.Length / columns);
            char[,] matrix = new char[rows, columns];

            int index = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    matrix[r, c] = index < plaintext.Length ? plaintext[index++] : ' ';
                }
            }

            StringBuilder ciphertext = new StringBuilder();
            for (int c = 0; c < columns; c++)
            {
                for (int r = 0; r < rows; r++)
                {
                    ciphertext.Append(matrix[r, c]);
                }
            }

            return ciphertext.ToString();
        }

        private bool TryGetColumnsFromKey(object key, out int columns)
        {
            if (key is int intKey)
            {
                columns = intKey;
                return true;
            }

            if (key is string strKey && int.TryParse(strKey, out int parsed))
            {
                columns = parsed;
                return true;
            }

            columns = 0;
            return false;
        }
    }
}
