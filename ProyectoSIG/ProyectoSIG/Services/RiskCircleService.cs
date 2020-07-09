﻿using ProyectoSIG.Client;
using ProyectoSIG.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using System.Linq;
using Xamarin.Forms.Internals;

namespace ProyectoSIG.Services
{
    public static class RiskCircleService
    {

        public static async Task<List<MapElement>> GetRiskCircles(string url)
        {
            Dictionary<string, CircleColors> RiskColors = new Dictionary<string, CircleColors>();
            RiskColors.Add("bajo", new CircleColors(Color.DarkGreen, Color.FromHex("#885C8001")));
            RiskColors.Add("moderado", new CircleColors(Color.Yellow, Color.FromHex("#88FFFD82")));
            RiskColors.Add("alto", new CircleColors(Color.Orange, Color.FromHex("#88fA9F42")));
            RiskColors.Add("extremadamente alto", new CircleColors(Color.FromHex("#88FF0000"), Color.FromHex("#88FFC0CB")));

            List<MapElement> mapElements = new List<MapElement>();
            List<RiskCircle> circles = await RestClient.Get<RiskCircle>(url);
            foreach(RiskCircle circulitao in circles)
            {
                CircleColors circleColors = RiskColors.First(col => col.Key == circulitao.risk_level).Value as CircleColors;
                Circle circle = new Circle();
                circle.Center = new Position(circulitao.latitude, circulitao.longitude);
                circle.Radius = new Distance(circulitao.radius);
                circle.StrokeWidth = 8;
                circle.FillColor = circleColors.FillColor;
                circle.StrokeColor = circleColors.StrokeColor;                
                mapElements.Add(circle);
            }

            return mapElements;
        }

    }
}
