using caffetogo.Models;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace caffetogo.Controllers
{
    public class Servicies
    {
        public static IEnumerable<UserView> UserConverter(IEnumerable<User> user)
        {
            List<UserView> userview = new List<UserView>();
            foreach (var item in user)
            {
                UserView userviewobj = new UserView();
                userviewobj.Id = item.Id;
                userviewobj.Email = item.Email;
                userviewobj.Activity = item.Activity;
                try
                {
                    using (Aes aesAlg = Aes.Create())
                    {
                        aesAlg.Key = new byte[] { 24, 85, 236, 168, 68, 66, 17, 60, 1, 67, 106, 129, 239, 131, 175, 181, 161, 191, 160, 95, 253, 231, 237, 137, 131, 176, 189, 172, 166, 210, 43, 82 };
                        aesAlg.IV = new byte[] { 251, 55, 226, 56, 41, 220, 232, 9, 51, 114, 106, 151, 185, 119, 186, 209 };
                        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                        using (MemoryStream msDecrypt = new MemoryStream(item.Password))
                        {
                            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                                {
                                    userviewobj.Password = srDecrypt.ReadToEnd();
                                }
                            }
                        }
                    }
                }
                catch
                {
                    userviewobj.Password = "none";
                }
                userview.Add(userviewobj);
            }
            IEnumerable<UserView> users = userview;
            return users;
        }
        public static User UserCreateConverter(UserView userviewobj)
        {
            User user = new User();
            user.Id = userviewobj.Id;
            user.Email = userviewobj.Email;
            user.Activity = userviewobj.Activity;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = new byte[] { 24, 85, 236, 168, 68, 66, 17, 60, 1, 67, 106, 129, 239, 131, 175, 181, 161, 191, 160, 95, 253, 231, 237, 137, 131, 176, 189, 172, 166, 210, 43, 82 };
                aesAlg.IV = new byte[] { 251, 55, 226, 56, 41, 220, 232, 9, 51, 114, 106, 151, 185, 119, 186, 209 };
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(userviewobj.Password);
                        }
                        user.Password = msEncrypt.ToArray();
                    }
                }
            }
            return user;
        }
        public static Product ProductCreateConverter(ProductView productView)
        {
            Product product = new Product();
            product.Id = productView.Id;
            product.item = productView.item;
            product.price = productView.price;
            return product;
        }
    }
}
