using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(AutenticacaoAspNet.Startup))]

namespace AutenticacaoAspNet
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Autenticacao/Login")
            });

            //o antififogery token por default espera um identifier
            //o antifogery token  é um sistema pra identificar o usuario e evitar que sites de terceiros enviem 
            //esse formulario e ele precisa de um identificador unico pra identificar que é aquele usuario q esta 
            //enviando o form, entao aqui estamos passando oq esta no claim como identificador unico que foi definido no
            //Controller Autenticacao na action Login
            AntiForgeryConfig.UniqueClaimTypeIdentifier = "Login";
        }
    }
}
