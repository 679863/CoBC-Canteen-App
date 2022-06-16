using System;
using System.Collections.Generic;
using CoBCCanteen.ViewModels;
using CoBCCanteen.Views;

using Xamarin.Forms;

namespace CoBCCanteen.Views
{	
	public partial class LoginPage : ContentPage
	{	
		public LoginPage ()
		{
			InitializeComponent ();
			BindingContext = new LoginPageViewModel();
		}
	}
}

