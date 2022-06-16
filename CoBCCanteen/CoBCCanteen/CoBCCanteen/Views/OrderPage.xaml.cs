using System;
using System.Collections.Generic;
using CoBCCanteen.ViewModels;

using Xamarin.Forms;

namespace CoBCCanteen.Views
{	
	public partial class OrderPage : ContentPage
	{	
		public OrderPage ()
		{
			InitializeComponent ();
			BindingContext = new OrderPageViewModel();
		}

		private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
			await Shell.Current.GoToAsync($"//{ nameof(LoginPage) }");
        }
	}
}

