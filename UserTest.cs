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
public class UserTesztekTest
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
    public void userKosarFizetesUres()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavigálunk a cél oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagyságú ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click(); // Megkeressük Id alapján a Belépés gombot, és rákattintunk
        driver.FindElement(By.Name("email")).Click();// Az email mezõbe kattintunk.
        driver.FindElement(By.Name("email")).SendKeys("asd@asd.hu"); // Beírjuk egy létezõ felhasználó email-ét
        driver.FindElement(By.Name("password")).Click(); // Majd a jelszó mezõre kattintunk
        driver.FindElement(By.Name("password")).SendKeys("asd"); // Beírjuk a létezõ felhasználó jelszavát
        driver.FindElement(By.Id("login-submit")).Click(); // A Belépés gombra kattintunk
        driver.FindElement(By.Id("thebutton")).Click();// A felugró üzenetet bezárjuk a gombbal
        driver.FindElement(By.Id("card-button")).Click(); // Tetszõleges termék Kivonását inicializáljuk
        {
            var element = driver.FindElement(By.Id("card-button"));
            Actions builder = new Actions(driver);
            builder.MoveToElement(element).Perform();
        }
        {
            var element = driver.FindElement(By.tagName("body"));
            Actions builder = new Actions(driver);
            builder.MoveToElement(element, 0, 0).Perform();
        }
        driver.FindElement(By.CssSelector(".col:nth-child(1) td:nth-child(2) > #card-button")).Click();// A termék kivonás gombjára való keresés és kattintás
        driver.FindElement(By.CssSelector("form > #thebutton")).Click();// A Fizetés gombra kattintunk
        driver.FindElement(By.CssSelector(".btn:nth-child(3)")).Click(); // Majd véglegesítjük az üres fizetésünket a Fizetés gombbal
    }
    [Test]
    public void userKosarFizetes()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavigálunk a cél oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagyságú ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click(); // Megkeressük Id alapján a Belépés gombot, és rákattintunk
        driver.FindElement(By.Name("email")).Click();// Az email mezõbe kattintunk.
        driver.FindElement(By.Name("email")).SendKeys("asd@asd.hu"); // Beírjuk egy létezõ felhasználó email-ét
        driver.FindElement(By.Name("password")).Click(); // Majd a jelszó mezõre kattintunk
        driver.FindElement(By.Name("password")).SendKeys("asd"); // Beírjuk a létezõ felhasználó jelszavát
        driver.FindElement(By.Id("login-submit")).Click(); // A Belépés gombra kattintunk
        driver.FindElement(By.Id("thebutton")).Click(); // A felugró üzenetet bezárjuk a gombbal
        driver.FindElement(By.Id("card-button")).Click(); // Tetszõleges termékeket hozzáadunk a kosarunkhoz
        {
            var element = driver.FindElement(By.Id("card-button"));
            Actions builder = new Actions(driver);
            builder.MoveToElement(element).Perform();
        }
        {
            var element = driver.FindElement(By.tagName("body"));
            Actions builder = new Actions(driver);
            builder.MoveToElement(element, 0, 0).Perform();
        }
        driver.FindElement(By.CssSelector(".col:nth-child(3) td:nth-child(1) > #card-button")).Click(); //Elsõ termék amit megkeres és rákattint
        driver.FindElement(By.CssSelector(".col:nth-child(11) td:nth-child(1) > #card-button")).Click(); // Második termék amit megkeres és rákattint
        driver.FindElement(By.CssSelector(".col:nth-child(13) td:nth-child(1) > #card-button")).Click(); // Harmadik termék amit megkeres és rákattint
        driver.FindElement(By.CssSelector("form > #thebutton")).Click();// A Fizetés gombra kattintunk
        driver.FindElement(By.CssSelector(".btn:nth-child(3)")).Click();// Majd véglegesítjük a sikeres fizetésünket a Fizetés gombbal
        driver.FindElement(By.Id("thebutton")).Click(); // Végül bezárjuk a felugró ablakot ami tájékoztat minket
    }
}
