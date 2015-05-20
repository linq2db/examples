using System;
using System.Diagnostics;
using System.Linq;

using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;

using NUnit.Framework;

namespace LinqToDBCrudDemo
{
	// Watch video on http://youtu.be/m--oX73EGeQ
	//
	[TestFixture]
	public class Tests
	{
		static Tests()
		{
			DataConnection.TurnTraceSwitchOn();
			DataConnection.WriteTraceLine = (s, s1) => Debug.WriteLine(s, s1);
		}

		[Table]
		public class TestTable
		{
			[PrimaryKey, Identity]           public int       ID;
			[Column(Length = 50), NotNull]   public string    Name;
			[Column(Length = 250), Nullable] public string    Description;
			[Column]                         public DateTime? CreatedOn;
		}

		[Table]
		public class TestTable2
		{
			[PrimaryKey, Identity]           public int ID;
			[Column(Length = 50), NotNull]   public string Name;
			[Column(Length = 250), Nullable] public string Description;
			[Column]                         public DateTime? CreatedOn;
		}

		[Table]
		public class TestTable3
		{
			[PrimaryKey]                   public int ID;
			[Column(Length = 50), NotNull] public string Name;
		}

		// CreateTable and DropTable
		//
		[Test]
		public void CreateTest([Values(
			ProviderName.SqlServer, ProviderName.Access, ProviderName.DB2, ProviderName.Firebird, 
			ProviderName.Informix, ProviderName.MySql, ProviderName.Oracle, ProviderName.PostgreSQL,
			ProviderName.SqlCe, ProviderName.SQLite, ProviderName.Sybase)] string configString)
		{
			using (var db = new DataConnection(configString))
			{
				try { db.DropTable<TestTable>();  } catch {}
				try { db.DropTable<TestTable2>(); } catch {}
				try { db.DropTable<TestTable3>(); } catch {}

				db.CreateTable<TestTable>();
				db.CreateTable<TestTable2>();
				db.CreateTable<TestTable3>();
			}
		}

		// Insert
		// this method inserts a single object
		//
		[Test]
		public void InsertTest([Values(
			ProviderName.SqlServer, ProviderName.Access, ProviderName.DB2, ProviderName.Firebird, 
			ProviderName.Informix, ProviderName.MySql, ProviderName.Oracle, ProviderName.PostgreSQL,
			ProviderName.SqlCe, ProviderName.SQLite, ProviderName.Sybase)] string configString)
		{
			using (var db = new DataConnection(configString))
			{
				db.Insert(new TestTable
				{
					Name = "Crazy Frog",
				});
			}
		}

		// Insert
		// this method takes Expression Tree and converts it to SQL
		//
		[Test]
		public void InsertTest2([Values(
			ProviderName.SqlServer, ProviderName.Access, ProviderName.DB2, ProviderName.Firebird, 
			ProviderName.Informix, ProviderName.MySql, ProviderName.Oracle, ProviderName.PostgreSQL,
			ProviderName.SqlCe, ProviderName.SQLite, ProviderName.Sybase)] string configString)
		{
			using (var db = new DataConnection(configString))
			{
				db.GetTable<TestTable>()
					.Insert(() => new TestTable
					{
						Name      = "Crazy Frog",
						CreatedOn = Sql.CurrentTimestamp
					});
			}
		}

		// Insert
		// this method generates INSERT INTO / SELECT statement
		//
		[Test]
		public void InsertTest3([Values(
			ProviderName.SqlServer, ProviderName.Access, ProviderName.DB2, ProviderName.Firebird, 
			ProviderName.Informix, ProviderName.MySql, ProviderName.Oracle, ProviderName.PostgreSQL,
			ProviderName.SqlCe, ProviderName.SQLite, ProviderName.Sybase)] string configString)
		{
			using (var db = new DataConnection(configString))
			{
				db
					.GetTable<TestTable>()
					.Where (t => t.Name == "Crazy Frog")
					.Insert(
						db.GetTable<TestTable2>(),
						t => new TestTable2
						{
							Name = t.Name + " II",
							CreatedOn = t.CreatedOn.Value.AddDays(1)
						});
			}
		}

