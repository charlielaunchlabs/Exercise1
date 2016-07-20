using System;
using System.Collections.ObjectModel;
using XLabs.Platform.Services.Media;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace Exercise1
{
	public class UserProfileSQL : ContentPage
	{

		Entry fname = new Entry() { Text = "" };
		Entry lname = new Entry() { Text = "" };
		Entry email = new Entry() { Text = "" };
		Image image = new Image();
		Button btn = new Button() { Text = "Select Image" };


		public UserProfileSQL()
		{
			this.Title = "SQL Profile";
			var goTo = new ToolbarItem
			{
				Priority = 1,
				Text = "Cancel",
				Command = new Command(async () => await Navigation.PopModalAsync())
			};
			var saVe = new ToolbarItem
			{
				Priority = 0,
				Text = "Edit/Save",
				Command = new Command(saveF)
			};
			this.ToolbarItems.Add(goTo);
			this.ToolbarItems.Add(saVe);



			fname.Text = UserSQL.First_Name;
			fname.WidthRequest = 250;

			lname.Text =UserSQL.Last_Name;
			lname.WidthRequest = 250;

			email.Text = UserSQL.Email;
			email.WidthRequest = 250;

			Content = new StackLayout 
			{ 
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = { fname,lname,email,btn,image}
			};

			btn.Clicked += async(sender, e) => { 
				var device = Resolver.Resolve<IDevice>();
				var mp = device.MediaPicker;
				var options = new XLabs.Platform.Services.Media.CameraMediaStorageOptions();

				XLabs.Platform.Services.Media.MediaFile selectedImage;

					selectedImage = await mp.SelectPhotoAsync(options);
				
				var s = selectedImage.Source;

				image.Source = ImageSource.FromStream(() => s);
			
			};

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

						App.DAUtil.UpdateUser(insert);
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


