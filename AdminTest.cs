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
    [SetUp]// A Browser driver-t el�k�sz�tem haszn�latra
    public void SetUp()
    {
        driver = new ChromeDriver();
        js = (IJavaScriptExecutor)driver;
        vars = new Dictionary<string, object>();
    }
    [TearDown] // A TearDown-al automatikusan bez�rhatjuk a b�ng�sz�t
    protected void TearDown()
    {
        driver.Quit();
    }
    [Test]
    public void adminBuyAddUpdateDel()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavig�lunk a c�l oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagys�g� ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click();// Megkeress�k Id alapj�n a Bel�p�s gombot, �s r�kattintunk
        driver.FindElement(By.Name("email")).Click(); // Megkeress�k az email mez�t, majd belekattintunk.
        driver.FindElement(By.Name("email")).SendKeys("Admin@admin.hu");// Az email mez�be az admin email-�t be�rjuk. 
        driver.FindElement(By.Name("password")).Click();// Megkeress�k az jelsz� mez�t, majd belekattintunk.
        driver.FindElement(By.Name("password")).SendKeys("Admin");// A jelsz� mez�be az admin jelszav�t be�rjuk. 
        driver.FindElement(By.Id("login-submit")).Click();// Megkeress�k a Bel�p�s gombot, �s r�kattintunk
        driver.FindElement(By.CssSelector("table:nth-child(2) .ht:nth-child(3) > .headlink")).Click(); // A Buy gombot megkeress�k �s r�kattintunk
        driver.FindElement(By.Name("UserId")).Click(); // Megkeress�k a UserId elemet �s belekattintunk
        driver.FindElement(By.Name("UserId")).SendKeys("1005"); // Kit�ltj�k egy l�tez� UserId-vel
        driver.FindElement(By.Name("Items")).Click(); // Megkeress�k a Items elemet �s belekattintunk
        driver.FindElement(By.Name("Items")).SendKeys("2006,2007");// Kit�ltj�k tetsz�leges,l�tez� term�k Id-kal
        driver.FindElement(By.CssSelector("div:nth-child(9) .sendlink > input")).Click();// Megkeress�k �s r�nyomunk a Create gombra
        driver.FindElement(By.CssSelector(".main:nth-child(10) tr:nth-child(21) > td:nth-child(3) > input")).Click();// Megkeress�k az Items-et amit csin�ltunk �s belekattintunk
        driver.FindElement(By.CssSelector(".main:nth-child(10) tr:nth-child(21) > td:nth-child(3) > input")).SendKeys("2006,2007,2009");// �t�rjuk az Items-et
        driver.FindElement(By.CssSelector(".main:nth-child(10) tr:nth-child(21) > .sendlink:nth-child(5) > input")).Click();// Megkeress�k �s r�nyomunk az Update gombra hogy friss�tse
        driver.FindElement(By.CssSelector("tr:nth-child(21) > .sendlink:nth-child(4) > input")).Click(); // Megkeress�k �s megnyomjuk a Delete gombot, hogy t�r�lj�k a most k�sz�tett kosarat.
        driver.FindElement(By.LinkText("Index")).Click(); // Visszat�r�nk az Indexre a lap tetej�n tal�lhat� Index gombbal.
    }
    [Test]
    public void adminProductAddUpdateDel()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavig�lunk a c�l oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagys�g� ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click();// Megkeress�k Id alapj�n a Bel�p�s gombot, �s r�kattintunk
        driver.FindElement(By.Name("email")).Click(); // Megkeress�k az email mez�t, majd belekattintunk.
        driver.FindElement(By.Name("email")).SendKeys("Admin@admin.hu");// Az email mez�be az admin email-�t be�rjuk. 
        driver.FindElement(By.Name("password")).Click();// Megkeress�k az jelsz� mez�t, majd belekattintunk.
        driver.FindElement(By.Name("password")).SendKeys("Admin");// A jelsz� mez�be az admin jelszav�t be�rjuk. 
        driver.FindElement(By.Id("login-submit")).Click();// Megkeress�k a Bel�p�s gombot, �s r�kattintunk
        driver.FindElement(By.LinkText("Product")).Click();// Megkeress�k �s r�kattintunk a Product gombra, hogy a megfelel� helyre vezessen
        driver.FindElement(By.Name("item")).Click();// Lokaliz�ljuk az item elemet, �s belekattintunk
        driver.FindElement(By.Name("item")).SendKeys("teszt_item"); // Bevissz�k a tetsz�leges �rt�ket az item nev�nek.
        driver.FindElement(By.Name("price")).Click();// Lokaliz�ljuk a price elemet, �s belekattintunk
        driver.FindElement(By.Name("price")).SendKeys("3210");// Bevissz�k a tetsz�leges �rt�ket az item �r�nak.
        driver.FindElement(By.Name("Pictures")).Click();// Lokaliz�ljuk a Pictures elemet, �s r�kattintunk
        driver.FindElement(By.Name("Pictures")).SendKeys("X:\\caffetogo\\caffetogo\\tesztkep.jpeg"); // Felt�lt�nk egy k�pet
        driver.FindElement(By.CssSelector("tr:nth-child(2) > .sendlink:nth-child(4) > input")).Click(); // L�trehozzuk a term�k�nket a Create gombbal
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
        driver.FindElement(By.CssSelector("tr:nth-child(42) > td:nth-child(3) > input")).Click();// Megkeress�k az �j term�k �r�t, �s r�kattintunk
        driver.FindElement(By.CssSelector("tr:nth-child(42) > td:nth-child(3) > input")).SendKeys("3230");// Be�runk egy tetsz�leges �rat
        driver.FindElement(By.CssSelector("tr:nth-child(42) > .sendlink:nth-child(6) > input")).Click(); // Az Update gomb megkeres�s�vel, �s megnyom�s�val friss�tett�k az �rat.
        driver.FindElement(By.CssSelector("tr:nth-child(42) > .sendlink:nth-child(5) > input")).Click(); // Megkeress�k a Delete gombot, megnyomjuk hogy t�rl�dj�n az �j term�k
        driver.FindElement(By.LinkText("Index")).Click(); // Visszat�r�nk az Indexre a lap tetej�n tal�lhat� Index gombbal.
    }
    [Test]
    public void adminUserAddUpdateDel()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavig�lunk a c�l oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagys�g� ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click();// Megkeress�k Id alapj�n a Bel�p�s gombot, �s r�kattintunk
        driver.FindElement(By.Name("email")).Click(); // Megkeress�k az email mez�t, majd belekattintunk.
        driver.FindElement(By.Name("email")).SendKeys("Admin@admin.hu");// Az email mez�be az admin email-�t be�rjuk. 
        driver.FindElement(By.Name("password")).Click();// Megkeress�k az jelsz� mez�t, majd belekattintunk.
        driver.FindElement(By.Name("password")).SendKeys("Admin");// A jelsz� mez�be az admin jelszav�t be�rjuk. 
        driver.FindElement(By.Id("login-submit")).Click();// Megkeress�k a Bel�p�s gombot, �s r�kattintunk
        driver.FindElement(By.Name("Email")).Click(); // A felhaszn�l�kn�l megkeress�k az Email elemet
        driver.FindElement(By.Name("Email")).SendKeys("user1@user.hu"); // Bele�runk egy �j Emailt
        driver.FindElement(By.Name("Password")).Click(); // A felhaszn�l�kn�l megkeress�k a Jelsz� elemet �s r�kattintunk
        driver.FindElement(By.Name("Password")).SendKeys("user"); // Megadunk neki egy Jelszavat
        driver.FindElement(By.CssSelector("div:nth-child(3) .sendlink > input")).Click(); // R�kattintunk a Create gombra
        driver.FindElement(By.Name("Email")).Click();// A felhaszn�l�kn�l megkeress�k az Email elemet �s r�kattintunk
        driver.FindElement(By.Name("Email")).SendKeys("user2@user.hu");// Bele�runk egy �j Emailt
        driver.FindElement(By.Name("Password")).Click();// A felhaszn�l�kn�l megkeress�k a Jelsz� elemet �s r�kattintunk
        driver.FindElement(By.Name("Password")).SendKeys("user"); // Megadunk neki egy Jelszavat
        driver.FindElement(By.CssSelector("div:nth-child(3) .sendlink > input")).Click();// R�kattintunk a Create gombra
        driver.FindElement(By.CssSelector("tr:nth-child(21) > td:nth-child(4) > input")).Click(); // Lokaliz�ljuk az Activity elemet
        driver.FindElement(By.CssSelector("tr:nth-child(21) > td:nth-child(4) > input")).SendKeys("2022-03-03T19:05"); // �t�rjuk az Activity-t
        driver.FindElement(By.CssSelector("body")).Click(); // Updatelj�k hogy beker�lj�n a friss�tett Activity d�tum
        driver.FindElement(By.CssSelector(".main:nth-child(4) tr:nth-child(21) > .sendlink:nth-child(6) > input")).Click();//Megkeress�l a Delete gombot a kre�lt felhaszn�l� mellett, �s megnyomjuk
        driver.FindElement(By.CssSelector(".main:nth-child(4) tr:nth-child(21) > .sendlink:nth-child(5) > input")).Click();//Megkeress�l a Delete gombot a m�sik kre�lt felhaszn�l� mellett, �s megnyomjuk
        driver.FindElement(By.LinkText("Index")).Click(); // Visszat�r�nk az Indexre a lap tetej�n tal�lhat� Index gombbal.
    }
}
