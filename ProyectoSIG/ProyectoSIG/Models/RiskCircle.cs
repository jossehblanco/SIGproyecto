using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSIG.Models
{
    public class RiskCircle
    {
        public int id { get; set; }
        public int density { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public float radius { get; set; }
        public string risk_level { get; set; }
    }
}