		// Into, Value, Insert
		// alternative for those who don't like new operator for Insert
		//
		[Test]
		public void InsertTest4([Values(
			ProviderName.SqlServer, ProviderName.Access, ProviderName.DB2, ProviderName.Firebird, 
			ProviderName.Informix, ProviderName.MySql, ProviderName.Oracle, ProviderName.PostgreSQL,
			ProviderName.SqlCe, ProviderName.SQLite, ProviderName.Sybase)] string configString)
		{
			using (var db = new DataConnection(configString))
			{
				db
					.GetTable<TestTable>()
					.Where(t => t.Name == "Crazy Frog")
					.Into(db.GetTable<TestTable2>())
						.Value(t => t.Name,      t => t.Name + " II")
						.Value(t => t.CreatedOn, t => t.CreatedOn.Value.AddDays(1))
					.Insert();
			}
		}

		// InsertWithIdentity
		// this method inserts a row and returns identity value
		//
		[Test]
		public void InsertWithIdentityTest([Values(
			ProviderName.SqlServer, ProviderName.Access, ProviderName.DB2, ProviderName.Firebird, 
			ProviderName.Informix, ProviderName.MySql, ProviderName.Oracle, ProviderName.PostgreSQL,
			ProviderName.SqlCe, ProviderName.SQLite, ProviderName.Sybase)] string configString)
		{
			using (var db = new DataConnection(configString))
			{
				var identity = db.GetTable<TestTable>()
					.InsertWithIdentity(() => new TestTable
					{
						Name      = "Crazy Frog",
						CreatedOn = Sql.CurrentTimestamp
					});

				Console.WriteLine(identity);
			}
		}

		// InsertOrUpdate
		// this method updates existing record or inserts new one if it does not exist
		//
		[Test]
		public void InsertOrUpdateTest([Values(
			ProviderName.SqlServer, ProviderName.Access, ProviderName.DB2, ProviderName.Firebird, 
			ProviderName.Informix, ProviderName.MySql, ProviderName.Oracle, ProviderName.PostgreSQL,
			ProviderName.SqlCe, ProviderName.SQLite, ProviderName.Sybase)] string configString)
		{
			using (var db = new DataConnection(configString))
			{
				db.GetTable<TestTable3>()
					.InsertOrUpdate(
						() => new TestTable3
						{
							ID   = 5,
							Name = "Crazy Frog",
						},
						t => new TestTable3
						{
							Name = "Crazy Frog IV",
						});
			}
		}

		// InsertOrReplace
		// this method updates all columns of existing record or inserts new one if it does not exist
		//
		[Test]
		public void InsertOrReplaceTest([Values(
			ProviderName.SqlServer, ProviderName.Access, ProviderName.DB2, ProviderName.Firebird, 
			ProviderName.Informix, ProviderName.MySql, ProviderName.Oracle, ProviderName.PostgreSQL,
			ProviderName.SqlCe, ProviderName.SQLite, ProviderName.Sybase)] string configString)
		{
			using (var db = new DataConnection(configString))
			{
				db.InsertOrReplace(
					new TestTable3
					{
						ID   = 5,
						Name = "Crazy Frog",
					});
			}
		}

		// Update
		// this method updates all columns of existing record
		//
		[Test]
		public void UpdateTest([Values(
			ProviderName.SqlServer, ProviderName.Access, ProviderName.DB2, ProviderName.Firebird, 
			ProviderName.Informix, ProviderName.MySql, ProviderName.Oracle, ProviderName.PostgreSQL,
			ProviderName.SqlCe, ProviderName.SQLite, ProviderName.Sybase)] string configString)
		{
			using (var db = new DataConnection(configString))
			{
				db.Update(
					new TestTable3
					{
						ID   = 5,
						Name = "Crazy Frog",
					});
			}
		}

