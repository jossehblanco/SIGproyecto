using ProyectoSIG.ViewModels;
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
    public partial class RegisterView : ContentPage
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            //(BindingContext as RegisterViewModel).BackButton();
            return base.OnBackButtonPressed();
        }

        private void Confirmacion_Changed(object sender, TextChangedEventArgs e)
        {
            if(e.NewTextValue.Length > 0)
            {
                string compare = password.Text ?? "";
                if (compare.Equals(e.NewTextValue)) 
                {
                    (BindingContext as RegisterViewModel).ErrorText = null;
                }
                else
                {
                    (BindingContext as RegisterViewModel).ErrorText = "Las contraseñas no coinciden";
                }
            }
            else
            {
                (BindingContext as RegisterViewModel).ErrorText = null;
            }
        }

        private void OnUsuarioCompleted(object sender, EventArgs e)
        {
            password.Focus();
        }

        private void OnPasswordCompleted(object sender, EventArgs e)
        {
            confirmacion.Focus();
        }
    }
}