using AppointmentAPI.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentAPI.App_Start
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            // Vincula la clase MqProducer a sí misma en un alcance singleton.
            // Esto significa que Ninject creará una única instancia de MqProducer 
            // y la reutilizará en todas las solicitudes que necesiten esta dependencia.
            Bind<MqProducer>().ToSelf().InSingletonScope();
        }
    }
}