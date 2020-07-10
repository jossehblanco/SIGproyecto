using ProyectoSIG.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoSIG
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            if (Application.Current.Properties.ContainsKey("Logged"))
            {
                if ((bool)Application.Current.Properties["Logged"])
                {
                    MainPage = new MasterView();
                }
                else
                {
                    MainPage = new LoginView();
                }
            }
            else
            {
                MainPage = new LoginView();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
