using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;
using Core.Queries;

namespace Web.Controllers
{
	[Authorize]
    public class HomeController : Controller
    {
        private IStore store;

        public HomeController(IStore store)
        {
            this.store = store;
        }

        public ActionResult Index()
        {
            return View(store.Execute(new HomeQuery()));
        }

    }
}
