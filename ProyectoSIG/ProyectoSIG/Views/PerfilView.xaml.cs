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
            if (NoCheck.IsChecked)
                NoCheck.IsChecked = !e.Value;
        }

        private void No_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (SiCheck.IsChecked)
                SiCheck.IsChecked = !e.Value;
        }
    }
}