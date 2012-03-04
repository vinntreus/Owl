using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class SessionController : Controller
    {
		[HttpGet]
        public ActionResult Create()
		{
			return View();
		}
	}
}
