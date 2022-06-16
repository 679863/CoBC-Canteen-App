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
        private User activeUser { get; set; }

        public ICommand RunTest { get; }

        public OrderPageViewModel()
        {
            activeUser = (App.Current as CoBCCanteen.App).ActiveUser;
            Console.WriteLine((App.Current as CoBCCanteen.App).ActiveUser.Lastname);
            RunTest = new Command(async() => await TestPass());
        }

        async Task TestPass()
        {
            await Shell.Current.DisplayAlert($"{activeUser.Firstname} {activeUser.Lastname}", $"{activeUser.Id} {activeUser.Email} {activeUser.IsAdmin} {activeUser.Balance} {activeUser.Password}", "OK");
        }
    }
}
