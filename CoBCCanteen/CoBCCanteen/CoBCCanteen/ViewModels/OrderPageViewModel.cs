using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using CoBCCanteen.Models;
using CoBCCanteen.Services;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using System.Collections.ObjectModel;
using MvvmHelpers;

namespace CoBCCanteen.ViewModels
{
    class OrderPageViewModel : BindableObject
    {
        // Property for storing logged in user.
        private User activeUser { get; set; }

        List<Models.MenuItem> mainItems { get; set; }
        List<Models.MenuItem> snackItems { get; set; }
        List<Models.MenuItem> drinkItems { get; set; }

        public ObservableRangeCollection<Models.MenuItem> MainItems { get; set; }
        public ObservableRangeCollection<Models.MenuItem> SnackItems { get; set; }
        public ObservableRangeCollection<Models.MenuItem> DrinkItems { get; set; }

        public OrderPageViewModel()
        {
            activeUser = (App.Current as CoBCCanteen.App).ActiveUser;
            MainItems = new ObservableRangeCollection<Models.MenuItem>();
            SnackItems = new ObservableRangeCollection<Models.MenuItem>();
            DrinkItems = new ObservableRangeCollection<Models.MenuItem>();
        }

        public Task Init()
        {
            return Task.Run(async () => await UpdateMenu());
        }

        async Task UpdateMenu()
        {
            mainItems = await MenuService.GetMainItems();
            snackItems = await MenuService.GetSnackItems();
            drinkItems = await MenuService.GetDrinkItems();

            MainItems.AddRange(mainItems);
            SnackItems.AddRange(snackItems);
            DrinkItems.AddRange(drinkItems);
        }
    }
}
