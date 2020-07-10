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
            MapElements = await RiskCircleService.GetRiskCircles("circles");
            foreach(MapElement mapita in MapElements)
            {
                mapElements.Add(mapita);
            }
        }
    }
}
