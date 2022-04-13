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
    public void userKosarFizetesUres()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavig�lunk a c�l oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagys�g� ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click(); // Megkeress�k Id alapj�n a Bel�p�s gombot, �s r�kattintunk
        driver.FindElement(By.Name("email")).Click();// Az email mez�be kattintunk.
        driver.FindElement(By.Name("email")).SendKeys("asd@asd.hu"); // Be�rjuk egy l�tez� felhaszn�l� email-�t
        driver.FindElement(By.Name("password")).Click(); // Majd a jelsz� mez�re kattintunk
        driver.FindElement(By.Name("password")).SendKeys("asd"); // Be�rjuk a l�tez� felhaszn�l� jelszav�t
        driver.FindElement(By.Id("login-submit")).Click(); // A Bel�p�s gombra kattintunk
        driver.FindElement(By.Id("thebutton")).Click();// A felugr� �zenetet bez�rjuk a gombbal
        driver.FindElement(By.Id("card-button")).Click(); // Tetsz�leges term�k Kivon�s�t inicializ�ljuk
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
        driver.FindElement(By.CssSelector(".col:nth-child(1) td:nth-child(2) > #card-button")).Click();// A term�k kivon�s gombj�ra val� keres�s �s kattint�s
        driver.FindElement(By.CssSelector("form > #thebutton")).Click();// A Fizet�s gombra kattintunk
        driver.FindElement(By.CssSelector(".btn:nth-child(3)")).Click(); // Majd v�gleges�tj�k az �res fizet�s�nket a Fizet�s gombbal
    }
    [Test]
    public void userKosarFizetes()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavig�lunk a c�l oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagys�g� ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click(); // Megkeress�k Id alapj�n a Bel�p�s gombot, �s r�kattintunk
        driver.FindElement(By.Name("email")).Click();// Az email mez�be kattintunk.
        driver.FindElement(By.Name("email")).SendKeys("asd@asd.hu"); // Be�rjuk egy l�tez� felhaszn�l� email-�t
        driver.FindElement(By.Name("password")).Click(); // Majd a jelsz� mez�re kattintunk
        driver.FindElement(By.Name("password")).SendKeys("asd"); // Be�rjuk a l�tez� felhaszn�l� jelszav�t
        driver.FindElement(By.Id("login-submit")).Click(); // A Bel�p�s gombra kattintunk
        driver.FindElement(By.Id("thebutton")).Click(); // A felugr� �zenetet bez�rjuk a gombbal
        driver.FindElement(By.Id("card-button")).Click(); // Tetsz�leges term�keket hozz�adunk a kosarunkhoz
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
        driver.FindElement(By.CssSelector(".col:nth-child(3) td:nth-child(1) > #card-button")).Click(); //Els� term�k amit megkeres �s r�kattint
        driver.FindElement(By.CssSelector(".col:nth-child(11) td:nth-child(1) > #card-button")).Click(); // M�sodik term�k amit megkeres �s r�kattint
        driver.FindElement(By.CssSelector(".col:nth-child(13) td:nth-child(1) > #card-button")).Click(); // Harmadik term�k amit megkeres �s r�kattint
        driver.FindElement(By.CssSelector("form > #thebutton")).Click();// A Fizet�s gombra kattintunk
        driver.FindElement(By.CssSelector(".btn:nth-child(3)")).Click();// Majd v�gleges�tj�k a sikeres fizet�s�nket a Fizet�s gombbal
        driver.FindElement(By.Id("thebutton")).Click(); // V�g�l bez�rjuk a felugr� ablakot ami t�j�koztat minket
    }
}
