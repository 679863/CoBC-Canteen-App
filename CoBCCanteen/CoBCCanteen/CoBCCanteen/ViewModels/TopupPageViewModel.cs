using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoBCCanteen.Models;
using CoBCCanteen.Services;
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

		private string _cardNumber;
		public string CardNumber
        {
			get => _cardNumber;
            set
            {
				_cardNumber = value;
				OnPropertyChanged();
            }
        }

		private string _expiryDate;
		public string ExpiryDate
        {
			get => _expiryDate;
            set
            {
				_expiryDate = value;
				OnPropertyChanged();
            }
        }

		private string _cvv;
		public string CVV
        {
			get => _cvv;
            set
            {
				_cvv = value;
				OnPropertyChanged();
            }
        }

		private bool _isCardNumberValid;
		public bool IsCardNumberValid
        {
			get => _isCardNumberValid;
            set
            {
				_isCardNumberValid = value;
				OnPropertyChanged();
            }
        }

		private bool _isExpiryDateValid;
		public bool IsExpiryDateValid
		{
			get => _isExpiryDateValid;
			set
			{
				_isExpiryDateValid = value;
				OnPropertyChanged();
			}
		}

		private bool _isCVVValid;
		public bool IsCVVValid
		{
			get => _isCVVValid;
			set
			{
				_isCVVValid = value;
				OnPropertyChanged();
			}
		}

		private List<object> _errorCardNumber;
		public List<object> errorCardNumber
		{
			get => _errorCardNumber;
			set
			{
				if (value != null)
				{
					_errorCardNumber = value;
				}
			}
		}

		private List<object> _errorExpiryDate;
		public List<object> errorExpiryDate
		{
			get => _errorExpiryDate;
			set
			{
				if (value != null)
				{
					_errorExpiryDate = value;
				}
			}
		}

		private List<object> _errorCVV;
		public List<object> errorCVV
		{
			get => _errorCVV;
			set
			{
				if (value != null)
				{
					_errorCVV = value;
				}
			}
		}

		public ICommand Topup { get; }

		public TopupPageViewModel()
		{
			Topup = new Command(async () => await OnTopup());
		}

		public void Init()
        {
			activeUser = (App.Current as CoBCCanteen.App).ActiveUser;
			DisplayBalance = $"My Balance: { (activeUser.Balance / 100).ToString("C", CultureInfo.GetCultureInfo("en-GB")) }";
			DisplayTopupValue = _sliderTopupValue.ToString("C", CultureInfo.GetCultureInfo("en-GB"));
			SliderTopupValue = 0.0;
			CardNumber = String.Empty;
			ExpiryDate = String.Empty;
			CVV = String.Empty;
        }

		async Task<bool> ValidateSliderValue()
		{
			bool valid = false;

			if (_sliderTopupValue > 0.0)
			{
				valid = true;
			}
			else
			{
				valid = false;

				await Shell.Current.DisplayAlert("Invalid Topup Amount", $"The entered topup amount is invalid! The value must be £1 or more. Please try again.", "OK");
			}

			return valid;
		}

		async Task<bool> ValidateCardNumber()
		{
			bool valid = false;
			StringBuilder sb = new StringBuilder();

			if (_isCardNumberValid)
			{
				valid = true;
			}
			else
			{
				valid = false;
				sb.Clear();

				foreach (var error in _errorCardNumber)
				{
					if (error is string)
					{
						sb.Append(((string)error).ToString() + " ");
					}
				}

				await Shell.Current.DisplayAlert("Invalid Card Number", $"The entered card number is invalid! {sb.ToString()}Please try again.", "OK");
			}

			return valid;
		}

		async Task<bool> ValidateExpiryDate()
		{
			bool valid = false;
			StringBuilder sb = new StringBuilder();

			if (_isExpiryDateValid)
			{
				valid = true;
			}
			else
			{
				valid = false;
				sb.Clear();

				foreach (var error in _errorExpiryDate)
				{
					if (error is string)
					{
						sb.Append(((string)error).ToString() + " ");
					}
				}

				await Shell.Current.DisplayAlert("Invalid Expiry Date", $"The entered expiry date is invalid! {sb.ToString()}Please try again.", "OK");
			}

			return valid;
		}

		async Task<bool> ValidateCVV()
		{
			bool valid = false;
			bool formatValid = false;
			StringBuilder sb = new StringBuilder();

            if (int.Parse(_cvv.Substring(0, 2)) > 12)
            {
				formatValid = false;
				_errorCVV.Add("The value of the month cannot be greater than 12.");
            }
            else
            {
				formatValid = true;
            }

			if (_isCVVValid && formatValid)
			{
				valid = true;
			}
			else
			{
				valid = false;
				sb.Clear();

				foreach (var error in _errorCVV)
				{
					if (error is string)
					{
						sb.Append(((string)error).ToString() + " ");
					}
				}

				await Shell.Current.DisplayAlert("Invalid CVV", $"The entered CVV is invalid! {sb.ToString()}Please try again.", "OK");
			}

			return valid;
		}

		async Task OnTopup()
        {
			bool isSliderValueValid = await ValidateSliderValue();
			bool isCardNumberValid = await ValidateCardNumber();
			bool isExpiryDateValid = await ValidateExpiryDate();
			bool isCVVValid = await ValidateCVV();

            if (isSliderValueValid && isCardNumberValid && isExpiryDateValid && isCVVValid)
            {
				activeUser.Balance += (int)_sliderTopupValue * 100;

                try
                {
					(App.Current as CoBCCanteen.App).ActiveUser = await UserService.UpdateUserAndGet(activeUser);
					await Shell.Current.DisplayAlert("Topup Successful", $"The topup was a success! { _sliderTopupValue.ToString("C", CultureInfo.GetCultureInfo("en-GB")) } has been added to your account.", "OK");
					Init();
				}
                catch (Exception ex)
                {
					activeUser.Balance -= (int)_sliderTopupValue * 100;
					await Shell.Current.DisplayAlert("Topup Unsuccessful", "The topup was unsuccessful. Please try again!", "OK");
                }
            }
        }
	}
}

