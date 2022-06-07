using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CoBCCanteen.Models;

namespace CoBCCanteen
{
    public partial class App : Application
    {
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

