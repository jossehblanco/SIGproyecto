using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSIG.Models
{
    public class RiskCircle
    {
        public int id { get; set; }
        public int density { get; set; }
        public float lat { get; set; }
        public float _long { get; set; }
        public float radius { get; set; }
        public string risk_level { get; set; }
    }
}
