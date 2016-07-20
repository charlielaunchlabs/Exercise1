using System;

using Xamarin.Forms;

namespace Exercise1
{
	public class ProfilePage : ContentPage
	{
		
		public ProfilePage()
		{
			this.Title = "Profile";

			Button btn = new Button { Text = "Back" };
			Content = new StackLayout
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					new Label 
					{ 
						HorizontalTextAlignment = TextAlignment.Center,
						Text = MainPage.First_Name+" "+MainPage.Last_Name  
					},
					new Label { Text = MainPage.Email },
					btn
				}
			};


			btn.Clicked += async (sender, e) => {
				await Navigation.PopModalAsync();
			};
		}
	}
}


