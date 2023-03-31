using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using DALL.Data;


namespace Api_Midasoft
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            //Configura el dbcontext para usarlo como unica instancia por request
            app.CreatePerOwinContext(MidasoftContext.Create);
        }
    }
}
