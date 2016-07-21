using System;
using System.Collections.ObjectModel;
using XLabs.Platform.Services.Media;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using PCLStorage;

namespace Exercise1
{
	

	public class UserProfileSQL : ContentPage
	{

		Entry fname = new Entry() { Text = "" };
		Entry lname = new Entry() { Text = "" };
		Entry email = new Entry() { Text = "" };
		Image image = new Image();
		Button btn = new Button() { Text = "Select Image" };
		XLabs.Platform.Services.Media.MediaFile selectedImage;
		public string nm= "";
		IFileSystem FileSys { get { return FileSystem.Current; } }

		UserData clickedData;
			
		public UserProfileSQL(UserData data)
		{

			clickedData = data;


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

			image.Source = clickedData.Image;
			Content = new StackLayout 
			{ 
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = { fname,lname,email,btn,image}
			};

			btn.Clicked += async(sender, e) => { 
				var device = Resolver.Resolve<IDevice>();
				var mp = device.MediaPicker;
				var options = new XLabs.Platform.Services.Media.CameraMediaStorageOptions();

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
						clickedData.FirstName = fname.Text;
						clickedData.LastName = lname.Text;
						clickedData.Email = email.Text;
						clickedData.Image = selectedImage.Path;	             


						App.DAUtil.UpdateUser(clickedData);
						var all = App.DAUtil.AllUser();
						var oc = new ObservableCollection<UserData>(all);
						UserSQL.lista.ItemsSource = oc;

						//IFolder rootFolder = FileSystem.Current.LocalStorage;
						//IFolder folder = await rootFolder.CreateFolderAsync("PhotoStorage",CreationCollisionOption.OpenIfExists);
						//IFile file = await FileSystem.Current.GetFileFromPathAsync(selectedImage.Path);

						////IFile files = await folder.CreateFileAsync(file.Path, CreationCollisionOption.ReplaceExisting);
						//await file.MoveAsync(folder.Path,NameCollisionOption.GenerateUniqueName);
						////byte[] buffer = new byte[100];
						////using (System.IO.Stream stream = await files.OpenAsync(FileAccess.ReadAndWrite))
						////{
						////	stream.Write(buffer, 0, 100);
						////}


						await Navigation.PopModalAsync();
						// nm = file.Path;
						//await DisplayAlert(nm, nm, "OK");
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


