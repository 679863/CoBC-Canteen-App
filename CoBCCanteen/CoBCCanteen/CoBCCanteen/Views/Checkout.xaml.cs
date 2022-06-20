using System;
using System.Collections.Generic;
using CoBCCanteen.ViewModels;
using Xamarin.Forms;

namespace CoBCCanteen.Views
{	
	public partial class Checkout : ContentPage
	{
		CheckoutViewModel viewModel;

		public Checkout ()
		{
			InitializeComponent ();
			viewModel = this.BindingContext as CheckoutViewModel;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel != null)
            {
                viewModel.Init();
            }
        }
    }
}

