using System;
using System.Diagnostics;
using System.Linq;

using static System.Console;

using LinqToDB.Data;

namespace DataConnectionDemo
{
	using DataModels;
		
	class Program
	{
		static void Main()
		{
#if DEBUG
			DataConnection.TurnTraceSwitchOn();
			DataConnection.WriteTraceLine = (s, s1) => Debug.WriteLine(s, s1);
#endif

			NorthwindTest();
		}

		static void NorthwindTest()
		{
			using (var db = new NorthwindDB())
			{
				var q =
					from order in db.Orders
					select new
					{
						order.Customer.CompanyName,
						Count = order.OrderDetails.Count(),
					};

				foreach (var item in q)
				{
					WriteLine($"Company Name: {item.CompanyName}, Count: {item.Count}");
				}
			}
		}
	}
}
