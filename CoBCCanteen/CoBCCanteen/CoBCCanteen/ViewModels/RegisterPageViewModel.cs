using System;
using System.Windows.Input;
using Xamarin.Forms;
using CoBCCanteen.Views;

namespace CoBCCanteen.ViewModels
{
	public class RegisterPageViewModel : BindableObject
	{
		public RegisterPageViewModel()
		{
			RegisterUser = new Command(OnRegister);
			DisplayLogin = new Command(GoToLogin);
		}

		public ICommand RegisterUser { get; }
		public ICommand DisplayLogin { get; }

		async void OnRegister()
        {
			await Shell.Current.DisplayAlert("Registered", "Your user account has been successfully registered!", "OK");
			await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
		}

		async void GoToLogin()
        {
			await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
		}
	}
}

