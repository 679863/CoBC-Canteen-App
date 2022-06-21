using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CoBCCanteen.Models;
using System.Collections.Generic;
using System.Globalization;

namespace CoBCCanteen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        // Keeps track of the logged in user. Accessible from all viewmodels.
        private User _activeUser;
        public User ActiveUser
        {
            get => _activeUser;
            set
            {
                if (_activeUser != value)
                {
                    _activeUser = value;
                }
            }
        }

        public List<Models.MenuItem> Basket { get; set; }

        public App ()
        {
            InitializeComponent();
            MainPage = new AppShell();
            Basket = new List<Models.MenuItem>();
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-GB");
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

