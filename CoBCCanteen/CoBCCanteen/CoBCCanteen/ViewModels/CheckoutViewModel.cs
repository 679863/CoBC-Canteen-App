using System;
using System.Collections.Generic;
using MvvmHelpers;

namespace CoBCCanteen.ViewModels
{
	public class CheckoutViewModel
	{
		public ObservableRangeCollection<Models.MenuItem> basket { get; set; }

		public CheckoutViewModel()
		{
			basket = new ObservableRangeCollection<Models.MenuItem>();
		}

		public void Init()
        {
			basket.AddRange((App.Current as CoBCCanteen.App).Basket);
        }
	}
}

