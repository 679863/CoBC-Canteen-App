using System;
using System.Windows.Input;
using CoBCCanteen.Views;
using Xamarin.Forms;

namespace CoBCCanteen.ViewModels
{
	public class LoginPageViewModel
	{
		public LoginPageViewModel()
		{
			LoginUser = new Command(OnLogin);
			DisplayRegister = new Command(GoToRegister);
		}

		public ICommand LoginUser { get; }
		public ICommand DisplayRegister { get; }

		async void OnLogin()
        {
			await Shell.Current.GoToAsync($"//{ nameof(OrderPage) }");
		}

		async void GoToRegister()
        {
			await Shell.Current.GoToAsync($"{ nameof(RegisterPage) }");
		}
	}
}

