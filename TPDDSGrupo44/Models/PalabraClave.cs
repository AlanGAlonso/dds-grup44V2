﻿namespace TPDDSGrupo44.Models
{
    public class PalabraClave
    {
        ////////////////Atributos////////////////
        public int Id { get; set; }
        public string palabraClave { get; set; }


        ////////////////////////Constructor vacio////////////////
        public PalabraClave() { }

        ////////////////////////Constructor real////////////////
        public PalabraClave(string palabra) {
            palabraClave = palabra;
        }

    }
}