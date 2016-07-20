using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Exercise1
{
	public class TableAccess
	{
		SQLite.Net.SQLiteConnection db;
		public TableAccess()
		{
			db = DependencyService.Get<SQLConnect>().GetConnection();
			db.CreateTable<UserData>();
		}

		public int AddUser(UserData i)
		{
			return db.Insert(i);
		}
		public int UpdateUser(UserData i)
		{
			return db.Update(i);
		}
		public List<UserData> AllUser()
		{
			return db.Query<UserData>("Select * From [UserData]");
		}
		public int DeleteUser(UserData i)
		{
			return db.Delete(i);
		}
	}
}

