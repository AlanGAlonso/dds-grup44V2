using System;

namespace TPDDSGrupo44.Models
{
    public class POI
    {
        public int id { get; set; }
        public DateTime fechaBaja { get; set; }


        /* Getters */
        public int getId()
        {
            return this.id;
        }
    }
}