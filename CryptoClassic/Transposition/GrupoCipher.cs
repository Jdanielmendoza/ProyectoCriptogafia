using CryptoClassic.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClassic.Core.Transposition
{
    public class GrupoCipher : ICipher
    {
        public string Name => "Transposición por Grupos";

        public string Decrypt(string ciphertext, object key)
        {
            int groupSize = Convert.ToInt32(key);
            StringBuilder plaintext = new StringBuilder();

            for (int i = 0; i < ciphertext.Length; i += groupSize)
            {
                int size = Math.Min(groupSize, ciphertext.Length - i);
                string group = ciphertext.Substring(i, size);
                char[] reversed = group.ToCharArray();
                Array.Reverse(reversed);
                plaintext.Append(reversed);
            }

            return plaintext.ToString().TrimEnd();
        }

        public string Encrypt(string plaintext, object key)
        {
            int groupSize = Convert.ToInt32(key);
            StringBuilder ciphertext = new StringBuilder();

            for (int i = 0; i < plaintext.Length; i += groupSize)
            {
                int size = Math.Min(groupSize, plaintext.Length - i);
                string group = plaintext.Substring(i, size);
                char[] reversed = group.PadRight(groupSize, ' ').ToCharArray();
                Array.Reverse(reversed);
                ciphertext.Append(reversed);
            }

            return ciphertext.ToString();
        }
    }
}
