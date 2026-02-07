using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZastitaInformacija_19322.Services
{
    public class SHA1Service
    {
        private uint[] H = new uint[5];
        private byte[] MessageBlock = new byte[64];
        private int MessageBlockIndex;
        private uint LengthLow, LengthHigh;
        private bool Computed, Corrupted;

        public SHA1Service()
        {
            Reset();
        }

        public void Reset()
        {
            LengthLow = LengthHigh = 0;
            MessageBlockIndex = 0;

            H[0] = 0x67452301;
            H[1] = 0xEFCDAB89;
            H[2] = 0x98BADCFE;
            H[3] = 0x10325476;
            H[4] = 0xC3D2E1F0;

            Computed = false;
            Corrupted = false;
        }

        public void Update(byte[] data, int offset = 0, int count = -1)
        {
            if (count == -1) count = data.Length - offset;
            if (Computed || Corrupted) throw new InvalidOperationException("Cannot update after computing hash.");

            for (int i = offset; i < offset + count; i++)
            {
                MessageBlock[MessageBlockIndex++] = data[i];

                LengthLow += 8;
                if (LengthLow == 0)
                {
                    LengthHigh++;
                    if (LengthHigh == 0)
                    {
                        Corrupted = true; // Message too long
                    }
                }

                if (MessageBlockIndex == 64)
                    ProcessMessageBlock();
            }
        }

        public void Update(Stream stream)
        {
            byte[] buffer = new byte[4096];
            int read;
            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                Update(buffer, 0, read);
            }
        }

        public byte[] ComputeHash()
        {
            if (!Computed)
            {
                PadMessage();
                Computed = true;
            }

            byte[] hash = new byte[20]; // SHA-1 produces 160-bit hash (20 bytes)
            for (int i = 0; i < 5; i++)
            {
                hash[i * 4] = (byte)((H[i] >> 24) & 0xFF);
                hash[i * 4 + 1] = (byte)((H[i] >> 16) & 0xFF);
                hash[i * 4 + 2] = (byte)((H[i] >> 8) & 0xFF);
                hash[i * 4 + 3] = (byte)(H[i] & 0xFF);
            }

            return hash;
        }

        public string ComputeHashString()
        {
            var hash = ComputeHash();
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        private void ProcessMessageBlock()
        {
            uint[] K = { 0x5A827999, 0x6ED9EBA1, 0x8F1BBCDC, 0xCA62C1D6 };
            uint[] W = new uint[80];

            for (int t = 0; t < 16; t++)
            {
                W[t] = ((uint)MessageBlock[t * 4] << 24) |
                       ((uint)MessageBlock[t * 4 + 1] << 16) |
                       ((uint)MessageBlock[t * 4 + 2] << 8) |
                       ((uint)MessageBlock[t * 4 + 3]);
            }

            for (int t = 16; t < 80; t++)
            {
                W[t] = CircularShift(1, W[t - 3] ^ W[t - 8] ^ W[t - 14] ^ W[t - 16]);
            }

            uint A = H[0], B = H[1], C = H[2], D = H[3], E = H[4];

            for (int t = 0; t < 20; t++)
            {
                uint temp = CircularShift(5, A) + ((B & C) | (~B & D)) + E + W[t] + K[0];
                E = D;
                D = C;
                C = CircularShift(30, B);
                B = A;
                A = temp;
            }

            for (int t = 20; t < 40; t++)
            {
                uint temp = CircularShift(5, A) + (B ^ C ^ D) + E + W[t] + K[1];
                E = D;
                D = C;
                C = CircularShift(30, B);
                B = A;
                A = temp;
            }

            for (int t = 40; t < 60; t++)
            {
                uint temp = CircularShift(5, A) + ((B & C) | (B & D) | (C & D)) + E + W[t] + K[2];
                E = D;
                D = C;
                C = CircularShift(30, B);
                B = A;
                A = temp;
            }

            for (int t = 60; t < 80; t++)
            {
                uint temp = CircularShift(5, A) + (B ^ C ^ D) + E + W[t] + K[3];
                E = D;
                D = C;
                C = CircularShift(30, B);
                B = A;
                A = temp;
            }

            H[0] += A; H[1] += B; H[2] += C; H[3] += D; H[4] += E;

            MessageBlockIndex = 0;
        }

        private void PadMessage()
        {
            MessageBlock[MessageBlockIndex++] = 0x80;

            if (MessageBlockIndex > 56)
            {
                while (MessageBlockIndex < 64)
                    MessageBlock[MessageBlockIndex++] = 0;
                ProcessMessageBlock();
            }

            while (MessageBlockIndex < 56)
                MessageBlock[MessageBlockIndex++] = 0;

            // Append length in bits
            for (int i = 0; i < 4; i++)
                MessageBlock[MessageBlockIndex++] = (byte)((LengthHigh >> (24 - i * 8)) & 0xFF);
            for (int i = 0; i < 4; i++)
                MessageBlock[MessageBlockIndex++] = (byte)((LengthLow >> (24 - i * 8)) & 0xFF);

            ProcessMessageBlock();
        }

        private uint CircularShift(int bits, uint word)
        {
            return (word << bits) | (word >> (32 - bits));
        }
    }

}
