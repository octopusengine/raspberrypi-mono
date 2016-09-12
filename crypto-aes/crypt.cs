using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;


namespace CryptFile
{
	 class AES

    {
        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }


        public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };


            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (RijndaelManaged AES = new RijndaelManaged())
                    {
                        AES.KeySize = 256;
                        AES.BlockSize = 128;

                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);

                        AES.Mode = CipherMode.CBC;

                        using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                            cs.Close();
                        }
                        decryptedBytes = ms.ToArray();
                    }
                }

            }

            catch { }

            return decryptedBytes;
        }

        public string EncryptText(string input, string password)
        {
            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }

        public string DecryptText(string input, string password)
        {
            // Get the bytes of the string
            byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            string result = null;

            try
            {
                result = Encoding.UTF8.GetString(bytesDecrypted);
            }

            catch { }
            return result;
        }


        public void EncryptFile(string fileInput, string fileEncrypted, string password)
        {            
            byte[] bytesToBeEncrypted = File.ReadAllBytes(fileInput);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);
            
            File.WriteAllBytes(fileEncrypted, bytesEncrypted);
        }



        public void DecryptFile(string fileEncrypted, string fileOutput, string password)
        {            
            byte[] bytesToBeDecrypted = File.ReadAllBytes(fileEncrypted);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            try
            {
                File.WriteAllBytes(fileOutput, bytesDecrypted);
            }
            catch { }

        }
    }


    class Program
    {
        static void ASCII_label()
        {
            // http://patorjk.com/software/taag/#p=display&f=Standard&t=CryptFile%0A%0Aby%20%0A%0ACode%2021
            Console.WriteLine("");
            Console.WriteLine("   ____                  _   _____ _ _      ");
            Console.WriteLine("  / ___|_ __ _   _ _ __ | |_|  ___(_) | ___ ");
            Console.WriteLine(" | |   | '__| | | | '_ \\| __| |_  | | |/ _ \\");
            Console.WriteLine(" | |___| |  | |_| | |_) | |_|  _| | | |  __/");
            Console.WriteLine("  \\____|_|   \\__, | .__/ \\__|_|   |_|_|\\___|");
            Console.WriteLine(" | |__  _   _|___/|_|                       ");
            Console.WriteLine(" | '_ \\| | | |                              ");
            Console.WriteLine(" | |_) | |_| |                              ");
            Console.WriteLine(" |_.__/ \\__, |                              ");
            Console.WriteLine("   ____ |___/    _        ____  _           ");
            Console.WriteLine("  / ___|___   __| | ___  |___ \\/ |          ");
            Console.WriteLine(" | |   / _ \\ / _` |/ _ \\   __) | |          ");
            Console.WriteLine(" | |__| (_) | (_| |  __/  / __/| |          ");
            Console.WriteLine("  \\____\\___/ \\__,_|\\___| |_____|_|          ");
            Console.WriteLine(" ");
            Console.WriteLine(" & octopusengine.eu for Linux");
            Console.WriteLine(" bonum.comunae@volny.cz");
        }
        
        
        static void help()
        {
            ASCII_label();
            Console.WriteLine("The program is designed for encrypt and decrypt files.");
            Console.WriteLine("Extension * .aes  is intended only for encrypted files > Software decide if you want to encrypt or decrypt.");
            Console.WriteLine("");
            Console.WriteLine("Remember this password, otherwise you will lose the original data!");
            Console.WriteLine("");
            Console.WriteLine("Start program:");
            Console.WriteLine("");
            Console.WriteLine("CryptFile [*.*] | YOUR_FILE.*");
            Console.WriteLine("");
            Console.WriteLine("The result is a file YOUR_FILE.aes.");
            
            Console.WriteLine("");
            Console.ReadKey();
            return;
        }
        
        static void Main(string[] args)
        {
            
            string soubor = "";
            //string pripona = "";

            if (args.Length >= 1)
            {
                if ((args[0].ToString() == "help") || (args[0].ToString() == "HELP"))
                {
                    help();
                    return;
                }
            }

            ASCII_label();
            

            if (args.Length > 0) soubor = args[0].ToString();
            else
            {
                Console.WriteLine("File: ");
                soubor = Console.ReadLine();
                Console.WriteLine("");
            }

            if (!File.Exists(soubor))
            {
                Console.WriteLine("File does not exist!");
                Console.ReadKey();
                return;
            }
                

                // desifrovani
                if (soubor.IndexOf(".aes") != -1)
                {
                    Console.WriteLine("Decpryt file: " + soubor);
                    Console.WriteLine("");
                    Console.WriteLine("password: ");
                    string password = Console.ReadLine();
                    string souborVystup = soubor.Substring(0, soubor.Length - 4);                    
                    AES aes = new AES();
                    aes.DecryptFile(soubor, souborVystup, password);
                }
                // sifrovani
                else
                {
                    Console.WriteLine("Encpryt file: " + soubor);
                    Console.WriteLine("");
                    Console.WriteLine("password: ");
                    string password = Console.ReadLine();
                    string souborVystup = soubor + ".aes";                    
                    AES aes = new AES();
                    aes.EncryptFile(soubor, souborVystup, password);
                }
            
        }
    }
}
