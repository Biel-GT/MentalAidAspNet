using Microsoft.Owin;
using Owin;
using System.Web.Services.Description;
using TesteAdoNET.Data;

[assembly: OwinStartupAttribute(typeof(TesteAdoNET.Startup))]
namespace TesteAdoNET
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
