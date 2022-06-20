using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoBCCanteen.Services;
using CoBCCanteen.ViewModels;
using Xamarin.Forms;

namespace CoBCCanteen.Views
{
	public partial class ItemPage : ContentPage
	{
		ItemPageViewModel viewModel;

		public ItemPage ()
		{
			InitializeComponent ();
			viewModel = this.BindingContext as ItemPageViewModel;
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
            await Shell.Current.GoToAsync($"//{ nameof(OrderPage) }/{ nameof(ItemPage) }/{ nameof(Checkout) }");
        }
    }
}

