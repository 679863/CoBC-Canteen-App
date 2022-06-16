using System;
using System.Globalization;
using CoBCCanteen.Models;
using Xamarin.Forms;

namespace CoBCCanteen.ViewModels
{
	public class TopupPageViewModel : BindableObject
	{
		User activeUser { get; set; }

		private string _displayBalance;
		public string DisplayBalance
        {
			get => _displayBalance;
            set
            {
				_displayBalance = value;
				OnPropertyChanged(nameof(DisplayBalance));
            }
        }

		public TopupPageViewModel()
		{
		}

		public void Init()
        {
			activeUser = (App.Current as CoBCCanteen.App).ActiveUser;
			DisplayBalance = $"Balance: { (activeUser.Balance / 100).ToString("C", CultureInfo.GetCultureInfo("en-GB")) }";
        }
	}
}

