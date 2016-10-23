using System.Collections.Generic;
using AutoMapper;
using TPDDSGrupo44.Models;
using System;

namespace TPDDSGrupo44.DataModels
{
    public class JsonCGP
    {
        public int comuna { get; set; }
        public string zonas { get; set; }
        public string director { get; set; }
        public string domicilio { get; set; }
        public string telefono { get; set; }

        public List<ServiciosJSON> servicios { get; set; }


        //public void mapear() {
        //    var config = new MapperConfiguration(cfg => {
        //        cfg.CreateMap<JsonCGP, CGP>();
        //    });

        //    IMapper mapper = config.CreateMapper();
        //    var jsoncgp = new JsonCGP();
        //    var dest = mapper.Map<JsonCGP, CGP>(jsoncgp);
        //}

        public CGP mapear()
        {
            GetJsonCGP getJson = new GetJsonCGP();

            CGP cgp = new CGP();

            cgp.nombreDePOI = "Sede Comunal" + comuna;

            foreach (ServiciosJSON s in servicios)
            {
                ServicioCGP servicio = new ServicioCGP();

                servicio.nombre = s.nombre;

                foreach (HorariosJSON h in s.horarios)
                {
                    HorarioAbierto horario = new HorarioAbierto();
                    horario.dia = (DayOfWeek)System.Enum.ToObject(typeof(DayOfWeek), (h.diaSemana));
                    horario.horarioInicio = h.horaDesde;
                    horario.horarioFin = h.horaHasta;

                    servicio.horarioAbierto.Add(horario);
                }

                cgp.servicios.Add(servicio);
            }
            return cgp;
        }
    }
}
