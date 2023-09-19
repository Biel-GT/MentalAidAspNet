using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TesteAdoNET.Data;
using TesteAdoNET.Helper;
using TesteAdoNET.Models;

namespace TesteAdoNET.Controllers
{
    public class HomeController : Controller
    {
        //private readonly MentalAidEntities _context;
        //private readonly Sessao _sessao;

        //public HomeController(MentalAidEntities context, Sessao sessao)
        //{
        //    _context = context ?? HttpContext.GetOwinContext().Get<MentalAidEntities>();
        //    _sessao = sessao ?? HttpContext.GetOwinContext().Get<Sessao>();
        //}

        public HomeController() {
            //sessao = RecuperarSessao();
        }

        public Sessao RecuperarSessao()
        {
            return new Sessao(HttpContext);
        }
        private Sessao sessao;

        public ActionResult Index()
        {
            if (sessao != null)
            {
                sessao = RecuperarSessao(); 
                if (sessao.BuscarSessao() != null) return RedirectToAction("Dashboard");
            }

            return View();
        }

        
        public ActionResult Dashboard(Usuarios usuario)
        {
            if (usuario.Id != 0)
            {
                sessao = RecuperarSessao();
                return View("Dashboard", usuario);
            }
            else return RedirectToAction("Login", "Account", new { mensagem = "sessaoInv" });
        }

        public ActionResult Testes()
        {
            sessao = RecuperarSessao(); 
            if (sessao.BuscarSessao() == null) return RedirectToAction("Login", "Account", new { mensagem = "sessaoInv" });

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel form)
        {
            if (ModelState.IsValid)
            {
                sessao = RecuperarSessao();
                Usuarios novoUsuario = new Usuarios { Email = form.Email, Senha = form.Password };

                using (MentalAidEntities ctx = new MentalAidEntities()) 
                {
                    ctx.Usuarios.AddObject(novoUsuario);
                    ctx.SaveChanges();

                    sessao.CriarSessao(novoUsuario);
                }
                return RedirectToAction("Dashboard", novoUsuario);
            }

            return RedirectToAction("Register", "Account");
        }

        
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login", "Account", model);
            }
            else
            {
                using(MentalAidEntities ctx = new MentalAidEntities())
                {
                    var qr = ctx.Usuarios.AsQueryable().Where(u => u.Email == model.Email);

                    if (qr.Count() > 0)
                    {
                        var usuario = qr.ToList();

                        if (usuario[0].Senha == model.Password)
                        {
                            sessao = RecuperarSessao();
                            sessao.CriarSessao(usuario[0]);
                            return View("Dashboard", usuario[0]);
                        }
                        else
                        {
                            return RedirectToAction("Login", "Account", new { mensagem = "senhaInv" });
                        }
                    }
                    else
                    {
                        return RedirectToAction("Login", "Account", new { mensagem = "contaInex"});
                    }
                }
            }
        }

        //Logout
        public ActionResult Logout()
        {
            sessao = RecuperarSessao();  
            sessao.RemoverSessao();
            return RedirectToAction("Index");
        }


        public ActionResult Game(Usuarios usuario)
        {
            if (usuario.Id != 0)
            {
                sessao = RecuperarSessao();
                return View("Game", usuario);
            }
            else return RedirectToAction("Login", "Account", new { mensagem = "sessaoInv" });
        }
    }
}