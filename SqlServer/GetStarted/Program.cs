using System;
using System.Linq;

namespace GetStarted
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var db = new DataModel.NorthwindDB())
			{
				var q =
					from c in db.Customers
					select c;

				foreach (var c in q)
					Console.WriteLine(c.ContactName);
			}
		}
	}
}
