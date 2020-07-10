using Newtonsoft.Json;
using ProyectoSIG.Client;
using ProyectoSIG.Helpers;
using ProyectoSIG.Models;
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
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private async void OnRegistrarse_Clicked(object sender, EventArgs e)
        {
            if (Navigation.ModalStack.Count == 0 || Navigation.ModalStack.Last().GetType() != typeof(RegisterView))
                await Navigation.PushModalAsync(new RegisterView());
        }

        private void OnUsuario_Completed(object sender, EventArgs e)
        {
            password.Focus();
        }
    }
}