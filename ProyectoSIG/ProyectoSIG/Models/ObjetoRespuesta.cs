using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSIG.Models
{
    public class ObjetoRespuesta<T>
    {
        public string Mensaje { get; set; }
        public bool Succesful { get; set; }
        public List<T> ObjetosRecuperados { get; set; }
        public Type ListType { 
            get
            {
                return ObjetosRecuperados?.GetType().GetGenericArguments()[0];
            } 
        }

    }
}
