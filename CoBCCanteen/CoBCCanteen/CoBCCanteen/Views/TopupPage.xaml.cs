using System;
using System.Collections.Generic;
using CoBCCanteen.ViewModels;
using Xamarin.Forms;

namespace CoBCCanteen.Views
{	
	public partial class TopupPage : ContentPage
	{
		TopupPageViewModel viewModel;

		public TopupPage ()
		{
			InitializeComponent ();
            viewModel = this.BindingContext as TopupPageViewModel;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel != null)
            {
                viewModel.Init();
            }
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            double stepValue = 1.0;
            var newStep = Math.Round(e.NewValue / stepValue);

            SliderValue.Value = newStep * stepValue;
        }
    }
}

