using System;
using System.Linq;

namespace HowToUseOldVersion
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var db = new TestDataDB())
			{
				var q =
					from p in db.People
					where p.PersonID < 10
					select p;

				foreach (var person in q)
				{
					Console.WriteLine("{0}, {1}", person.LastName, person.FirstName);
				}
			}
		}
	}
}
