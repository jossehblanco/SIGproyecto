using ProyectoSIG.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSIG.Services.Navigation
{
    public interface INavigationService
    {
        BaseViewModel PreviousPageViewModel { get; }

        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;

        Task NavigateToModalAsync<TViewModel>() where TViewModel : BaseViewModel;

        void SingOut();

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();
    }
}
