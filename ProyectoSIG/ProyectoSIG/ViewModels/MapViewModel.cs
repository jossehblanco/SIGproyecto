using ProyectoSIG.Models;
using ProyectoSIG.PopUps;
using ProyectoSIG.Services;
using ProyectoSIG.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ProyectoSIG.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        private List<MapElement> mapElements;
        public List<MapElement> MapElements
        {
            get { return mapElements ?? new List<MapElement>(); }
            set 
            { 
                if(mapElements != value)
                {
                    mapElements = value;
                    OnPropertyChanged();
                }
            }
        }

        ICommand _showInfo;
        public ICommand ShowInfo
        {
            get { return _showInfo; }
        }

        public MapViewModel()
        {
            _showInfo = new Command(async() => {
                await PopupNavigation.Instance.PushAsync(new InformationView(),true);
            });
        }

        public async Task SetMapCircles(IList<MapElement> mapElements)
        {
            ObjetoRespuesta<MapElement> objetoRespuesta = await RiskCircleService.GetRiskCircles("circles");
            if(!objetoRespuesta.Succesful)
            {
                Device.BeginInvokeOnMainThread(() => {
                    DialogService.ShowError(objetoRespuesta.Mensaje, "Error", "Ok");
                });
                //await DialogService.ShowError(objetoRespuesta.Mensaje, "Error", "Ok", null);
                return;
            }

            MapElements = objetoRespuesta.ObjetosRecuperados;
            foreach(MapElement mapita in MapElements)
            {
                mapElements.Add(mapita);
            }
        }

        public async Task<Position> GetUserLocation()
        {
            Position position = new Position(0,0);
            try
            {
                var location = await Geolocation.GetLocationAsync();

                if (location == null)
                    location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    position = new Position(location.Latitude, location.Longitude);
                }
            }
            catch(Exception ex)
            {
                Device.BeginInvokeOnMainThread(() => {
                    DialogService.ShowError(ex.Message, "Error", "Ok");
                });
            }
            return position;
        }
    }
}
