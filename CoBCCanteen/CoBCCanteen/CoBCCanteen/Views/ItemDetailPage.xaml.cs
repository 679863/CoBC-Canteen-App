using System.ComponentModel;
using Xamarin.Forms;
using CoBCCanteen.ViewModels;

namespace CoBCCanteen.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
