using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Exercise1
{
	public class UserSQL : ContentPage
	{
		public static ContentPage ssa { get; set; }
		public static ListView lista { get; set; }
		public static string First_Name { get; set; }
		public static string Last_Name { get; set; }
		public static string Email { get; set; }

		public UserSQL()
		{
			ssa = this;
			this.Title = " Users SQL Data ";
			var goTo = new ToolbarItem
			{
				Text = "  +  ",

				Command = new Command(async () => await Navigation.PushModalAsync(new NavigationPage(new AddUserSQL())))
			};
			this.ToolbarItems.Add(goTo);
			lista = new ListView();
			lista.ItemTemplate = new DataTemplate(typeof(CustomListChuchu));
			var all = App.DAUtil.AllUser();

			var oc = new ObservableCollection<UserData>(all);
			lista.ItemsSource = oc;
			lista.BindingContext = oc;
			lista.SeparatorVisibility = Xamarin.Forms.SeparatorVisibility.None;
			lista.ItemTapped += async (sender, e) =>
			{
				UserData item = (UserData)e.Item;
				First_Name = item.FirstName;
				Last_Name = item.LastName;
				Email = item.Email;
				await Navigation.PushModalAsync(new NavigationPage(new UserProfileSQL()));
			};
			Content = new StackLayout
			{
				Children = 
				{
					lista
				}
			};

		}
	}


	class CustomListChuchu : ViewCell
	{


		public CustomListChuchu()
		{

			Label firstlbl = new Label
			{
				TextColor = Color.Accent,
				FontSize = 20,
				FontAttributes = Xamarin.Forms.FontAttributes.Italic,
			};
			firstlbl.SetBinding(Label.TextProperty, "FirstName");

			Label lastlbl = new Label
			{
				TextColor = Color.Accent,
				FontSize = 20,
				FontAttributes = Xamarin.Forms.FontAttributes.Italic,
			};
			lastlbl.SetBinding(Label.TextProperty, "LastName");

			Label emaillbl = new Label
			{
				TextColor = Color.Accent,
				FontSize = 10,
				FontAttributes = Xamarin.Forms.FontAttributes.Italic,
			};
			emaillbl.SetBinding(Label.TextProperty, "Email");

			Button btn = new Button
			{
				Text = "Delete",
				TextColor = Color.Black,
				BackgroundColor = Color.White,
				BorderColor = Color.Black
			};
			btn.SetBinding(Button.CommandParameterProperty, new Binding("."));


			StackLayout firstStack = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { firstlbl, lastlbl }
			};

			StackLayout secondstack = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { firstStack, emaillbl }
			};


			StackLayout main = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { secondstack, btn }
			};

			btn.Clicked += async (sender, e) =>
			 {
				 var b = (Button)sender;
				var item = (UserData)b.CommandParameter;
				bool choice = await UserSQL.ssa.DisplayAlert("Clicked",
				                                             item.FirstName + " button was clicked", "Yes", "No");
				 if (choice)
				 {
					App.DAUtil.DeleteUser(item);
					UserSQL.lista.ItemsSource = App.DAUtil.AllUser();
				 }

			 };

			View = main;
		}


	}
}


