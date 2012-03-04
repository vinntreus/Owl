using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core;
using Core.Users;
using Raven.Client;
using Web.Models;

namespace Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly ICommandExecutor commands;
        private readonly IStore store;

        public UsersController(ICommandExecutor commands, IStore store)
        {
            this.commands = commands;
            this.store = store;
        }

        public ViewResult Index()
        {
            return View(store.AllUsers());
        }

        //
        // Post: /User/
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AddUserMessage message)
        {
            if(!ModelState.IsValid)
            {
                return View(message);
            }
            
            commands.ExecuteCommand(new AddUserCommand(message));

            return RedirectToAction("Index");
        }

    }
}
