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

		ObservableCollection<ListContent> item = new ObservableCollection<ListContent>();
		public MainPage()
		{


			this.Title = " Users ";
			var goTo = new ToolbarItem
			{
				Text = "Plus",
				Command = new Command(async () => await Navigation.PushModalAsync(new NavigationPage(new AddUser())))
			};
			this.ToolbarItems.Add(goTo);


			ListView list = new ListView()
			{
				ItemTemplate = new DataTemplate(() =>
				{
					var textCell = new TextCell();
					textCell.SetBinding(TextCell.TextProperty, "FullName");
					textCell.SetBinding(TextCell.DetailProperty, "Email");
					return textCell;
				})
			};


			//JsonTextReader reader = new JsonTextReader(new StringReader(AddUser.user_cache));
			//reader.SupportMultipleContent = true;

		//	while (true)
		//	{
				//if (!reader.Read())
				//{
				//	break;
				//}

				//JsonSerializer serializer = new JsonSerializer();
			var role = JsonConvert.DeserializeObject<List< User >>(AddUser.user_cache);

			//roles.Add(role );

		//	}
			item.Clear();
			//foreach (User role in roles)
			//{

			try
			{ 
				for (int i = 0; i < role.Count; i++)
				{
					try
					{
						item.Add(new ListContent() { FullName = role[i].first_name +" "+ role[i].last_name , Email = role[i].email });
						list.ItemsSource = item;
						list.BindingContext = item;
					}
					catch
					{

					}
				}
			}
			catch (System.NullReferenceException) { 
			}

			//}

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


