﻿using System;
using System.Windows.Input;
using CoBCCanteen.Views;
using Xamarin.Forms;
using CoBCCanteen.Services;
using CoBCCanteen.Models;

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
            if ((_id == null) | (_password == null))
            {
                await Shell.Current.DisplayAlert("Invalid Credentials", "No credentials have been entered! Please try again.", "OK");
            }
            else
            {
                try
                {
                    Console.WriteLine(_id);
                    (App.Current as CoBCCanteen.App).ActiveUser = await UserService.Login(_id, UserService.HashPassword(_password));
                    if ((App.Current as CoBCCanteen.App).ActiveUser != null)
                    {
                        Console.WriteLine((App.Current as CoBCCanteen.App).ActiveUser.Firstname);
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

