using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
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
