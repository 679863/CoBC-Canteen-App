using System;
using System.Windows.Input;
using Xamarin.Forms;
using CoBCCanteen.Views;
using Xamarin.CommunityToolkit;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CoBCCanteen.ViewModels
{
	public class RegisterPageViewModel : BindableObject
	{
		public RegisterPageViewModel()
		{
			RegisterUser = new Command(async() => await OnRegister());
			DisplayLogin = new Command(async() => await GoToLogin());
		}

		public ICommand RegisterUser { get; }
		public ICommand DisplayLogin { get; }

		private string firstname;

		public string Firstname
        {
			get => firstname;
            set
            {
                if (value == firstname)
                {
					return;
                }
                else
                {
					firstname = value;
					OnPropertyChanged();
                }
            }
        }

		private List<object> errorsFirstname;

		public List<object> ErrorsFirstname
        {
			get => errorsFirstname;
			set => errorsFirstname = value;
        }

		async Task OnRegister()
		{
			await Shell.Current.DisplayAlert(firstname, "Your user account has been successfully registered!", "OK");
			await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
		}

		async Task GoToLogin()
        {
			await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
		}
	}
}

