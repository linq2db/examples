using System;
using System.Diagnostics;
using System.Linq;
using DataModel;
using LinqToDB.Data;

namespace LinqToDBDemo
{
	// Watch video on http://youtu.be/Qc-5UpMYQO0
	//
	class Program
	{
		static void Main(string[] args)
		{
#if DEBUG
			DataConnection.TurnTraceSwitchOn();
			DataConnection.WriteTraceLine = (s, s1) => Debug.WriteLine(s, s1);
#endif

			using (var db = new NorthwindDB())
			{
				var q =
					from c in db.Customers
					select new
					{
						c.CompanyName,
						OrderCount = c.Orders.Count()
					};

				foreach (var c in q)
					Console.WriteLine(c);
			}
		}
	}
}
