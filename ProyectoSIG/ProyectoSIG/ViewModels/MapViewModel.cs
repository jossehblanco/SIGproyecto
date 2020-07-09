using ProyectoSIG.Services;
using ProyectoSIG.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
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

        public MapViewModel()
        {
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
