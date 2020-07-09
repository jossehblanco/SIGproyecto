using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProyectoSIG.Models
{
    public class CircleColors
    {
        public Color StrokeColor { get; set; }
        public Color FillColor { get; set; }

        public CircleColors(Color StrokeColor, Color FillColor)
        {
            this.StrokeColor = StrokeColor;
            this.FillColor = FillColor;
        }
    }
}
