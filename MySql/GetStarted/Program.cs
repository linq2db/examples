using System;
using System.Linq;

namespace GetStarted
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var db = new DataModel.testdataDB())
			{
				var q =
					from c in db.people
					select c;

				foreach (var c in q)
					Console.WriteLine(c.PersonID);
			}
		}
	}
}
