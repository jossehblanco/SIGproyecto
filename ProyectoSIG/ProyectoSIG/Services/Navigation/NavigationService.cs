﻿using ProyectoSIG.Helpers;
using ProyectoSIG.Models;
using ProyectoSIG.ViewModels.Base;
using ProyectoSIG.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProyectoSIG.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        public BaseViewModel PreviousPageViewModel
        {
            get
            {
                var mainPage = Application.Current.MainPage as NavigationPage;
                var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
                return viewModel as BaseViewModel;
            }
        }

        //public Task GoBack()
        //{
        //    var mainPage = Application.Current.MainPage as NavigationPage;            
            
        //    return;
        //}

        public Task NavigateToModalAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null, true);
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null,false);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter, false);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as NavigationPage;

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as NavigationPage;

            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter, bool IsModal)
        {
            Page page = CreatePage(viewModelType, parameter);

            var navigationPage = Application.Current.MainPage as NavigationPage;
            if (navigationPage != null)
            {
                if (IsModal)
                    await navigationPage.Navigation.PushModalAsync(page,true);
                else
                    await navigationPage.PushAsync(page);   
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(page);
            }
            //}

            //await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }

        public void SignOut()
        {
            Application.Current.Properties[Constants.Userid] = null;
            Application.Current.Properties[Constants.Token] = null;
            Application.Current.Properties[Constants.Logged] = false;
            Application.Current.MainPage = new LoginView();
        }

        public void SignIn(int UserId, string Token)
        {
            Application.Current.Properties[Constants.Userid] = UserId;
            Application.Current.Properties[Constants.Token] = Token;
            Application.Current.Properties[Constants.Logged] = true;
            Application.Current.MainPage = new MasterView();
        }

        public async Task GoBack(Type tipo, bool IsModal)
        {
            if (IsModal)
                if(Application.Current.MainPage.Navigation.ModalStack.Count != 0 && Application.Current.MainPage.Navigation.ModalStack.Last().GetType() == tipo)
                    await Application.Current.MainPage.Navigation.PopModalAsync(true);
            else
                if (Application.Current.MainPage.Navigation.NavigationStack.Count != 0 && Application.Current.MainPage.Navigation.NavigationStack.Last().GetType() == tipo)
                    await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
