using System.Web.Mvc;
using Core;
using Core.Libraries;
using Core.Queries;
using Web.Models;

namespace Web.Controllers
{
    public class LibraryController : Controller
    {
        private ICommandExecutor commandExecutor;
        private IStore store;

        public LibraryController(ICommandExecutor commandExecutor, IStore store)
        {
            this.commandExecutor = commandExecutor;
            this.store = store;
        }

        public ActionResult Index(int id)
        {
            return View(store.Execute(new LibraryQuery(id)));
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
