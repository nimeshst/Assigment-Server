
using System.Security.Cryptography;
using System.Text;

public class EncryptDecrypt
{
    private readonly Aes _aes;

    // public readonly byte[] Key = Encoding.UTF8.GetBytes("123456789012345678901234567890##");

    // public readonly byte[] IV = Encoding.UTF8.GetBytes("1234567890123456");

    public EncryptDecrypt(string Key, string IV)
    {
        _aes = Aes.Create();
        _aes.Key = Encoding.UTF8.GetBytes(Key); 
        _aes.IV = Encoding.UTF8.GetBytes(IV);
    }

    public byte[] Encrypt(string plainString)
    {
        using var encryptor = _aes.CreateEncryptor();
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainString);
        return encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
    }
    public string Decrypt(byte[] encryptedBytes)
    {
        using var decryptor = _aes.CreateDecryptor();
        byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
        return Encoding.UTF8.GetString(decryptedBytes);
    }
}
