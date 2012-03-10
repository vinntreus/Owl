using System.Web.Mvc;
using Core;
using Core.Libraries;
using Web.Models;

namespace Web.Controllers
{
    public class LibraryController : Controller
    {
        private ICommandExecutor commandExecutor;

        public LibraryController(ICommandExecutor commandExecutor)
        {
            this.commandExecutor = commandExecutor;
        }

        public ActionResult Index(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateLibraryViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            var result = commandExecutor.Execute(new CreateLibraryCommand(model));

            if (result.IsSuccess())
            {
                return RedirectToAction("Index", new { id = result.ReturnValue.Id });
            }
            
            ModelState.AddModelError("", result.CombinedErrors());

            return View(model);
        }

    }
}
