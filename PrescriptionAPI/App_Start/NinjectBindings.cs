using PrescriptionAPI.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrescriptionAPI.App_Start
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            // Vincula la clase MqConsumer a sí misma en un alcance singleton.
            // Esto significa que Ninject creará una única instancia de MqConsumer 
            // y la reutilizará en todas las solicitudes que necesiten esta dependencia.
            Bind<MqConsumer>().ToSelf().InSingletonScope();
        }
    }
}