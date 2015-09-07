using System;
using System.Diagnostics;

using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;

namespace Managed
{
	using DataModels;

	class Program
	{
		[Table]
		public class TestTable
		{
			[PrimaryKey, Identity]           public int       ID;
			[Column(Length = 50), NotNull]   public string    Name;
			[Column(Length = 250), Nullable] public string    Description;
			[Column]                         public DateTime? CreatedOn;
		}

		static void Main()
		{
			DataConnection.TurnTraceSwitchOn();
			DataConnection.WriteTraceLine = (s,s1) => Debug.WriteLine(s,s1);

			using (var db = new TestDataDB())
			{
				try { db.DropTable<TestTable>(); } catch (Exception) {}

				var table = db.CreateTable<TestTable>();

				var identity = db.GetTable<TestTable>()
					.InsertWithIdentity(() => new TestTable
					{
						Name      = "Crazy Frog",
						CreatedOn = Sql.CurrentTimestamp
					});

				Console.WriteLine(identity);

				table.Drop();
			}
		}
	}
}
