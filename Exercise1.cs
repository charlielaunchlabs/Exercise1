using System;
using System.Diagnostics;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Mvvm;

namespace Exercise1
{



	public class App : Application
	{

		static TableAccess dbUtil;

		public App()
		{

			Init();
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


		public static void Init()
		{

			var app = Resolver.Resolve<IXFormsApp>();
			if (app == null)
			{
				return;
			}

			app.Closing += (o, e) => Debug.WriteLine("Application Closing");
			app.Error += (o, e) => Debug.WriteLine("Application Error");
			app.Initialize += (o, e) => Debug.WriteLine("Application Initialized");
			app.Resumed += (o, e) => Debug.WriteLine("Application Resumed");
			app.Rotation += (o, e) => Debug.WriteLine("Application Rotated");
			app.Startup += (o, e) => Debug.WriteLine("Application Startup");
			app.Suspended += (o, e) => Debug.WriteLine("Application Suspended");
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

