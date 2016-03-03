Web Application
---------------

1. Install the [linq2db](https://nuget.org/packages/linq2db/) nuget.
2. Add WCF Service to your ASP.NET application. Call 'LinqWcfService' for instance.
3. Go to the LinqWcfService.svc.cs file and replace base interface with 'LinqToDB.ServiceModel.LinqService'.
4. Delete the ILinqWcfService interface from your application.
5. Add 'connectionStrings' section to your Web.config.

Client
------


1. Install an apporopriate linq2db nuget.
2. Open [LinqToDB.Templates\CopyMe.XXXX.tt.txt](https://github.com/linq2db/examples/blob/master/SqlServer/GetStarted/LinqToDB.Templates/CopyMe.SqlServer.tt.txt) and follow the instructions inside the file.
3. Modify your DataModel.tt file in the likeness of [Northwind.tt](https://github.com/linq2db/examples/blob/master/LinqOverWCF/ASP.NET Host/Client/Northwind.tt).
