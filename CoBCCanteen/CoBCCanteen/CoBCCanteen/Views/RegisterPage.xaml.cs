using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using CoBCCanteen.ViewModels;

namespace CoBCCanteen.Views
{	
	public partial class RegisterPage : ContentPage
	{
		RegisterPageViewModel viewModel;

		public RegisterPage ()
		{
			InitializeComponent();
			viewModel = this.BindingContext as RegisterPageViewModel;
		}

		private async void btnRegister_Clicked(Object sender, EventArgs e)
        {
			await FirstnameValidation.ForceValidate();
			await LastnameValidation.ForceValidate();
			await IDValidation.ForceValidate();
			await EmailValidation.ForceValidate();
			await PasswordValidation.ForceValidate();
			await PasswordConfirmValidation.ForceValidate();

            if (viewModel.RegisterUser.CanExecute(null))
            {
				viewModel.RegisterUser.Execute(null);
            }
		}
    }
}

