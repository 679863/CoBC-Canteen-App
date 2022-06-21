using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoBCCanteen.Models;
using CoBCCanteen.Services;
using MvvmHelpers;
using Xamarin.Forms;

namespace CoBCCanteen.ViewModels
{
	public class CheckoutViewModel : BindableObject
	{
		private string _displayTotal;
		public string DisplayTotal
        {
			get => _displayTotal;
            set
            {
				_displayTotal = value;
				OnPropertyChanged(nameof(DisplayTotal));
            }
        }

		public ObservableRangeCollection<Models.MenuItem> basket { get; set; }

		public ICommand OnCheckout { get; }
		public ICommand OnDelete { get; }
		public ICommand OnClear { get; }

		public CheckoutViewModel()
		{
			basket = new ObservableRangeCollection<Models.MenuItem>();
			OnCheckout = new Command(async () => await Checkout());
			OnClear = new Command(execute: () => ClearBasket());
			OnDelete = new Command<string>(execute: (id) => RemoveFromBasket(id));
		}

		public void Init()
        {
			basket.Clear();
			basket.AddRange((App.Current as CoBCCanteen.App).Basket);
			DisplayTotal = $"Total: { (GetTotal() / 100f).ToString("C", CultureInfo.GetCultureInfo("en-GB"))}";
        }

		int GetTotal()
        {
			int total = 0;

            for (int i = 0; i < (App.Current as CoBCCanteen.App).Basket.Count; i++)
            {
				total += (App.Current as CoBCCanteen.App).Basket[i].Price;
            }

			return total;
        }

		async Task Checkout()
        {
            if (basket.Count > 0)
            {
				int remainingBalance = (App.Current as CoBCCanteen.App).ActiveUser.Balance - GetTotal();

                if (remainingBalance >= 0)
                {
                    try
                    {
						(App.Current as CoBCCanteen.App).ActiveUser.Balance -= GetTotal();
						(App.Current as CoBCCanteen.App).ActiveUser = await UserService.UpdateUserAndGet((App.Current as CoBCCanteen.App).ActiveUser);
						await Shell.Current.DisplayAlert("Order Placed", "Your order has been placed! Please allow time for canteen staff to put your order together. On arrival, tell the canteen staff your college ID.", "OK");
						(App.Current as CoBCCanteen.App).Basket.Clear();
						Init();
					}
                    catch (Exception ex)
                    {
						await Shell.Current.DisplayAlert("Unable To Connect To The Server", "A connection to the server could not be established! Please try again.", "OK");
					}
                }
                else
                {
					await Shell.Current.DisplayAlert("Insufficient Funds", "You do not have enough funds to complete this purchase! Please go to the topup page and add funds.", "OK");
				}
			}
            else
            {
				await Shell.Current.DisplayAlert("Unable To Process", "There are no items in your basket! Please add items and then purchase.", "OK");
			}
		}

		void RemoveFromBasket(string _itemID)
        {
			int itemID = int.Parse(_itemID);
			var match = (App.Current as CoBCCanteen.App).Basket.FirstOrDefault(x => x.Id == itemID);

            if (match != null)
            {
				(App.Current as CoBCCanteen.App).Basket.Remove(match);
				Init();
            }
        }

		void ClearBasket()
        {
			(App.Current as CoBCCanteen.App).Basket.Clear();
			basket.Clear();
			DisplayTotal = "Total: £0.00";
        }
	}
}

