using ProyectoSIG.Client;
using ProyectoSIG.Helpers;
using ProyectoSIG.Models;
using ProyectoSIG.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProyectoSIG.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private bool _syncing = false;
        public bool Syncing
        {
            get { return _syncing; }
            set
            {
                if (value != _syncing)
                {
                    _syncing = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _active = true;
        public bool Active
        {
            get { return _active; }
            set
            {
                if(_active != value)
                {
                    _active = value;
                    ((Command)_login).ChangeCanExecute();
                    OnPropertyChanged();
                }
            }
        }

        private bool _error = false;
        public bool Error
        {
            get { return _error; }
            set
            {
                if(_error != value)
                {
                    _error = value;
                    OnPropertyChanged();
                }
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        ICommand _login;
        public ICommand Login
        {
            get { return _login; }
        }

        public LoginViewModel()
        {
            _login = new Command(async () =>
            {
                Active = false;
                Syncing = true;
                await LoginApi();
            });
        }

        async Task LoginApi()
        {
            Error = false;
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                Error = true;
                Username = null;
                Password = null;
            }
            else
            {
                LoginParameters login = new LoginParameters { username = Username, password = Password };
                RestClient.AddUserPassHeader(login);
                ObjetoRespuesta<Token> objetoRespuesta = await RestClient.PostForToken("login");
                if (!objetoRespuesta.Succesful)
                {
                    Error = true;
                    //await DisplayAlert("Error", objetoRespuesta.Mensaje, "Ok");
                }
                else
                {
                    NavigationService.SignIn(objetoRespuesta.ObjetosRecuperados[0].user_id, objetoRespuesta.ObjetosRecuperados[0].access_token);
                }
            }
            Syncing = false;
            Active = true;
        }
    }
}
