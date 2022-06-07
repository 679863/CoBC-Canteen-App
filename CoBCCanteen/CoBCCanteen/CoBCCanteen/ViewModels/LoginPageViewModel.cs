using System;
using System.Windows.Input;
using CoBCCanteen.Views;
using Xamarin.Forms;

namespace CoBCCanteen.ViewModels
{
	public class LoginPageViewModel : BindableObject
	{
		public ICommand LoginUser { get; }
		public ICommand DisplayRegister { get; }

		public LoginPageViewModel()
		{
			LoginUser = new Command(OnLogin);
			DisplayRegister = new Command(GoToRegister);
		}

		private string _id;
		public string ID
        {
			get => _id;
            set
            {
                if (value == _id)
                {
					return;
                }
                else
                {
					_id = value;
					OnPropertyChanged();
                }
            }
        }

		private string _password;
		public string Password
        {
			get => _password;
            set
            {
                if (value == _password)
                {
					return;
                }
                else
                {
					_password = value;
					OnPropertyChanged();
                }
            }
        }

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

