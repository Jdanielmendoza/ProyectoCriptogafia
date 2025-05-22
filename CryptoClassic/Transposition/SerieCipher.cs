using CryptoClassic.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClassic.Core.Transposition
{
    public class SerieCipher : ICipher
    {
        public string Name => "Transposición por Series";

        public string Decrypt(string ciphertext, object key)
        {
            int[] series = key.ToString().Split(' ', ',').Select(int.Parse).ToArray();
            int blockSize = series.Length;
            int fullBlocks = (int)Math.Ceiling((double)ciphertext.Length / blockSize);

            int[] inverseSeries = new int[blockSize];
            for (int i = 0; i < blockSize; i++)
            {
                inverseSeries[series[i] - 1] = i;
            }

            StringBuilder plaintext = new StringBuilder();

            for (int i = 0; i < fullBlocks; i++)
            {
                char[] block = new char[blockSize];
                for (int j = 0; j < blockSize; j++)
                {
                    int index = i * blockSize + j;
                    block[j] = index < ciphertext.Length ? ciphertext[index] : ' ';
                }

                for (int j = 0; j < blockSize; j++)
                {
                    plaintext.Append(block[inverseSeries[j]]);
                }
            }

            return plaintext.ToString().TrimEnd();
        }

        public string Encrypt(string plaintext, object key)
        {
            int[] series = key.ToString().Split(' ', ',').Select(int.Parse).ToArray();
            int blockSize = series.Length;
            int fullBlocks = (int)Math.Ceiling((double)plaintext.Length / blockSize);

            StringBuilder ciphertext = new StringBuilder();

            for (int i = 0; i < fullBlocks; i++)
            {
                char[] block = new char[blockSize];
                for (int j = 0; j < blockSize; j++)
                {
                    int index = i * blockSize + j;
                    block[j] = index < plaintext.Length ? plaintext[index] : ' ';
                }

                for (int j = 0; j < blockSize; j++)
                {
                    ciphertext.Append(block[series[j] - 1]);
                }
            }

            return ciphertext.ToString();
        }
    }
}
