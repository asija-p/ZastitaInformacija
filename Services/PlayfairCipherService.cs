using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace ZastitaInformacija_19322.Services
{
    public class PlayfairCipherService
    {
        private const string FIXED_KEY = "monarchy";
        public string Encrypt(string plainText)
        {
            string key = FIXED_KEY;
            char[,] keyTable = new char[5, 5];

            RemoveSpaces(ref key);
            ToLowerCase(ref key);

            ToLowerCase(ref plainText);
            RemoveSpaces(ref plainText);
            Prepare(ref plainText);

            GenerateKeyTable(ref key, keyTable);
            EncryptText(ref plainText, keyTable);

            return plainText;
        }

        public string Decrypt(string cipherText)
        {
            string key = FIXED_KEY;
            char[,] keyTable = new char[5, 5];

            ToLowerCase(ref key);
            RemoveSpaces(ref key);

            ToLowerCase(ref cipherText);
            RemoveSpaces(ref cipherText);

            GenerateKeyTable(ref key, keyTable);
            DecryptText(ref cipherText, keyTable);

            return cipherText;
        }

        private static void ToLowerCase(ref string plain)
        {
            char[] arr = plain.ToCharArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] >= 'A' && arr[i] <= 'Z')
                    arr[i] = (char)(arr[i] + 32);
            }
            plain = new string(arr);
        }

        private static void RemoveSpaces(ref string plain)
        {
            string temp = "";
            for (int i = 0; i < plain.Length; i++)
            {
                if (plain[i] != ' ')
                    temp += plain[i];
            }
            plain = temp;
        }

        private static void GenerateKeyTable(ref string key, char[,] keyT)
        {
            int[] hash = new int[26];

            for (int i = 0; i < key.Length; i++)
            {
                if (key[i] != 'j')
                    hash[key[i] - 'a'] = 2;
            }

            hash['j' - 'a'] = 1;

            int row = 0, col = 0;

            for (int i = 0; i < key.Length; i++)
            {
                if (hash[key[i] - 'a'] == 2)
                {
                    hash[key[i] - 'a'] = 1;
                    keyT[row, col++] = key[i];

                    if (col == 5)
                    {
                        row++;
                        col = 0;
                    }
                }
            }

            for (int i = 0; i < 26; i++)
            {
                if (hash[i] == 0)
                {
                    keyT[row, col++] = (char)(i + 'a');

                    if (col == 5)
                    {
                        row++;
                        col = 0;
                    }
                }
            }
        }

        private static void Search(char[,] keyT, char a, char b, int[] arr)
        {
            if (a == 'j') a = 'i';
            if (b == 'j') b = 'i';

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (keyT[i, j] == a)
                    {
                        arr[0] = i;
                        arr[1] = j;
                    }
                    else if (keyT[i, j] == b)
                    {
                        arr[2] = i;
                        arr[3] = j;
                    }
                }
            }
        }

        private static void Prepare(ref string str)
        {
            if (str.Length % 2 != 0)
                str += 'z';
        }

        private static void EncryptText(ref string str, char[,] keyT)
        {
            int[] arr = new int[4];
            char[] ch = str.ToCharArray();

            for (int i = 0; i < ch.Length; i += 2)
            {
                Search(keyT, ch[i], ch[i + 1], arr);

                if (arr[0] == arr[2])
                {
                    ch[i] = keyT[arr[0], (arr[1] + 1) % 5];
                    ch[i + 1] = keyT[arr[0], (arr[3] + 1) % 5];
                }
                else if (arr[1] == arr[3])
                {
                    ch[i] = keyT[(arr[0] + 1) % 5, arr[1]];
                    ch[i + 1] = keyT[(arr[2] + 1) % 5, arr[1]];
                }
                else
                {
                    ch[i] = keyT[arr[0], arr[3]];
                    ch[i + 1] = keyT[arr[2], arr[1]];
                }
            }

            str = new string(ch);
        }

        private static void DecryptText(ref string str, char[,] keyT)
        {
            int[] arr = new int[4];
            char[] ch = str.ToCharArray();

            for (int i = 0; i < ch.Length; i += 2)
            {
                Search(keyT, ch[i], ch[i + 1], arr);

                if (arr[0] == arr[2])
                {
                    ch[i] = keyT[arr[0], (arr[1] - 1 + 5) % 5];
                    ch[i + 1] = keyT[arr[0], (arr[3] - 1 + 5) % 5];
                }
                else if (arr[1] == arr[3])
                {
                    ch[i] = keyT[(arr[0] - 1 + 5) % 5, arr[1]];
                    ch[i + 1] = keyT[(arr[2] - 1 + 5) % 5, arr[1]];
                }
                else
                {
                    ch[i] = keyT[arr[0], arr[3]];
                    ch[i + 1] = keyT[arr[2], arr[1]];
                }
            }

            str = new string(ch);
        }

        public byte[] Encrypt(byte[] data)
        {
            // Convert file bytes to a string
            string plainText = Encoding.UTF8.GetString(data);
            // Use your existing string logic
            string encrypted = Encrypt(plainText);
            // Return back as bytes
            return Encoding.UTF8.GetBytes(encrypted);
        }

        public byte[] Decrypt(byte[] data)
        {
            // Convert encrypted bytes to a string
            string cipherText = Encoding.UTF8.GetString(data);
            // Use your existing string logic
            string decrypted = Decrypt(cipherText);
            // Return back as bytes
            return Encoding.UTF8.GetBytes(decrypted);
        }
    }


}
