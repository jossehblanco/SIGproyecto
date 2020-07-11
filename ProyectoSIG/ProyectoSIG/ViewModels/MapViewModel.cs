using ProyectoSIG.Models;
using ProyectoSIG.PopUps;
using ProyectoSIG.Services;
using ProyectoSIG.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private bool _active = true;
        public bool Active
        {
            get { return _active; }
            set
            {
                if(_active != value)
                {
                    _active = value;
                    ((Command)_localizar).ChangeCanExecute();
                    OnPropertyChanged();
                }
            }
        }

        ICommand _localizar;
        public ICommand Localizar
        {
            get { return _localizar; }
        }

        public Xamarin.Forms.Maps.Map Map { get; set; }

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
                if (PopupNavigation.Instance.PopupStack.Count == 0 || PopupNavigation.Instance.PopupStack.Last().GetType() != typeof(InformationView))
                    await PopupNavigation.Instance.PushAsync(new InformationView(),true);
            });

            _localizar = new Command(async () =>
            {
                Active = false;

                await GetUserLocation();

                Active = true;
            }, () => { return Active; });
        }

        public async Task<bool> SetMapCircles(IList<MapElement> mapElements)
        {
            ObjetoRespuesta<MapElement> objetoRespuesta = await RiskCircleService.GetRiskCircles("circles");
            if(!objetoRespuesta.Succesful)
            {
                //Device.BeginInvokeOnMainThread(() => {
                //    DialogService.ShowError(objetoRespuesta.Mensaje, "Error", "Ok");
                //});
                Device.BeginInvokeOnMainThread(() =>
                {
                    NavigationService.SignOut();
                });
                //await DialogService.ShowError(objetoRespuesta.Mensaje, "Error", "Ok", null);
                return true;
            }

            MapElements = objetoRespuesta.ObjetosRecuperados;
            foreach(MapElement mapita in MapElements)
            {
                mapElements.Add(mapita);
            }
            return false;
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

                if (position.Latitude != 0 && position.Longitude != 0)
                {
                    Pin pin = new Pin();
                    pin.Position = position;
                    pin.Label = "Tu estas aquí";
                    Map.Pins.Add(pin);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Map.MoveToRegion(new MapSpan(position, 0.05, 0.05));
                    });
                }
            }
            catch(Exception ex)
            {
                Device.BeginInvokeOnMainThread(() => {
                    DialogService.ShowError("Por favor brindar permisos de GPS o encender el GPS.", "Error", "Ok");
                });
            }
            return position;
        }
    }
}
