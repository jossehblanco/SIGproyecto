using ProyectoSIG.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoSIG.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterView : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MasterView()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            Detail = new NavigationPage(new MapView());

            MenuPages.Add((int)ViewPages.MapView, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)ViewPages.MapView:
                        MenuPages.Add(id, new NavigationPage(new MapView()));
                        break;
                    case (int)ViewPages.AboutView:
                        MenuPages.Add(id, new NavigationPage(new AboutView()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}