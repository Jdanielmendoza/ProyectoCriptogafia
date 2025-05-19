using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClassic.Core.Interfaces
{
    public interface ICipher
    {
        string Name { get; }
        string Encrypt(string plaintext, object key);
        string Decrypt(string ciphertext, object key);
    }
}
