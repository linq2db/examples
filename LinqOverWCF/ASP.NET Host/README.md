Web Application
---------------

1. Install the [linq2db](https://nuget.org/packages/linq2db/) nuget.
2. Add WCF Service to your ASP.NET application. Call the service 'LinqWcfService' for instance.
3. Go to the LinqWcfService.svc.cs file and replace base interface with 'LinqToDB.ServiceModel.LinqService'.
4. Delete the ILinqWcfService interface from your application.
5. Add 'connectionStrings' section to your Web.config.

Client
------

1. Install an apporopriate linq2db nuget. The [linq2db.SqlServer](https://nuget.org/packages/linq2db.SqlServer/) is used in this example.
2. Create a DataModel.tt file in the likeness of [Northwind.tt](https://github.com/linq2db/examples/blob/master/LinqOverWCF/ASP.NET Host/Client/Northwind.tt).
