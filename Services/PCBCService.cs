using System;

public static class PCBC
{
    private const int BLOCK_SIZE = 16;
    // Initialization Vector (IV) - in a real app, this should be random and sent with the message
    private static readonly byte[] IV = new byte[BLOCK_SIZE]; 

    public static byte[] Encrypt(byte[] data, Func<byte[], byte[]> encryptBlock)
    {
        byte[] output = new byte[data.Length];
        byte[] prevPlaintext = new byte[BLOCK_SIZE];
        byte[] prevCiphertext = new byte[BLOCK_SIZE];
        
        // Initialize with IV
        Array.Copy(IV, prevCiphertext, BLOCK_SIZE);

        for (int i = 0; i < data.Length; i += BLOCK_SIZE)
        {
            byte[] currentPlaintext = new byte[BLOCK_SIZE];
            Array.Copy(data, i, currentPlaintext, 0, BLOCK_SIZE);

            // PCBC Logic: P_i XOR (P_{i-1} XOR C_{i-1})
            byte[] blockToEncrypt = new byte[BLOCK_SIZE];
            for (int j = 0; j < BLOCK_SIZE; j++)
            {
                blockToEncrypt[j] = (byte)(currentPlaintext[j] ^ (prevPlaintext[j] ^ prevCiphertext[j]));
            }

            byte[] currentCiphertext = encryptBlock(blockToEncrypt);
            Array.Copy(currentCiphertext, 0, output, i, BLOCK_SIZE);

            // Update history for next block
            prevPlaintext = currentPlaintext;
            prevCiphertext = currentCiphertext;
        }

        return output;
    }

    public static byte[] Decrypt(byte[] data, Func<byte[], byte[]> decryptBlock)
    {
        byte[] output = new byte[data.Length];
        byte[] prevPlaintext = new byte[BLOCK_SIZE];
        byte[] prevCiphertext = new byte[BLOCK_SIZE];

        Array.Copy(IV, prevCiphertext, BLOCK_SIZE);

        for (int i = 0; i < data.Length; i += BLOCK_SIZE)
        {
            byte[] currentCiphertext = new byte[BLOCK_SIZE];
            Array.Copy(data, i, currentCiphertext, 0, BLOCK_SIZE);

            // Decrypt the block first
            byte[] decryptedBlock = decryptBlock(currentCiphertext);

            // PCBC Logic: P_i = D(C_i) XOR (P_{i-1} XOR C_{i-1})
            byte[] currentPlaintext = new byte[BLOCK_SIZE];
            for (int j = 0; j < BLOCK_SIZE; j++)
            {
                currentPlaintext[j] = (byte)(decryptedBlock[j] ^ (prevPlaintext[j] ^ prevCiphertext[j]));
            }

            Array.Copy(currentPlaintext, 0, output, i, BLOCK_SIZE);

            // Update history for next block
            prevPlaintext = currentPlaintext;
            prevCiphertext = currentCiphertext;
        }

        return output;
    }
}