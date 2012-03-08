using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core;
using Core.Users;
using Raven.Client;
using Web.Models;
using Web.Security;

namespace Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly ICommandExecutor commands;
        private readonly IStore store;
        private IAuthenticator authenticator;

        public UsersController(ICommandExecutor commands, IStore store, IAuthenticator authenticator)
        {
            this.commands = commands;
            this.store = store;
            this.authenticator = authenticator;
        }

        public ViewResult Index()
        {
            return View(store.AllUsers());
        }

		[AllowAnonymous]
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

		[AllowAnonymous]
        [HttpPost]
        public ActionResult Create(AddUserMessage message)
        {
            if(!ModelState.IsValid)
            {
                return View(message);
            }            
            
            var result = commands.Execute(new AddUserCommand(message));

            if (!result.IsSuccess())
            {
                ModelState.AddModelError("", result.CombinedErrors());
                return View(message);
            }
            else
            {
                commands.Execute(new ActivityCommand(string.Format("{0} was created", result.ReturnValue.Username)));
                authenticator.SetAuthCookie(result.ReturnValue.Username, true);
            }

            return RedirectToAction("Index");
        }

    }
}
