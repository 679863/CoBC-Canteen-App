using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CoBCCanteen.Models;

namespace CoBCCanteen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public User ActiveUser { get; set; }

        public App ()
        {
            InitializeComponent();
            MainPage = new AppShell();
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

