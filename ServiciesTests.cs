using caffetogo.Controllers;
using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using caffetogo.Models;
using Microsoft.AspNetCore.Mvc;

namespace caffetogo.Tests
{
    [TestFixture]
    public class ServiciesTest
    {
        [Test]
        public void ProductCreateConverterTest()
        {
            //Arrange
            ProductView productView = new ProductView(); //Behívjuk a ProductView függvényünket
            productView.Id = 1; // Integer adattagját állítjuk be
            productView.item = "koffeinbomba"; // String adattagját állítjuk be
            productView.price = 69696; // Integer adattagját állítjuk be
            //Act
            Product product = Servicies.ProductCreateConverter(productView); // Példányosítjuk a függvényt
            //Assert
            Assert.AreEqual(product.Id, productView.Id); // Összehasonlítjuk a megkapott Id adatot a megadottal
            Assert.AreEqual(product.item, productView.item); // Összehasonlítjuk a megkapott item adatot a megadottal
            Assert.AreEqual(product.price, productView.price); // Összehasonlítjuk a megkapott price adatot a megadottal
        }

        [Test]
        public void UserCreateConverterTest()
        {
            //Arrange
            UserView userviewobj = new UserView(); //Behívjuk a UserView függvényünket
            userviewobj.Id = 1; // Integer adattagját állítjuk be
            userviewobj.Email = "teszt@email.com";// String(email) adattagját állítjuk be
            userviewobj.Activity = new DateTime(2013, 6, 1, 12, 32, 30); // DateTime adattagját állítjuk be
            //Act
            User user = Servicies.UserCreateConverter(userviewobj); // Példányosítjuk a függvényt
            //Assert
            Assert.AreEqual(user.Id, userviewobj.Id); // Összehasonlítjuk a megkapott Id adatot a megadottal
            Assert.AreEqual(user.Email, userviewobj.Email); // Összehasonlítjuk a megkapott Email adatot a megadottal
            Assert.AreEqual(user.Activity, userviewobj.Activity); // Összehasonlítjuk a megkapott Activity adatot a megadottal
        }

    }


}
