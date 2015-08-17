using System;
using System.Diagnostics;

using DataModels;

using LinqToDB;
using LinqToDB.Data;

namespace SqlServer2005
{
	class Program
	{
		static void Main()
		{
			DataConnection.TurnTraceSwitchOn();
			DataConnection.WriteTraceLine = (s, s1) => Debug.WriteLine(s, s1);

			using (var db = new ExampleDB())
			{
				db.TestTables.Insert(() => new TestTable
				{
					CreatedOn = DateTime.Now
				});
			}
		}
	}
}
