using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using CoBCCanteen.Models;
using CoBCCanteen.Services;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;

namespace CoBCCanteen.ViewModels
{
    class OrderPageViewModel : BindableObject
    {
        User activeUser = (App.Current as CoBCCanteen.App).ActiveUser;

        public ICommand RunTest { get; }

        public OrderPageViewModel()
        {
            RunTest = new Command(async() => await TestPass());
        }

        async Task TestPass()
        {
            await Shell.Current.DisplayAlert($"{activeUser.Firstname} {activeUser.Lastname}", $"{activeUser.Id} {activeUser.Email} {activeUser.IsAdmin} {activeUser.Balance} {activeUser.Password}", "OK");
        }
    }
}
