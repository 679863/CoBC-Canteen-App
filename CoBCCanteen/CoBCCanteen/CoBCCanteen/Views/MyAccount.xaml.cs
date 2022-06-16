using System;
using System.Collections.Generic;
using CoBCCanteen.ViewModels;
using Xamarin.Forms;

namespace CoBCCanteen.Views
{	
	public partial class MyAccount : ContentPage
	{
		MyAccountViewModel viewModel;

		public MyAccount ()
		{
			InitializeComponent ();
			viewModel = this.BindingContext as MyAccountViewModel;
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

