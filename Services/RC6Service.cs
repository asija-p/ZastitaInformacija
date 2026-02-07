using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ZastitaInformacija_19322.Services;

namespace ZastitaInformacija_19322.Services
{
    public class RC6Service
    {

        private const int W = 32;          // word size in bits
        private const int R = 20;          // number of rounds
        private const int BLOCK_SIZE = 16; // 16 bytes per block

        private const uint Pw = 0xB7E15163;
        private const uint Qw = 0x9E3779B9;

        private readonly byte[] KEY = Encoding.UTF8.GetBytes("mysecretpassword"); // Fixed internal key

        private uint[] S;

        // Rotate left
        private static uint RotL(uint x, int y) => (x << (y & 31)) | (x >> (32 - (y & 31)));
        // Rotate right
        private static uint RotR(uint x, int y) => (x >> (y & 31)) | (x << (32 - (y & 31)));

        // Key expansion
        private uint[] GenerateSubkeys(byte[] key)
        {
            int u = W / 8;
            int c = key.Length / u;
            if (key.Length % u != 0) c++;

            uint[] L = new uint[c];
            for (int i = 0; i < key.Length; i++)
            {
                L[i / 4] |= (uint)key[i] << (8 * (i % 4));
            }

            int t = 2 * R + 4;
            S = new uint[t];
            S[0] = Pw;
            for (int i = 1; i < t; i++) S[i] = S[i - 1] + Qw;

            uint A = 0, B = 0;
            int iS = 0, iL = 0;
            int v = 3 * Math.Max(t, c);
            for (int i = 0; i < v; i++)
            {
                A = S[iS] = RotL(S[iS] + A + B, 3);
                B = L[iL] = RotL(L[iL] + A + B, (int)(A + B));
                iS = (iS + 1) % t;
                iL = (iL + 1) % c;
            }

            return S;
        }

        private byte[] EncryptBlock(byte[] block)
        {
            uint A = BitConverter.ToUInt32(block, 0);
            uint B = BitConverter.ToUInt32(block, 4);
            uint C = BitConverter.ToUInt32(block, 8);
            uint D = BitConverter.ToUInt32(block, 12);

            B += S[0];
            D += S[1];

            for (int i = 1; i <= R; i++)
            {
                uint t = RotL(B * (2 * B + 1), 5);
                uint u = RotL(D * (2 * D + 1), 5);
                A = RotL(A ^ t, (int)u) + S[2 * i];
                C = RotL(C ^ u, (int)t) + S[2 * i + 1];

                uint tmp = A; A = B; B = C; C = D; D = tmp;
            }

            A += S[2 * R + 2];
            C += S[2 * R + 3];

            byte[] result = new byte[BLOCK_SIZE];
            Array.Copy(BitConverter.GetBytes(A), 0, result, 0, 4);
            Array.Copy(BitConverter.GetBytes(B), 0, result, 4, 4);
            Array.Copy(BitConverter.GetBytes(C), 0, result, 8, 4);
            Array.Copy(BitConverter.GetBytes(D), 0, result, 12, 4);
            return result;
        }

        private byte[] DecryptBlock(byte[] block)
        {
            uint A = BitConverter.ToUInt32(block, 0);
            uint B = BitConverter.ToUInt32(block, 4);
            uint C = BitConverter.ToUInt32(block, 8);
            uint D = BitConverter.ToUInt32(block, 12);

            C -= S[2 * R + 3];
            A -= S[2 * R + 2];

            for (int i = R; i >= 1; i--)
            {
                uint tmp = D; D = C; C = B; B = A; A = tmp;
                uint t = RotL(B * (2 * B + 1), 5);
                uint u = RotL(D * (2 * D + 1), 5);
                C = RotR(C - S[2 * i + 1], (int)t) ^ u;
                A = RotR(A - S[2 * i], (int)u) ^ t;
            }

            D -= S[1];
            B -= S[0];

            byte[] result = new byte[BLOCK_SIZE];
            Array.Copy(BitConverter.GetBytes(A), 0, result, 0, 4);
            Array.Copy(BitConverter.GetBytes(B), 0, result, 4, 4);
            Array.Copy(BitConverter.GetBytes(C), 0, result, 8, 4);
            Array.Copy(BitConverter.GetBytes(D), 0, result, 12, 4);
            return result;
        }

        private byte[] Pad(byte[] data)
        {
            int padLen = BLOCK_SIZE - (data.Length % BLOCK_SIZE);
            byte[] padded = new byte[data.Length + padLen];
            Array.Copy(data, padded, data.Length);
            padded[data.Length] = 0x80; // PKCS7-like padding
            return padded;
        }

        private byte[] RemovePadding(byte[] data)
        {
            int i = data.Length - 1;
            while (i >= 0 && data[i] == 0) i--;
            if (i >= 0 && data[i] == 0x80) i--;
            byte[] result = new byte[i + 1];
            Array.Copy(data, result, result.Length);
            return result;
        }

        public byte[] Encrypt(byte[] data, bool usePCBC = false)
        {
            S = GenerateSubkeys(KEY);
            byte[] padded = Pad(data);

            if (!usePCBC)
            {
                byte[] output = new byte[padded.Length];
                for (int i = 0; i < padded.Length; i += BLOCK_SIZE)
                {
                    byte[] block = new byte[BLOCK_SIZE];
                    Array.Copy(padded, i, block, 0, BLOCK_SIZE);
                    Array.Copy(EncryptBlock(block), 0, output, i, BLOCK_SIZE);
                }
                return output;
            }
            else
            {
                // Ensure your PCBC class also returns byte[]
                return PCBC.Encrypt(padded, EncryptBlock);
            }
        }

        public byte[] Decrypt(byte[] data, bool usePCBC = false)
        {
            S = GenerateSubkeys(KEY);

            if (!usePCBC)
            {
                byte[] output = new byte[data.Length];
                for (int i = 0; i < data.Length; i += BLOCK_SIZE)
                {
                    byte[] block = new byte[BLOCK_SIZE];
                    Array.Copy(data, i, block, 0, BLOCK_SIZE);
                    Array.Copy(DecryptBlock(block), 0, output, i, BLOCK_SIZE);
                }
                return RemovePadding(output);
            }
            else
            {
                byte[] decrypted = PCBC.Decrypt(data, DecryptBlock);
                return RemovePadding(decrypted);
            }
        }
    }
}
