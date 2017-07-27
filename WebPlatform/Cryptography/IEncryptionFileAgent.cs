using System.IO; 

namespace WebPlatform.Cryptography
{
    public interface IEncryptionFileAgent
    {
        Stream DecryptFile(Stream inputStream, char[] passPhrase);
        Stream EncryptFile(Stream inputStream, char[] passPhrase, bool armor, bool withIntegrityCheck);
         
    }
}
