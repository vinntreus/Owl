using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Core;
using Core.Sessions;
using Web.Models;
using Web.Security;

namespace Web.Controllers
{
	[AllowAnonymous]
    public class SessionController : Controller
    {
		private ICommandExecutor commandExecutor;
		private IAuthenticator authenticator;

		public SessionController(ICommandExecutor commandExecutor, IAuthenticator authenticator)
		{
			this.commandExecutor = commandExecutor;
			this.authenticator = authenticator;
		}

		[HttpGet]
        public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(SessionViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var isAuthenticated = commandExecutor.Execute(new CreateSessionCommand(model));
			if (isAuthenticated)
			{
				authenticator.SetAuthCookie(model.Username, model.PersistCookie);
				return RedirectToAction("Index", "Home");
			}

			ModelState.AddModelError("", "Wrong username or password");
			return View(model);
		}

        public RedirectToRouteResult Destroy()
        {
            authenticator.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
