using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CoBCCanteen.Models;
using CoBCCanteen.Views;
using Xamarin.Forms;

namespace CoBCCanteen.ViewModels
{
	public class MyAccountViewModel : BindableObject
	{
		public User activeUser { get; set; }

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

		public ICommand LogoutUser { get; set; }

		public MyAccountViewModel()
		{
			LogoutUser = new Command(async () => await Logout());
		}

		public void Init()
        {
			activeUser = (App.Current as CoBCCanteen.App).ActiveUser;
			DisplayFullname = $"Name: { activeUser.Firstname } { activeUser.Lastname }";
			DisplayID = $"ID: { activeUser.Id }";
			DisplayEmail = $"Email: { activeUser.Email }";
			DisplayBalance = $"Balance: { (activeUser.Balance / 100).ToString("C") }";
		}

		async Task Logout()
        {
			(App.Current as CoBCCanteen.App).ActiveUser = null;
			activeUser = null;
			await Shell.Current.GoToAsync($"//{ nameof(LoginPage) }");
        }
	}
}