		// Update
		// this method updates only columns you specify
		//
		[Test]
		public void UpdateTest2([Values(
			ProviderName.SqlServer, ProviderName.Access, ProviderName.DB2, ProviderName.Firebird, 
			ProviderName.Informix, ProviderName.MySql, ProviderName.Oracle, ProviderName.PostgreSQL,
			ProviderName.SqlCe, ProviderName.SQLite, ProviderName.Sybase)] string configString)
		{
			using (var db = new DataConnection(configString))
			{
				db
					.GetTable<TestTable>()
					.Where (t => t.ID == 1)
					.Update(t => new TestTable
					{
						Name = "Crazy Frog",
					});
			}
		}

		// Update
		// override with WHERE clause
		//
		[Test]
		public void UpdateTest3([Values(
			ProviderName.SqlServer, ProviderName.Access, ProviderName.DB2, ProviderName.Firebird, 
			ProviderName.Informix, ProviderName.MySql, ProviderName.Oracle, ProviderName.PostgreSQL,
			ProviderName.SqlCe, ProviderName.SQLite, ProviderName.Sybase)] string configString)
		{
			using (var db = new DataConnection(configString))
			{
				db
					.GetTable<TestTable>()
					.Update(
						t => t.ID == 1,
						t => new TestTable
						{
							Name = "Crazy Frog",
						});
			}
		}

		// Update
		// alternative for those who don't like new operator for Update
		//
		[Test]
		public void UpdateTest4([Values(
			ProviderName.SqlServer, ProviderName.Access, ProviderName.DB2, ProviderName.Firebird, 
			ProviderName.Informix, ProviderName.MySql, ProviderName.Oracle, ProviderName.PostgreSQL,
			ProviderName.SqlCe, ProviderName.SQLite, ProviderName.Sybase)] string configString)
		{
			using (var db = new DataConnection(configString))
			{
				db
					.GetTable<TestTable>()
					.Where(t => t.ID == 1)
					.Set(t => t.Name,      t => "Crazy Frog IV")
					.Set(t => t.CreatedOn, t => t.CreatedOn.Value.AddHours(1))
					.Update();
			}
		}

		// Delete
		// this method deletes an existing record
		//
		[Test]
		public void DeleteTest([Values(
			ProviderName.SqlServer, ProviderName.Access, ProviderName.DB2, ProviderName.Firebird, 
			ProviderName.Informix, ProviderName.MySql, ProviderName.Oracle, ProviderName.PostgreSQL,
			ProviderName.SqlCe, ProviderName.SQLite, ProviderName.Sybase)] string configString)
		{
			using (var db = new DataConnection(configString))
			{
				db
					.GetTable<TestTable>()
					.Where(t => t.ID == 1)
					.Delete();
			}
		}

		// if you have an issue with transaction log, you can specify size of batch to be deleted
		// some databases do not support it, some do
		//
		[Test]
		public void DeleteBigTableTest([Values(
			ProviderName.SqlServer,  
			ProviderName.Oracle,
			ProviderName.Sybase)] string configString)
		{
			using (var db = new DataConnection(configString))
			{
				while (db.GetTable<TestTable>().Take(10000).Delete() > 0);
			}
		}

		// BulkCopy
		//
		[Test]
		public void BulkCopyTest([Values(
			ProviderName.SqlServer, ProviderName.Access, ProviderName.DB2, ProviderName.Firebird, 
			ProviderName.Informix, ProviderName.MySql, ProviderName.Oracle, ProviderName.PostgreSQL,
			ProviderName.SqlCe, ProviderName.SQLite, ProviderName.Sybase)] string configString)
		{
			using (var db = new DataConnection(configString))
			{
				db.BulkCopy(
					new BulkCopyOptions { BulkCopyTimeout = 60 * 10 },
					Enumerable
						.Range(1, 100)
						.Select(n => new TestTable
						{
							Name = n.ToString()
						})
					);
			}
		}
	}
}
