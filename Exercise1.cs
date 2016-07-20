using System;

using Xamarin.Forms;

namespace Exercise1
{



	public class App : Application
	{

		static TableAccess dbUtil;

		public App()
		{
			MainPage = new MasterPage();
		}



		public static TableAccess DAUtil
		{
			get
			{
				if (dbUtil == null)
				{
					dbUtil = new TableAccess();
				}
				return dbUtil;
			}
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

