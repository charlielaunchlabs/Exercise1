using System;

using Xamarin.Forms;

namespace Exercise1
{
	public class MainPage : ContentPage
	{
		public MainPage()
		{


			this.Title = " Users ";
			var goTo = new ToolbarItem
			{
				Text = "Plus",
				Command = new Command(async () => await Navigation.PushModalAsync(new NavigationPage(new AddUser())))
			};
			this.ToolbarItems.Add(goTo);


			ListView list = new ListView();



			Content = new StackLayout
			{

				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					list
				}
			};
		}
	}
}


