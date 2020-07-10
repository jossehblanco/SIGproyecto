using Newtonsoft.Json;
using ProyectoSIG.Client;
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

        private void OnRegistrarse_Clicked(object sender, EventArgs e)
        {

        }

        private async void OnLogIn_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(usuario.Text) || string.IsNullOrEmpty(password.Text))
            {
                error.IsVisible = true;
                usuario.Text = null;
                password.Text = null;
            }
            else
            {
                error.IsVisible = false;
                LoginParameters login = new LoginParameters { username = usuario.Text, password = password.Text };
                RestClient.AddUserPassHeader(login);
                ObjetoRespuesta<Token> objetoRespuesta = await RestClient.PostForToken("login");
                if (!objetoRespuesta.Succesful)
                {
                    error.IsVisible = true;
                    await DisplayAlert("Error", objetoRespuesta.Mensaje, "Ok");
                }
                else
                {
                    Application.Current.Properties["token"] = objetoRespuesta.ObjetosRecuperados[0].access_token;
                    Application.Current.Properties["UserId"] = objetoRespuesta.ObjetosRecuperados[0].user_id;
                    Application.Current.Properties["Logged"] = true;
                    Application.Current.MainPage = new MasterView();
                }

            }
        }

        private void OnUsuario_Completed(object sender, EventArgs e)
        {
            password.Focus();
        }
    }
}