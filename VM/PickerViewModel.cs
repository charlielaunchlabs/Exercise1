using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;

namespace Exercise1
{
	[ViewType(typeof(UserProfileSQL))]
	public class MyTestViewModel : XLabs.Forms.Mvvm.ViewModel
	{
		public MyTestViewModel()
		{
		}

		private string _textMessage = "My Test";
		public string TextMessage
		{
			get
			{
				return _textMessage;
			}
			set
			{
				this.SetProperty(ref _textMessage, value);
			}
		}

		private Command _cmdToVerifyIsWorking;
		public Command CmdToVerifyIsWorking
		{
			get
			{
				return _cmdToVerifyIsWorking ?? (_cmdToVerifyIsWorking = new Command(
					(m) => VerifyIsWoking(),
					(o) => true));
			}
		}

		private void VerifyIsWoking()
		{
			this.TextMessage = "YES! It seems to be working!!";
			return;
		}

		/*******************************************************************************************/

		/// <summary>
		/// The _picture chooser.
		/// </summary>
		private IMediaPicker mediaPicker;

		private Command _cmdToUseMediaPicker;
		public Command CmdToUseMediaPicker
		{
			get
			{
				return _cmdToUseMediaPicker ?? (_cmdToUseMediaPicker = new Command(
					async (m) => await UseMediaPicker(),
					(o) => true));
			}
		}

		private async Task UseMediaPicker()
		{
			Setup();
			await SelectPicture();

			return;
		}

		/// <summary>
		/// Setups this instance.
		/// </summary>
		private void Setup()
		{
			if (mediaPicker != null)
			{
				return;
			}

			var device = Resolver.Resolve<IDevice>();

			mediaPicker = Resolver.Resolve<IMediaPicker>();
			////RM: hack for working on windows phone? 
			if (mediaPicker == null)
			{
				mediaPicker = device.MediaPicker;
			}
		}

		/// <summary>
		/// Selects the picture.
		/// </summary>
		/// <returns>Select Picture Task.</returns>
		private async Task SelectPicture()
		{
			Setup();

			try
			{
				var mediaFile = await this.mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
				{
					DefaultCamera = CameraDevice.Front,
					MaxPixelDimension = 400
				});

				ImageSource imageSource = ImageSource.FromStream(() => mediaFile.Source);

				var sourceLength = mediaFile.Source.Length;

				Debug.WriteLine("SelectPicture.sourceLength." + sourceLength.ToString());
			}
			catch (System.Exception ex)
			{
				Debug.WriteLine("SelectPicture.Exception. " + ex.Message);
			}
		}
	}
}

