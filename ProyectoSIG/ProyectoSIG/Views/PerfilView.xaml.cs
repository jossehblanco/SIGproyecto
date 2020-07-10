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
    public partial class PerfilView : ContentPage
    {
        public PerfilView()
        {
            InitializeComponent();
        }

        private void Si_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }

        private void No_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }
    }
}