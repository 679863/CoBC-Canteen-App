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

        private void ItemDelete_Tapped(object sender, EventArgs e)
        {
            Image img = (Image)sender;
            TapGestureRecognizer tgr = (TapGestureRecognizer)img.GestureRecognizers[0];
            string parameter = tgr.CommandParameter.ToString();

            if (viewModel.OnDelete.CanExecute(parameter))
            {
                viewModel.OnDelete.Execute(parameter);
            }
        }
    }
}

