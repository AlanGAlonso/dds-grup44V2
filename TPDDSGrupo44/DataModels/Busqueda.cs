﻿using System;
using System.ComponentModel.DataAnnotations;
using TPDDSGrupo44.ViewModels;

namespace TPDDSGrupo44.DataModels
{
    public class Busqueda
    {
        [Key]
        ////////////////Atributos////////////////
        public int Id { get; set; }
        public string textoBuscado { get; set; }
        public int cantidadDeResultados { get; set; }
        public DateTime fecha { get; set; }
        public string usuario { get; set; }
        public virtual DispositivoTactil terminal { get; set; }
        public TimeSpan duracionDeBusqueda { get; set; }

        ////////////////Constructor vacio////////////////
        public Busqueda () { }




        public Busqueda(string texto, int resultados, DateTime fechaBusqueda, DispositivoTactil terminalBusqueda, TimeSpan duracion)
        {
            textoBuscado = texto;
            cantidadDeResultados = resultados;
            fecha = fechaBusqueda;
            terminal = terminalBusqueda;
            duracionDeBusqueda = duracion;
            if (BaseViewModel.usuario != null )
            {
                usuario = BaseViewModel.usuario.nombre;
            }
        }
    }
}