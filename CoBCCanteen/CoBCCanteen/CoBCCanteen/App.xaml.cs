﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CoBCCanteen.Models;

namespace CoBCCanteen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
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

