using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Exercise1
{

	public class ListContent
	{
		public string FullName { get; set; }
		public string Email { get; set; }
	}

	public class MainPage : ContentPage
	{

		public static string First_Name { get; set; }
		public static string Last_Name { get; set; }
		public static string Email { get; set; }
		public static ObservableCollection<User> item = new ObservableCollection<User>();
		public static ListView list { get; set; }


		public MainPage()
		{

			this.Title = " Users ";
			var goTo = new ToolbarItem
			{
				Text = "  +  ",

				Command = new Command(async () => await Navigation.PushModalAsync(new NavigationPage(new AddUser())))
			};
			this.ToolbarItems.Add(goTo);

			 list = new ListView()
			{
				ItemTemplate = new DataTemplate(() =>
				{
					var textCell = new TextCell();
					textCell.SetBinding(TextCell.TextProperty, "first_name");
					textCell.SetBinding(TextCell.DetailProperty, "email");
					return textCell;
				}),

			};
			list.BindingContext = item;
			list.ItemTapped += async (sender, e) => {
				User item = (User)e.Item;
				First_Name = item.first_name;
				Last_Name = item.last_name;
				Email = item.email;
				await Navigation.PushModalAsync(new NavigationPage(new ProfilePage()));
			};

			var role = JsonConvert.DeserializeObject<ObservableCollection<User>>(CacheData.user_cache);

				try
					{
						list.ItemsSource = role;
					}
					catch(Exception)
					{
					}


			StackLayout main = new StackLayout
			{

				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = 
				{
					list 
				}
			};
			Content = main;
		}
	}



}


