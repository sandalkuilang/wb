using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Security;
using WebPlatform.Cryptography;

namespace WebPlatform.Configuration
{
    public class EmailSettings : ConfigurationSection
    {
        [ConfigurationProperty("smtpServer")]
        public string SmtpServer
        {
            get
            {
                return (string)this["smtpServer"];
            }
            set
            {
                this["smtpServer"] = value;
            }
        }

        [ConfigurationProperty("mailSender")]
        public string MailSender
        {
            get
            {
                return (string)this["mailSender"];
            }
            set
            {
                this["mailSender"] = value;
            }
        }
        
        [ConfigurationProperty("smtpPort")]
        public string SmtpPort
        {
            get
            {
                return (string)this["smtpPort"];
            }
            set
            {
                this["smtpPort"] = value;
            }
        }
         
        [ConfigurationProperty("userName")]
        public string UserName
        {
            get
            {
                return (string)this["userName"];
            }
            set
            {
                this["userName"] = value;
            }
        }
          
        [ConfigurationProperty("password")]
        public string Password
        {
            get
            {
                return (string)this["password"];
            }
            set
            {
                this["password"] = value;
            }
        }
        
        [ConfigurationProperty("isSSL")]
        public bool IsSSL
        {
            get
            {
                return (bool)this["isSSL"];
            }
            set
            {
                this["isSSL"] = value;
            }
        }
         
        [ConfigurationProperty("key")]
        public string Key
        {
            get
            {
                return (string)this["key"];
            }
            set
            {
                this["key"] = value;
            }
        }

        [ConfigurationProperty("iv")]
        public string IV
        {
            get
            {
                return (string)this["iv"];
            }
            set
            {
                this["iv"] = value;
            }
        }

        public SecureString SecurePassword
        {
            get
            {
                try
                {
                    IEncryptionAgent encryption = new RijndaelEncryption(Key, IV);
                    char[] pass = encryption.Decrypt(this.Password).ToCharArray();
                    SecureString securePassword = new SecureString();

                    foreach (char password in pass)
                        securePassword.AppendChar(password);

                    return securePassword;
                }
                catch (System.Security.Cryptography.CryptographicException)
                {
                    return new SecureString();
                }
            }
        }

        public string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
                throw new ArgumentNullException("securePassword");

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
