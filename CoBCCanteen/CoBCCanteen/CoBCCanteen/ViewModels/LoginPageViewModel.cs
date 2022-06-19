using System;
using System.Windows.Input;
using CoBCCanteen.Views;
using Xamarin.Forms;
using CoBCCanteen.Services;
using CoBCCanteen.Models;
using System.IO;

namespace CoBCCanteen.ViewModels
{
	public class LoginPageViewModel : BindableObject
	{
        // For entry binding.
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

        // For button binding.
        public ICommand LoginUser { get; }
		public ICommand DisplayRegister { get; }

        public LoginPageViewModel()
		{
			LoginUser = new Command(OnLogin);
			DisplayRegister = new Command(GoToRegister);
        }

		async void OnLogin()
        {
            await MenuService.DeleteDatabse();

            if ((_id == null) | (_password == null))
            {
                await Shell.Current.DisplayAlert("Invalid Credentials", "No credentials have been entered! Please try again.", "OK");
            }
            else
            {
                try
                {
                    // Stores logged in user to ActiveUser in App.xaml.cs.
                    (App.Current as CoBCCanteen.App).ActiveUser = await UserService.Login(_id, UserService.HashPassword(_password));
                    if ((App.Current as CoBCCanteen.App).ActiveUser != null)
                    {
                        await Shell.Current.GoToAsync($"//{ nameof(OrderPage) }");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Invalid Credentials", "No existing user matches the entered credentials! Please try again.", "OK");
                    }

                }
                catch (Exception)
                {
                    await Shell.Current.DisplayAlert("Unable To Connect To The Server", "A connection to the server could not be established! Please try again.", "OK");
                }
            }
		}

		async void GoToRegister()
        {
			await Shell.Current.GoToAsync($"{ nameof(RegisterPage) }");
		}
	}
}

