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
    [QueryProperty("UserID", "id")]
    class OrderPageViewModel : BindableObject
    {
        private string _userID;
        public string UserID
        {
            get => _userID;
            set
            {
                _userID = value;
            }
        }

        public ICommand RunTest { get; }

        public OrderPageViewModel()
        {
            RunTest = new Command(async() => await TestPass());
        }

        async Task TestPass()
        {
            //await Shell.Current.DisplayAlert($"{_activeUser.Firstname} {_activeUser.Lastname}", $"{_activeUser.Id} {_activeUser.Email} {_activeUser.IsAdmin} {_activeUser.Balance}", "OK");
        }
    }
}
