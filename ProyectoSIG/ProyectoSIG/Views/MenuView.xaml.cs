using ProyectoSIG.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoSIG.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuView : ContentPage
    {
        MasterView RootPage { get => Application.Current.MainPage as MasterView; }
        List<HomeMenuItem> menuItems;

        public MenuView()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>(new[]
                {
                    new HomeMenuItem { Id = ViewPages.MapView, Title = "Ver Mapa", Imagen = "agregartran" },
                    new HomeMenuItem { Id = ViewPages.AboutView, Title = "Acerca de Nosotros", Imagen="detalles.png" }

                });

            MenuItemsListView.ItemsSource = menuItems;

            MenuItemsListView.SelectedItem = menuItems[0];

            MenuItemsListView.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}