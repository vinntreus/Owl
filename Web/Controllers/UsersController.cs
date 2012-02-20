using System.Web.Mvc;
using Core;
using Web.Models;

namespace Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IHandleUsers users;

        public UsersController(IHandleUsers users)
        {
            this.users = users;
        }

        public ViewResult Index()
        {
            return View(users.All());
        }

        //
        // Post: /User/
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateUserMessage message)
        {
            if(!ModelState.IsValid)
            {
                return View(message);
            }
            
            users.Add(message);

            return RedirectToAction("Index");
        }

    }
}
