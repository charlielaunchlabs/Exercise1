using System;
using SQLite.Net;

namespace Exercise1
{
	public interface SQLConnect
	{

		SQLiteConnection GetConnection();

	}
}

