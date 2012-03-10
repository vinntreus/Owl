using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Core;
using Core.Activities;
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

			var result = commandExecutor.Execute(new CreateSessionCommand(model));
			if (result.IsSuccess())
			{
				authenticator.SetAuthCookie(model.Username, model.PersistCookie);                
				return RedirectToAction("Index", "Home");
			}

			ModelState.AddModelError("", result.CombinedErrors());
			return View(model);
		}

        public RedirectToRouteResult Destroy()
        {
            authenticator.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
