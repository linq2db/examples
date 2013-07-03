using System;
using System.Linq;

namespace GetStarted
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var db = new DataModel.TestDataDB())
			{
				var q =
					from c in db.People
					select c;

				foreach (var c in q)
					Console.WriteLine(c.PersonID);
			}
		}
	}
}
