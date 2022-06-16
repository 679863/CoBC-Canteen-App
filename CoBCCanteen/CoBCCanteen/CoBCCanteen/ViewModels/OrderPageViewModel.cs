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
        // Property for storing logged in user.
        private User activeUser { get; set; }

        public OrderPageViewModel()
        {
            // Move into function called Init(). Call in OrderPage.xaml.cs in OnAppearing(). Will update value when page appears.
            activeUser = (App.Current as CoBCCanteen.App).ActiveUser;
        }
    }
}
