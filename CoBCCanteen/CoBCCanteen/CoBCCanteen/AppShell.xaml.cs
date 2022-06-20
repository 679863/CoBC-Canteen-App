using System;
using System.Collections.Generic;
using Xamarin.Forms;
using CoBCCanteen.Views;

namespace CoBCCanteen
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(ItemPage), typeof(ItemPage));
            Routing.RegisterRoute(nameof(Checkout), typeof(Checkout));
        }

    }
}

