using caffetogo.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public static IEnumerable<ProductView> ProductConverter(IEnumerable<Product> products)
        {
            List<ProductView> productview = new List<ProductView>();
            foreach (var item in products)
            {
                ProductView productView = new ProductView();
                productView.Id = item.Id;
                productView.item = item.item;
                productView.price = item.price;
                try
                {
                    using (var ms = new MemoryStream(item.Pictures))
                    {
                        productView.Pictures = Image.FromStream(ms);
                    }
                }
                catch
                {
                    productView.Pictures = null;
                }
                productview.Add(productView);
            }
            IEnumerable<ProductView> product = productview;
            return product;
        }
        public static Product ProductCreateConverter(ProductView productView)
        {
            Product product = new Product();
            product.Id = productView.Id;
            product.item = productView.item;
            product.price = productView.price;
            if (productView.Pictures != null)
            {
                using (var ms = new MemoryStream())
                {
                    productView.Pictures.Save(ms, productView.Pictures.RawFormat);
                    product.Pictures = ms.ToArray();
                }
            }
            else
            {
                product.Pictures = null;
            }
            return product;
        }
    }
}
