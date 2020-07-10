using ProyectoSIG.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ProyectoSIG.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MapView : ContentPage
    {
        public MapView()
        {
            InitializeComponent();

            mapa.MoveToRegion(new MapSpan(new Position(13.681170, -89.285328), 0.01,0.01));

            Task.Run(async () => {
                bool logout = await (BindingContext as MapViewModel).SetMapCircles(mapa.MapElements);
                if (logout)
                    return;
                Position pos = await (BindingContext as MapViewModel).GetUserLocation();
                if(pos.Latitude != 0 && pos.Longitude != 0)
                {
                    Pin pin = new Pin();
                    pin.Position = pos;
                    pin.Label = "Tu estas aquí";
                    mapa.Pins.Add(pin);
                    Device.BeginInvokeOnMainThread(() => {
                        mapa.MoveToRegion(new MapSpan(pos, 0.05,0.05));
                    });
                }
            });

        }
    }
}
