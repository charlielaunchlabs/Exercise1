using System;
using System.Collections.Generic;

namespace Exercise1
{
	
		public class User
		{
			public string first_name { get; set; }
			public string last_name { get; set; }
			public string email { get; set; }

		public static explicit operator User(List<User> v)
		{
			throw new NotImplementedException();
		}
	}
	
}

