using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteAdoNET.Data;
using TesteAdoNET.Helper;

namespace TesteAdoNET.App_Start
{
    public class MyDependecyResolver : IDependencyResolver
    {
        public object GetService(Type type)
        {
            if (type == typeof(MentalAidEntities))
            {
                return new MentalAidEntities();
            }
            return null;
        }

        public object GetServiceSessao(Type type)
        {
            if (type == typeof(Sessao))
            {
                return new Sessao();
            }
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Enumerable.Empty<object>();
        }
    }
}