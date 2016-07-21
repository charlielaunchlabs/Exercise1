using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Exercise1
{
	public class AddUserSQL : ContentPage
	{

		Entry fname = new Entry() { Text = "" };
		Entry lname = new Entry() { Text = "" };
		Entry email = new Entry() { Text = "" };


		public AddUserSQL()
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

			Content = main;

		}

		public async void saveF()
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
						var insert = new UserData()
						{
							FirstName = fname.Text,
							LastName = lname.Text,
							Email = email.Text
						};

						App.DAUtil.AddUser(insert);
						var all = App.DAUtil.AllUser();
						var oc = new ObservableCollection<UserData>(all);
						UserSQL.lista.ItemsSource = oc;
						await Navigation.PopModalAsync();
					}
					catch { }
				}
				else {
					await this.DisplayAlert("Email Validation", "katong email pg high.school nmo", "OK");
				}
			}
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


