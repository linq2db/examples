using System;
using System.Linq;

using DataModel;

namespace GetStarted
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var db = new TestDataDB())
			{
				var q =
					from c in db.People
					select c;

				foreach (var c in q)
					Console.WriteLine(c.PersonID);

				db.Patient_SelectByName("", "");
			}
		}
	}
}
