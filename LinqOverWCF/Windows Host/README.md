Host
---------------

1. Install the [linq2db](https://nuget.org/packages/linq2db/) nuget.
2. Open host. See [Northwind.tt](https://github.com/linq2db/examples/blob/master/LinqOverWCF/Windows Host/Host/Program.cs).
3. Add 'connectionStrings' section to your Web.config.

Client
------

1. Install an apporopriate linq2db nuget.
2. Open [LinqToDB.Templates\CopyMe.XXXX.tt.txt](https://github.com/linq2db/examples/blob/master/LinqOverWCF/Windows Host/Client/LinqToDB.Templates/CopyMe.SqlServer.tt.txt) and follow the instructions inside the file.
3. Modify your DataModel.tt file in the likeness of [Northwind.tt](https://github.com/linq2db/examples/blob/master/LinqOverWCF/Windows Host/Client/Northwind.tt).
