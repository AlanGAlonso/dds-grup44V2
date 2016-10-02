using System.Collections.Generic;



namespace TPDDSGrupo44.Models
{
    public class JsonBank
    {
        public string banco { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public string sucursal { get; set; }
        public string gerente { get; set; }
        public List<string> servicios { get; set; }
        
    }
}

// DbGeography unaCoordenada = x,y