using System;
using SQLite.Net.Attributes;

namespace Exercise1
{
	public class UserData
	{
		[PrimaryKey, AutoIncrement]
		public long ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }

	}
}

