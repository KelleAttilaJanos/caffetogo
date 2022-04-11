# caffetogo
A projektet úgy készítettük el, hogy későbbiekben egy kisvállalat feltudja használni mivel rengeteg lehetőséget tartogat magában.
## Szoftverek listája:
Visual Studio Code 1.64:
>+	HTML 39,6%
Visual Studio 2022:
>+	C# 44,8%
>+	ASP.NET MVC
>>+	5.0
+	Packages:
>+	Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation 5.0.12
>+	Microsoft.EntityFrameworkCore 5.0.13
>+	Microsoft.EntityFrameworkCore.SqlServer 5.0.13
>+	Microsoft.EntityFrameworkCore.Tools 5.0.13
>+	Microsoft.VisualStudio.Web.CodeGeneration.Design 5.0.2
>+	NWebsec.AspNetCore.Middleware 3.0.0
>+	System.Drawing.Common 6.0.0

Microsoft SQL Server 2019 

SQL Server Management Studio v18.1

CSS 15,2%

JavaScript 0,4%

## Az webalkalmazás indítása:
### Lokális indítás:
A IIS EXPRESS futtatása előtt, megnyitjuk a SQL Server Management Studiot és elindítjuk a lokális szervert, és egyben kimásoljuk a szerver nevét.
A kimásolt adatot a megnyitott SLN-ben az appsetting.json-ben látható connection string-be illeszük be.
Amennyiben az elérési utat megfelelően adjuk meg, az IIS EXPRESS futtatásával megnyílik a webalkalmazásunk.
