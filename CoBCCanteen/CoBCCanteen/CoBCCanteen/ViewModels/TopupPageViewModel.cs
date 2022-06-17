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

		private string _displayTopupValue;
		public string DisplayTopupValue
        {
			get => _displayTopupValue;
            set
            {
				_displayTopupValue = value;
				OnPropertyChanged(nameof(DisplayTopupValue));
            }
        }

		private double _sliderTopupValue;
		public double SliderTopupValue
        {
			get => _sliderTopupValue;
            set
            {
				_sliderTopupValue = value;
				DisplayTopupValue = value.ToString("C", CultureInfo.GetCultureInfo("en-GB"));
				OnPropertyChanged(nameof(SliderTopupValue));
            }
        }

		public TopupPageViewModel()
		{
		}

		public void Init()
        {
			activeUser = (App.Current as CoBCCanteen.App).ActiveUser;
			DisplayBalance = $"My Balance: { (activeUser.Balance / 100).ToString("C", CultureInfo.GetCultureInfo("en-GB")) }";
			DisplayTopupValue = _sliderTopupValue.ToString("C", CultureInfo.GetCultureInfo("en-GB"));
        }
	}
}

