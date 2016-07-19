using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Exercise1
{
	public class MenuPage : ContentPage
	{

		public ListView Menu { get; set; }

		public MenuPage()
		{
			Title = "Menu"; // The Title property must be set.
			BackgroundColor = Color.White;

			Menu = new MenuListView();

			ContentView menuLabel = new ContentView
			{
				Padding = new Thickness(10, 36, 0, 5),
				Content = new Label
				{
					TextColor = Color.Black,
					Text = "Menu",
				}
			};

			StackLayout layout = new StackLayout
			{
				Spacing = 0,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			layout.Children.Add(menuLabel);
			layout.Children.Add(Menu);

			Content = layout;
		}
	}


	public class MenuListView : ListView
	{
		public MenuListView()
		{
			List<MenuItem> data = new MenuListData();

			ItemsSource = data;
			VerticalOptions = LayoutOptions.FillAndExpand;
			BackgroundColor = Color.Transparent;
			SeparatorVisibility = SeparatorVisibility.None;

			DataTemplate cell = new DataTemplate(typeof(MenuCell));
			cell.SetBinding(MenuCell.TextProperty, "Title");

			ItemTemplate = cell;
		}
	}


	public class MenuItem
	{
		public string Title { get; set; }

		public Type TargetType { get; set; }
	}


	public class MenuCell : ImageCell
	{
		public MenuCell() : base()
		{
			this.TextColor = Color.FromHex("AAAAAA");
		}
	}


	public class MenuListData : List<MenuItem>
	{

		public MenuListData()
		{
			
			this.Add(new MenuItem()
			{
				Title = "User",
				TargetType = typeof(MainPage)
			});

			this.Add(new MenuItem()
			{
				Title = "Add User",
				TargetType = typeof(AddUser)
			});

		}
	}
}


