using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Exercise1
{
	public class ObservesItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		User item;

		public ObservesItem()
		{
			item = new User();
		}

		public string FullName
		{
			set
			{
				if (!value.Equals(item.first_name, StringComparison.Ordinal))
				{
					item.first_name = value;
					OnPropertyChanged("first_name");
				}
			}
			get
			{
				return item.first_name;
			}
		}

		public string Email
		{
			set
			{
				if (!value.Equals(item.email, StringComparison.Ordinal))
				{
					item.last_name = value;
					OnPropertyChanged("email");
				}
			}
			get
			{
				return item.email;
			}
		}

		void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}

