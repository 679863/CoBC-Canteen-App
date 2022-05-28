using System;
using System.Collections.Generic;
using CoBCCanteen.Views;

using Xamarin.Forms;

namespace CoBCCanteen.Views
{	
	public partial class LoginPage : ContentPage
	{	
		public LoginPage ()
		{
			InitializeComponent ();
		}

		private async void Button_Clicked(object sender, EventArgs e)
        {
			await Shell.Current.GoToAsync($"//{ nameof(OrderPage) }");
        }

		private async void TapGestureRecogniser_Tapped(object sender, EventArgs e)
        {
			await Shell.Current.GoToAsync($"{ nameof(RegisterPage) }");
		}
	}
}

