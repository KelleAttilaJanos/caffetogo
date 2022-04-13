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
public class RegisterTesztekTest
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
    public void registerEmailUres()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavig�lunk a c�l oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagys�g� ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click();// Megkeress�k Id alapj�n a Bel�p�s gombot, �s r�kattintunk
        driver.FindElement(By.Id("register-form-link")).Click(); // Megkeress�k Id alapj�n a Regisztr�ci�-t �s r�kattintunk
        driver.FindElement(By.Name("Email")).Click(); // Az email mez�be kattintunk.
        driver.FindElement(By.Name("Password")).Click(); // A jelsz� mez�be kattintunk.
        driver.FindElement(By.Name("Password")).SendKeys("jelszo"); // Kit�ltj�k a jelsz� mez�t
        driver.FindElement(By.Name("confirmpassword")).Click(); // A jelsz� hiteles�t� mez�be kattintunk
        driver.FindElement(By.Name("confirmpassword")).SendKeys("jelszo"); // Ugyanazt a jelsz�t adjuk meg mint a jelsz� mez�ben
        driver.FindElement(By.Id("register-submit")).Click();// Megkeress�k Id alapj�n a Regisztr�ci� gombot, �s r�kattintunk
    }
    [Test]
    public void registerJelszoMas()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavig�lunk a c�l oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagys�g� ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click();// Megkeress�k Id alapj�n a Bel�p�s gombot, �s r�kattintunk
        driver.FindElement(By.Id("register-form-link")).Click(); // Megkeress�k Id alapj�n a Regisztr�ci�-t �s r�kattintunk
        driver.FindElement(By.Name("Email")).Click(); // Az email mez�be kattintunk.
        driver.FindElement(By.Name("Email")).SendKeys("user1@user.hu");// Kit�ltj�k az email mez�t
        driver.FindElement(By.Name("Password")).Click(); // A jelsz� mez�be kattintunk.
        driver.FindElement(By.Name("Password")).SendKeys("jelszo");// Kit�ltj�k a jelsz� mez�t
        driver.FindElement(By.Name("confirmpassword")).Click(); // A jelsz� hiteles�t� mez�be kattintunk
        driver.FindElement(By.Name("confirmpassword")).SendKeys("jelszomas"); // Az el�z� jelsz�t�l k�l�nb�z�t adunk meg
        driver.FindElement(By.Id("register-submit")).Click();// Megkeress�k Id alapj�n a Regisztr�ci� gombot, �s r�kattintunk
        driver.FindElement(By.Id("thebutton")).Click(); // A megjelen� gombra kattintunk 
    }
    [Test]
    public void registerJelszoUres()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavig�lunk a c�l oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagys�g� ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click();// Megkeress�k Id alapj�n a Bel�p�s gombot, �s r�kattintunk
        driver.FindElement(By.Id("register-form-link")).Click(); // Megkeress�k Id alapj�n a Regisztr�ci�-t �s r�kattintunk
        driver.FindElement(By.Name("Email")).Click(); // Az email mez�be kattintunk.
        driver.FindElement(By.Name("Email")).SendKeys("user1@user.hu"); // Kit�ltj�k az email mez�t
        driver.FindElement(By.Name("Password")).Click(); // A jelsz� mez�be kattintunk.
        driver.FindElement(By.Name("confirmpassword")).Click(); // A jelsz� hiteles�t� mez�be kattintunk
        driver.FindElement(By.Id("register-submit")).Click();// Megkeress�k Id alapj�n a Regisztr�ci� gombot, �s r�kattintunk
    }
    [Test]
    public void registerSikeres()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavig�lunk a c�l oldalra // Elnavig�lunk a c�l oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagys�g� ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click();// Megkeress�k Id alapj�n a Bel�p�s gombot, �s r�kattintunk
        driver.FindElement(By.Id("register-form-link")).Click(); // Megkeress�k Id alapj�n a Regisztr�ci�-t �s r�kattintunk
        driver.FindElement(By.Name("Email")).Click(); // Az email mez�be kattintunk.
        driver.FindElement(By.Name("Email")).SendKeys("ujuser@user.hu"); // M�g nem l�tez� email-t be�runk
        driver.FindElement(By.Name("Password")).Click(); // A jelsz� mez�be kattintunk.
        driver.FindElement(By.Name("Password")).SendKeys("user"); // Egy jelsz�t adunk meg a mez�be
        driver.FindElement(By.Name("confirmpassword")).Click(); // A jelsz� hiteles�t� mez�be kattintunk
        driver.FindElement(By.Name("confirmpassword")).SendKeys("user"); // Ugyanazt a jelsz�t adjuk meg mint a jelsz� mez�ben
        driver.FindElement(By.Id("register-submit")).Click();// Megkeress�k Id alapj�n a Regisztr�ci� gombot, �s r�kattintunk
        driver.FindElement(By.Id("thebutton")).Click(); // A megjelen� gombra kattintunk
    }
    [Test]
    public void registerUserLetezik()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavig�lunk a c�l oldalra  // Elnavig�lunk a c�l oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagys�g� ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click();// Megkeress�k Id alapj�n a Bel�p�s gombot, �s r�kattintunk
        driver.FindElement(By.Id("register-form-link")).Click(); // Megkeress�k Id alapj�n a Regisztr�ci�-t �s r�kattintunk
        driver.FindElement(By.Name("Email")).Click(); // Az email mez�be kattintunk.
        driver.FindElement(By.Name("Email")).SendKeys("asd@asd.hu"); // Megadunk egy m�r l�tez� felhaszn�l� email-�t
        driver.FindElement(By.Name("Password")).Click(); // A jelsz� mez�be kattintunk.
        driver.FindElement(By.Name("Password")).SendKeys("asd"); // Megadunk egy jelsz�t a mez�be
        driver.FindElement(By.Name("confirmpassword")).Click(); // A jelsz� hiteles�t� mez�be kattintunk
        driver.FindElement(By.Name("confirmpassword")).SendKeys("asd"); // Ugyanazt a jelsz�t adjuk meg mint a jelsz� mez�ben
        driver.FindElement(By.Id("register-submit")).Click();// Megkeress�k Id alapj�n a Regisztr�ci� gombot, �s r�kattintunk
        driver.FindElement(By.Id("thebutton")).Click(); // A megjelen� gombra kattintunk
    }
}
