using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoBCCanteen.Views
{	
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage ()
		{
			InitializeComponent();
		}

		private async void Button_Clicked(object sender, EventArgs e)
		{
			await DisplayAlert("Registered", "Your user account has been successfully registered!", "OK");
			await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
		}

		private async void TapGestureRecogniser_Tapped(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
		}
	}
}

