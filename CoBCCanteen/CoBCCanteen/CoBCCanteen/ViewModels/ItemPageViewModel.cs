using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CoBCCanteen.Services;
using Xamarin.Forms;

namespace CoBCCanteen.ViewModels
{
	[QueryProperty("ItemID", "itemID")]
	public class ItemPageViewModel : BindableObject
	{
		private string _itemID;
		public string ItemID
		{
			get => _itemID;
			set
			{
				_itemID = value;
				OnPropertyChanged();
			}
		}

		private Models.MenuItem _item;
		public Models.MenuItem item
		{
			get => _item;
			set
			{
				_item = value;
				OnPropertyChanged();
			}
		}

		private string _displayQuantity;
		public string DisplayQuantity
        {
			get => _displayQuantity;
            set
            {
				_displayQuantity = value;
				OnPropertyChanged(nameof(DisplayQuantity));
            }
        }

		public ICommand OnDecrease { get; }
		public ICommand OnIncrease { get; }
		public ICommand OnAddToBasket { get; }

		public ItemPageViewModel()
		{
			OnDecrease = new Command(execute: () => DecreaseQuantity());
			OnIncrease = new Command(execute: () => IncreaseQuantity());
			OnAddToBasket = new Command(execute: () => AddToBasket());
		}

		public Task Init()
        {
			DisplayQuantity = "1";
			return Task.Run(async () => await UpdateItem());
        }

		public async Task UpdateItem()
        {
			item = await MenuService.GetItemByID(int.Parse(ItemID));
        }

		void DecreaseQuantity()
        {
			int quantity = int.Parse(DisplayQuantity);
			quantity--;

            if (quantity < 1)
            {
				DisplayQuantity = "1";
            }
            else
            {
				DisplayQuantity = quantity.ToString();
            }
        }

		void IncreaseQuantity()
		{
			int quantity = int.Parse(DisplayQuantity);
			quantity++;

			if (quantity > 3)
			{
				DisplayQuantity = "3";
			}
			else
			{
				DisplayQuantity = quantity.ToString();
			}
		}

		async void AddToBasket()
        {
            for (int i = 0; i < int.Parse(DisplayQuantity); i++)
            {
				(App.Current as CoBCCanteen.App).Basket.Add(item);
            }

			await Shell.Current.DisplayAlert("Item Added To Basket", $"{ DisplayQuantity }x { item.Name } has been added to your basket!", "OK");
        }
	}
}

