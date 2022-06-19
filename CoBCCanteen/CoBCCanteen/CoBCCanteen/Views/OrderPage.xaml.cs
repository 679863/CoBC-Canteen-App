using System;
using System.Collections.Generic;
using CoBCCanteen.ViewModels;

using Xamarin.Forms;

namespace CoBCCanteen.Views
{	
	public partial class OrderPage : ContentPage
	{
		OrderPageViewModel viewModel;

		public OrderPage ()
		{
			InitializeComponent ();
			viewModel = this.BindingContext as OrderPageViewModel;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel != null)
            {
				viewModel.Init();
            }
        }

		private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
			await Shell.Current.GoToAsync($"//{ nameof(LoginPage) }");
        }
	}
}

