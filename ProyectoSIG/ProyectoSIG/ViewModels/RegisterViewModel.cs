using ProyectoSIG.Client;
using ProyectoSIG.Models;
using ProyectoSIG.ViewModels.Base;
using ProyectoSIG.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyectoSIG.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private bool _syncing = false;
        public bool Syncing
        {
            get { return _syncing; }
            set
            {
                if(value != _syncing)
                {
                    _syncing = value;
                    OnPropertyChanged();
                }
            }
        }

        private string username;
        public string Username { 
            get { return username; }
            set { username = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string confirmpass;
        public string ConfirmPass
        {
            get { return confirmpass; }
            set { confirmpass = value; }
        }

        private string _errorText;
        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                if(_errorText != value)
                {
                    _errorText = value;
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
                if(value != _active)
                {
                    _active = value;
                    ((Command)_return).ChangeCanExecute();
                    OnPropertyChanged();
                }
            }
        }

        ICommand _return;
        public ICommand ReturnCommand
        {
            get { return _return; }
        }

        ICommand _register;
        public ICommand Register
        {
            get { return _register; }
        }
        public RegisterViewModel()
        {
            _return = new Command(async () =>
            {
                Active = false;
                await BackButton();
            }, () => { return Active; });

            _register = new Command(async () =>
            {
                Active = false;
                Syncing = true;
                await SendRegister();
                Active = true;
            }, () => { return Active;  });
            
        }

        public async Task BackButton()
        {
            await NavigationService.GoBack(typeof(RegisterView), true);
            Active = true;
        }

        async Task SendRegister()
        {
            if(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPass))
            {
                await DialogService.ShowMessage("Por favor rellenar todos los campos.", "Ups!", "Ok",null);
                return;
            }
            if (ErrorText != null)
            {
                await DialogService.ShowMessage("Las contraseñas no coinciden.", "Ups!", "Ok", null);
                return;
            }
            if(Password.Length <= 4)
            {
                await DialogService.ShowMessage("Las contraseña es muy débil", "Ups!", "Ok", null);
                return;
            }
            ObjetoRespuesta<LoginParameters> objetoRespuesta = await RestClient.Post<LoginParameters>(new LoginParameters { username = Username, password = Password }, "register");
            if (objetoRespuesta.Succesful)
            {
                await DialogService.ShowMessage("Se ha ingresado con éxito.", "Éxito", "Ok",null);
                await NavigationService.GoBack(typeof(RegisterView),true);
            }
            else
            {
                await DialogService.ShowMessage(objetoRespuesta.Mensaje , "Error", "Ok", null);
            }
            Syncing = false;
        }
    }
}
