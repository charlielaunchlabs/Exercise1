using System;
namespace Exercise1
{
	public class CacheData
	{
		public CacheData()
		{
		}

		public static string user_cache
		{
			get
			{
				try
				{
					return (string)App.Current.Properties["user_cache"];
				}
				catch (Exception)
				{
					App.Current.Properties["user_cache"] = "";
					return (string)App.Current.Properties["user_cache"];
				}
			}
			set
			{
				App.Current.Properties["user_cache"] = value;
			}
		}

	}
}

