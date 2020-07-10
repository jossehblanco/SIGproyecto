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
                    new HomeMenuItem { Id = ViewPages.MapView, Title = "Ver Mapa", Imagen = "map.png" },
                    new HomeMenuItem { Id = ViewPages.PerfilView, Title = "Perfil", Imagen = "userprofile.png" },
                    new HomeMenuItem { Id = ViewPages.AboutView, Title = "Acerca de Nosotros", Imagen="about.png" }

                });

            MenuItemsListView.ItemsSource = menuItems;

            //MenuItemsListView.SelectedItem = menuItems[0];

            MenuItemsListView.ItemTapped += async (sender, e) =>
            {
                if (e.Item == null)
                    return;

                var id = (int)((HomeMenuItem)e.Item).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}