using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;
[TestFixture]
public class AdminTesztekTest
{
    private IWebDriver driver;
    public IDictionary<string, object> vars { get; private set; }
    private IJavaScriptExecutor js;
    [SetUp]// A Browser driver-t elõkészítem használatra
    public void SetUp()
    {
        driver = new ChromeDriver();
        js = (IJavaScriptExecutor)driver;
        vars = new Dictionary<string, object>();
    }
    [TearDown] // A TearDown-al automatikusan bezárhatjuk a böngészõt
    protected void TearDown()
    {
        driver.Quit();
    }
    [Test]
    public void adminBuyAddUpdateDel()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavigálunk a cél oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagyságú ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click();// Megkeressük Id alapján a Belépés gombot, és rákattintunk
        driver.FindElement(By.Name("email")).Click(); // Megkeressük az email mezõt, majd belekattintunk.
        driver.FindElement(By.Name("email")).SendKeys("Admin@admin.hu");// Az email mezõbe az admin email-ét beírjuk. 
        driver.FindElement(By.Name("password")).Click();// Megkeressük az jelszó mezõt, majd belekattintunk.
        driver.FindElement(By.Name("password")).SendKeys("Admin");// A jelszó mezõbe az admin jelszavát beírjuk. 
        driver.FindElement(By.Id("login-submit")).Click();// Megkeressük a Belépés gombot, és rákattintunk
        driver.FindElement(By.CssSelector("table:nth-child(2) .ht:nth-child(3) > .headlink")).Click(); // A Buy gombot megkeressük és rákattintunk
        driver.FindElement(By.Name("UserId")).Click(); // Megkeressük a UserId elemet és belekattintunk
        driver.FindElement(By.Name("UserId")).SendKeys("1005"); // Kitöltjük egy létezõ UserId-vel
        driver.FindElement(By.Name("Items")).Click(); // Megkeressük a Items elemet és belekattintunk
        driver.FindElement(By.Name("Items")).SendKeys("2006,2007");// Kitöltjük tetszõleges,létezõ termék Id-kal
        driver.FindElement(By.CssSelector("div:nth-child(9) .sendlink > input")).Click();// Megkeressük és rányomunk a Create gombra
        driver.FindElement(By.CssSelector(".main:nth-child(10) tr:nth-child(21) > td:nth-child(3) > input")).Click();// Megkeressük az Items-et amit csináltunk és belekattintunk
        driver.FindElement(By.CssSelector(".main:nth-child(10) tr:nth-child(21) > td:nth-child(3) > input")).SendKeys("2006,2007,2009");// Átírjuk az Items-et
        driver.FindElement(By.CssSelector(".main:nth-child(10) tr:nth-child(21) > .sendlink:nth-child(5) > input")).Click();// Megkeressük és rányomunk az Update gombra hogy frissítse
        driver.FindElement(By.CssSelector("tr:nth-child(21) > .sendlink:nth-child(4) > input")).Click(); // Megkeressük és megnyomjuk a Delete gombot, hogy töröljük a most készített kosarat.
        driver.FindElement(By.LinkText("Index")).Click(); // Visszatérünk az Indexre a lap tetején található Index gombbal.
    }
    [Test]
    public void adminProductAddUpdateDel()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavigálunk a cél oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagyságú ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click();// Megkeressük Id alapján a Belépés gombot, és rákattintunk
        driver.FindElement(By.Name("email")).Click(); // Megkeressük az email mezõt, majd belekattintunk.
        driver.FindElement(By.Name("email")).SendKeys("Admin@admin.hu");// Az email mezõbe az admin email-ét beírjuk. 
        driver.FindElement(By.Name("password")).Click();// Megkeressük az jelszó mezõt, majd belekattintunk.
        driver.FindElement(By.Name("password")).SendKeys("Admin");// A jelszó mezõbe az admin jelszavát beírjuk. 
        driver.FindElement(By.Id("login-submit")).Click();// Megkeressük a Belépés gombot, és rákattintunk
        driver.FindElement(By.LinkText("Product")).Click();// Megkeressük és rákattintunk a Product gombra, hogy a megfelelõ helyre vezessen
        driver.FindElement(By.Name("item")).Click();// Lokalizáljuk az item elemet, és belekattintunk
        driver.FindElement(By.Name("item")).SendKeys("teszt_item"); // Bevisszük a tetszõleges értéket az item nevének.
        driver.FindElement(By.Name("price")).Click();// Lokalizáljuk a price elemet, és belekattintunk
        driver.FindElement(By.Name("price")).SendKeys("3210");// Bevisszük a tetszõleges értéket az item árának.
        driver.FindElement(By.Name("Pictures")).Click();// Lokalizáljuk a Pictures elemet, és rákattintunk
        driver.FindElement(By.Name("Pictures")).SendKeys("X:\\caffetogo\\caffetogo\\tesztkep.jpeg"); // Feltöltünk egy képet
        driver.FindElement(By.CssSelector("tr:nth-child(2) > .sendlink:nth-child(4) > input")).Click(); // Létrehozzuk a termékünket a Create gombbal
        {
            var element = driver.FindElement(By.CssSelector("tr:nth-child(42) > td:nth-child(3) > input"));
            Actions builder = new Actions(driver);
            builder.MoveToElement(element).ClickAndHold().Perform();
        }
        {
            var element = driver.FindElement(By.CssSelector("tr:nth-child(42) > td:nth-child(3) > input"));
            Actions builder = new Actions(driver);
            builder.MoveToElement(element).Perform();
        }
        {
            var element = driver.FindElement(By.CssSelector("tr:nth-child(42) > td:nth-child(3) > input"));
            Actions builder = new Actions(driver);
            builder.MoveToElement(element).Release().Perform();
        }
        driver.FindElement(By.CssSelector("tr:nth-child(42) > td:nth-child(3) > input")).Click();// Megkeressük az új termék árát, és rákattintunk
        driver.FindElement(By.CssSelector("tr:nth-child(42) > td:nth-child(3) > input")).SendKeys("3230");// Beírunk egy tetszõleges árat
        driver.FindElement(By.CssSelector("tr:nth-child(42) > .sendlink:nth-child(6) > input")).Click(); // Az Update gomb megkeresésével, és megnyomásával frissítettük az árat.
        driver.FindElement(By.CssSelector("tr:nth-child(42) > .sendlink:nth-child(5) > input")).Click(); // Megkeressük a Delete gombot, megnyomjuk hogy törlõdjön az új termék
        driver.FindElement(By.LinkText("Index")).Click(); // Visszatérünk az Indexre a lap tetején található Index gombbal.
    }
    [Test]
    public void adminUserAddUpdateDel()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavigálunk a cél oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagyságú ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click();// Megkeressük Id alapján a Belépés gombot, és rákattintunk
        driver.FindElement(By.Name("email")).Click(); // Megkeressük az email mezõt, majd belekattintunk.
        driver.FindElement(By.Name("email")).SendKeys("Admin@admin.hu");// Az email mezõbe az admin email-ét beírjuk. 
        driver.FindElement(By.Name("password")).Click();// Megkeressük az jelszó mezõt, majd belekattintunk.
        driver.FindElement(By.Name("password")).SendKeys("Admin");// A jelszó mezõbe az admin jelszavát beírjuk. 
        driver.FindElement(By.Id("login-submit")).Click();// Megkeressük a Belépés gombot, és rákattintunk
        driver.FindElement(By.Name("Email")).Click(); // A felhasználóknál megkeressük az Email elemet
        driver.FindElement(By.Name("Email")).SendKeys("user1@user.hu"); // Beleírunk egy új Emailt
        driver.FindElement(By.Name("Password")).Click(); // A felhasználóknál megkeressük a Jelszó elemet és rákattintunk
        driver.FindElement(By.Name("Password")).SendKeys("user"); // Megadunk neki egy Jelszavat
        driver.FindElement(By.CssSelector("div:nth-child(3) .sendlink > input")).Click(); // Rákattintunk a Create gombra
        driver.FindElement(By.Name("Email")).Click();// A felhasználóknál megkeressük az Email elemet és rákattintunk
        driver.FindElement(By.Name("Email")).SendKeys("user2@user.hu");// Beleírunk egy új Emailt
        driver.FindElement(By.Name("Password")).Click();// A felhasználóknál megkeressük a Jelszó elemet és rákattintunk
        driver.FindElement(By.Name("Password")).SendKeys("user"); // Megadunk neki egy Jelszavat
        driver.FindElement(By.CssSelector("div:nth-child(3) .sendlink > input")).Click();// Rákattintunk a Create gombra
        driver.FindElement(By.CssSelector("tr:nth-child(21) > td:nth-child(4) > input")).Click(); // Lokalizáljuk az Activity elemet
        driver.FindElement(By.CssSelector("tr:nth-child(21) > td:nth-child(4) > input")).SendKeys("2022-03-03T19:05"); // Átírjuk az Activity-t
        driver.FindElement(By.CssSelector("body")).Click(); // Updateljük hogy bekerüljön a frissített Activity dátum
        driver.FindElement(By.CssSelector(".main:nth-child(4) tr:nth-child(21) > .sendlink:nth-child(6) > input")).Click();//Megkeressül a Delete gombot a kreált felhasználó mellett, és megnyomjuk
        driver.FindElement(By.CssSelector(".main:nth-child(4) tr:nth-child(21) > .sendlink:nth-child(5) > input")).Click();//Megkeressül a Delete gombot a másik kreált felhasználó mellett, és megnyomjuk
        driver.FindElement(By.LinkText("Index")).Click(); // Visszatérünk az Indexre a lap tetején található Index gombbal.
    }
}
