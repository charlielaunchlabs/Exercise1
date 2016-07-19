using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Xamarin.Forms;


namespace Exercise1
{
	public class AddUser : ContentPage
	{
		Entry fname = new Entry() { Text=""};
		Entry lname = new Entry() { Text = "" };
		Entry email = new Entry() { Text = "" };


		public async void  saveF() 
		{
			if (fname.Text == "" || lname.Text == "" || email.Text == "")
			{
				await this.DisplayAlert("Null Entry Found", "Sudli", "OK");
			}
			else 
			{
				if (isEMAIL(email.Text) == true)
				{
					try 
					{
						var role = JsonConvert.DeserializeObject<ObservableCollection<User>>(CacheData.user_cache);

						role.Add(new User { first_name = fname.Text, last_name = lname.Text, email = email.Text });

						string json = JsonConvert.SerializeObject(role);

						CacheData.user_cache = json;

						MainPage.list.ItemsSource = role;
						await Navigation.PopModalAsync();
					}
					catch 
					{
						RootObject.user.Add(new User { first_name = fname.Text, last_name = lname.Text, email = email.Text });

						string json = JsonConvert.SerializeObject(RootObject.user);
						CacheData.user_cache = json;
						MainPage.list.ItemsSource = RootObject.user;
						await Navigation.PopModalAsync();
					}
				}
				else { 
					await this.DisplayAlert("Email Validation", "katong email pg high.school nmo", "OK");
				}
			}
		}



		public AddUser()
		{
			this.Title = "Add User";
			var goTo = new ToolbarItem
			{
				Priority = 1,
				Text = "Cancel",
				Command = new Command(async () => await Navigation.PopModalAsync())
			};
			var saVe = new ToolbarItem
			{
				Priority = 0,
				Text = "Save",
				Command = new Command(saveF)
			};
			this.ToolbarItems.Add(goTo);
			this.ToolbarItems.Add(saVe);



			fname.Placeholder = "First Name";
			fname.PlaceholderColor = Color.Black;
			fname.WidthRequest = 250;

			lname.Placeholder = "Last Name";
			lname.PlaceholderColor = Color.Black;
			lname.WidthRequest = 250;

			email.Placeholder = "Email";
			email.PlaceholderColor = Color.Black;
			email.WidthRequest = 250;




			StackLayout main = new StackLayout
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Spacing = 10,
				Children = {
					fname,lname,email
				}
			};

			Content= main ;
		}

		public bool isEMAIL(string x)
		{
			try
			{
				if (Regex.Match(x, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
				{
					return true;
				}
				else if (x == "")
				{
					return false;
				}
				else
				{
					return false;
				}
			}
			catch (System.ArgumentNullException)
			{
				return false;
			}

		}
	}
}


