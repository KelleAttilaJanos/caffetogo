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
    public void registerEmailUres()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavigálunk a cél oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagyságú ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click();// Megkeressük Id alapján a Belépés gombot, és rákattintunk
        driver.FindElement(By.Id("register-form-link")).Click(); // Megkeressük Id alapján a Regisztráció-t és rákattintunk
        driver.FindElement(By.Name("Email")).Click(); // Az email mezõbe kattintunk.
        driver.FindElement(By.Name("Password")).Click(); // A jelszó mezõbe kattintunk.
        driver.FindElement(By.Name("Password")).SendKeys("jelszo"); // Kitöltjük a jelszó mezõt
        driver.FindElement(By.Name("confirmpassword")).Click(); // A jelszó hitelesítõ mezõbe kattintunk
        driver.FindElement(By.Name("confirmpassword")).SendKeys("jelszo"); // Ugyanazt a jelszót adjuk meg mint a jelszó mezõben
        driver.FindElement(By.Id("register-submit")).Click();// Megkeressük Id alapján a Regisztráció gombot, és rákattintunk
    }
    [Test]
    public void registerJelszoMas()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavigálunk a cél oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagyságú ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click();// Megkeressük Id alapján a Belépés gombot, és rákattintunk
        driver.FindElement(By.Id("register-form-link")).Click(); // Megkeressük Id alapján a Regisztráció-t és rákattintunk
        driver.FindElement(By.Name("Email")).Click(); // Az email mezõbe kattintunk.
        driver.FindElement(By.Name("Email")).SendKeys("user1@user.hu");// Kitöltjük az email mezõt
        driver.FindElement(By.Name("Password")).Click(); // A jelszó mezõbe kattintunk.
        driver.FindElement(By.Name("Password")).SendKeys("jelszo");// Kitöltjük a jelszó mezõt
        driver.FindElement(By.Name("confirmpassword")).Click(); // A jelszó hitelesítõ mezõbe kattintunk
        driver.FindElement(By.Name("confirmpassword")).SendKeys("jelszomas"); // Az elõzõ jelszótól különbözõt adunk meg
        driver.FindElement(By.Id("register-submit")).Click();// Megkeressük Id alapján a Regisztráció gombot, és rákattintunk
        driver.FindElement(By.Id("thebutton")).Click(); // A megjelenõ gombra kattintunk 
    }
    [Test]
    public void registerJelszoUres()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavigálunk a cél oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagyságú ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click();// Megkeressük Id alapján a Belépés gombot, és rákattintunk
        driver.FindElement(By.Id("register-form-link")).Click(); // Megkeressük Id alapján a Regisztráció-t és rákattintunk
        driver.FindElement(By.Name("Email")).Click(); // Az email mezõbe kattintunk.
        driver.FindElement(By.Name("Email")).SendKeys("user1@user.hu"); // Kitöltjük az email mezõt
        driver.FindElement(By.Name("Password")).Click(); // A jelszó mezõbe kattintunk.
        driver.FindElement(By.Name("confirmpassword")).Click(); // A jelszó hitelesítõ mezõbe kattintunk
        driver.FindElement(By.Id("register-submit")).Click();// Megkeressük Id alapján a Regisztráció gombot, és rákattintunk
    }
    [Test]
    public void registerSikeres()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavigálunk a cél oldalra // Elnavigálunk a cél oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagyságú ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click();// Megkeressük Id alapján a Belépés gombot, és rákattintunk
        driver.FindElement(By.Id("register-form-link")).Click(); // Megkeressük Id alapján a Regisztráció-t és rákattintunk
        driver.FindElement(By.Name("Email")).Click(); // Az email mezõbe kattintunk.
        driver.FindElement(By.Name("Email")).SendKeys("ujuser@user.hu"); // Még nem létezõ email-t beírunk
        driver.FindElement(By.Name("Password")).Click(); // A jelszó mezõbe kattintunk.
        driver.FindElement(By.Name("Password")).SendKeys("user"); // Egy jelszót adunk meg a mezõbe
        driver.FindElement(By.Name("confirmpassword")).Click(); // A jelszó hitelesítõ mezõbe kattintunk
        driver.FindElement(By.Name("confirmpassword")).SendKeys("user"); // Ugyanazt a jelszót adjuk meg mint a jelszó mezõben
        driver.FindElement(By.Id("register-submit")).Click();// Megkeressük Id alapján a Regisztráció gombot, és rákattintunk
        driver.FindElement(By.Id("thebutton")).Click(); // A megjelenõ gombra kattintunk
    }
    [Test]
    public void registerUserLetezik()
    {
        driver.Navigate().GoToUrl("https://localhost:44354/"); // Elnavigálunk a cél oldalra  // Elnavigálunk a cél oldalra
        driver.Manage().Window.Size = new System.Drawing.Size(1050, 964); // Megadott nagyságú ablakban nyitjuk meg
        driver.FindElement(By.Id("but")).Click();// Megkeressük Id alapján a Belépés gombot, és rákattintunk
        driver.FindElement(By.Id("register-form-link")).Click(); // Megkeressük Id alapján a Regisztráció-t és rákattintunk
        driver.FindElement(By.Name("Email")).Click(); // Az email mezõbe kattintunk.
        driver.FindElement(By.Name("Email")).SendKeys("asd@asd.hu"); // Megadunk egy már létezõ felhasználó email-ét
        driver.FindElement(By.Name("Password")).Click(); // A jelszó mezõbe kattintunk.
        driver.FindElement(By.Name("Password")).SendKeys("asd"); // Megadunk egy jelszót a mezõbe
        driver.FindElement(By.Name("confirmpassword")).Click(); // A jelszó hitelesítõ mezõbe kattintunk
        driver.FindElement(By.Name("confirmpassword")).SendKeys("asd"); // Ugyanazt a jelszót adjuk meg mint a jelszó mezõben
        driver.FindElement(By.Id("register-submit")).Click();// Megkeressük Id alapján a Regisztráció gombot, és rákattintunk
        driver.FindElement(By.Id("thebutton")).Click(); // A megjelenõ gombra kattintunk
    }
}
