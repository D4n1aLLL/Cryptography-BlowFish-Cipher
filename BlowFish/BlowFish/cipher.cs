using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlowFish
{
    class cipher
    {
        string[] P = {
    "243f6a88", "85a308d3", "13198a2e", "03707344", "a4093822",
    "299f31d0", "082efa98", "ec4e6c89", "452821e6", "38d01377",
    "be5466cf", "34e90c6c", "c0ac29b7", "c97c50dd", "3f84d5b5",
    "b5470917", "9216d5d9", "8979fb1b",
        };
        string key;
        public cipher(string _k)
        {
            key = _k;
        }

        public string hexaToBinary(string hexa)
        {
        string binarystring = String.Join(String.Empty,
          hexa.Select(
            c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
          )
        );
            return binarystring;
        }

        public string binaryToHexa(string binary)
        {
            if (string.IsNullOrEmpty(binary))
                return binary;

            StringBuilder result = new StringBuilder(binary.Length / 8 + 1);

            // TODO: check all 1's or 0's... throw otherwise

            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                // pad to length multiple of 8
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString().ToLower();
        }

        public string xor(string a, string b)
        {
            a = hexaToBinary(a);
            b = hexaToBinary(b);
            char t = a[0];
            string result = "";
            for (int i = 0; i < a.Length; i++)
            {
                result += a[i] ^ b[i];
            }
            result = binaryToHexa(result);
            return result.PadLeft(8, '0');
        }

        public void GenerateKey()
        {
            int j = 0;
            for (int i = 0; i < P.Length; i++)
            {
                P[i] = xor(P[i], (key.Skip(j).Take(j + 8)).ToString());
                j = (j + 8) % key.Length;
            }
        }

        public string addBin(string a, string b)
        {
            int t1 = Convert.ToInt32(a, 16);
            int t2 = Convert.ToInt32(b, 16);
            int total = t1 + t2;
            return total.ToString("X");
        }
    }
}
