using ProyectoSIG.Services;
using ProyectoSIG.Services.Dialog;
using ProyectoSIG.Services.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ProyectoSIG.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;


        public BaseViewModel()
        {
            DialogService = ViewModelLocator.Resolve<IDialogService>();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion   
    }
}
