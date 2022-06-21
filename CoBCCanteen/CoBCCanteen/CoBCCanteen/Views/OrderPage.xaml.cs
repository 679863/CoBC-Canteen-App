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

		private void CarouselItem_Tapped(object sender, EventArgs e)
		{
            StackLayout sl = (StackLayout)sender;
            TapGestureRecognizer tgr = (TapGestureRecognizer)sl.GestureRecognizers[0];
            string parameter = tgr.CommandParameter.ToString();

            if (viewModel.DisplayItemPage.CanExecute(parameter))
            {
                viewModel.DisplayItemPage.Execute(parameter);
            }
		}

		private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
			await Shell.Current.GoToAsync($"//{ nameof(OrderPage) }/{ nameof(Checkout) }");
        }
	}
}

