using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using TesteAdoNET.Data;
using TesteAdoNET.Helper;

namespace TesteAdoNET.Helper
{
    public class Sessao : ISessao
    {
        private readonly HttpContextBase _httpContext;

        public Sessao()
        {
        }

        public Sessao(HttpContextBase httpContext)
        {
            _httpContext = httpContext ?? _httpContext.GetOwinContext().Get<HttpContextBase>();
        }

        public Usuarios BuscarSessao()
        {
            string sessaoAtiva;
            if (_httpContext.Session["usuario"] == null) sessaoAtiva = "";
            else sessaoAtiva = _httpContext.Session["usuario"].ToString();

            if (String.IsNullOrEmpty(sessaoAtiva)) return new Usuarios { Id = 0 };
            else return JsonConvert.DeserializeObject<Usuarios>(sessaoAtiva);
        }

        public void CriarSessao(Usuarios usuario)
        {
            string usuarioSerialize = JsonConvert.SerializeObject(usuario);
            _httpContext.Session["usuario"] = usuarioSerialize;
        }

        public void RemoverSessao()
        {
            _httpContext.Session.Remove("usuario");
        }
    }
}