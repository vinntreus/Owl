using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Core;
using Core.Security;
using Web.Models;

namespace Web.Controllers
{
    public class SessionController : Controller
    {
		private ICommandExecutor commandExecutor;

		public SessionController(ICommandExecutor commandExecutor)
		{
			this.commandExecutor = commandExecutor;
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
				FormsAuthentication.SetAuthCookie(model.Username, true);
				return RedirectToAction("Index", "Home");
			}

			ModelState.AddModelError("", "Wrong username or password");
			return View(model);
		}
	}
}
