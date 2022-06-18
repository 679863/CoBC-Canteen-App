using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using CoBCCanteen.Models;
using CoBCCanteen.Views;
using Xamarin.Forms;

namespace CoBCCanteen.ViewModels
{
	public class MyAccountViewModel : BindableObject
	{
		// Property for storing the logged in user.
		User activeUser { get; set; }

		// For label binding.
		private string _displayFullname;
		public string DisplayFullname
        {
			get => _displayFullname;
            set
            {
				_displayFullname = value;
				OnPropertyChanged(nameof(DisplayFullname));
            }
        }

		private string _displayID;
		public string DisplayID
        {
			get => _displayID;
            set
            {
				_displayID = value;
				OnPropertyChanged(nameof(DisplayID));
            }
        }

		private string _displayEmail;
		public string DisplayEmail
        {
			get => _displayEmail;
            set
            {
				_displayEmail = value;
				OnPropertyChanged(nameof(DisplayEmail));
            }
        }

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

		// For button binding.
		public ICommand LogoutUser { get; set; }

		public MyAccountViewModel()
		{
			LogoutUser = new Command(async () => await Logout());
		}

		// Called in MyAccount.xaml.cs in OnAppearing. Updates bindings when page appears.
		public void Init()
        {
			activeUser = (App.Current as CoBCCanteen.App).ActiveUser;
			DisplayFullname = $"Name: { activeUser.Firstname } { activeUser.Lastname }";
			DisplayID = $"ID: { activeUser.Id }";
			DisplayEmail = $"Email: { activeUser.Email }";
			DisplayBalance = $"Balance: { (activeUser.Balance / 100).ToString("C", CultureInfo.GetCultureInfo("en-GB")) }";
		}

		async Task Logout()
        {
			(App.Current as CoBCCanteen.App).ActiveUser = null;
			activeUser = null;
			await Shell.Current.GoToAsync($"//{ nameof(LoginPage) }");
        }
	}
}

