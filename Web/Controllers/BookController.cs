using System;
using System.Web.Mvc;
using Core;
using Core.Books;
using Web.Models;

namespace Web.Controllers
{
    public class BookController : Controller
    {
        private ICommandExecutor commandExecutor;

        public BookController(ICommandExecutor commandExecutor)
        {
            this.commandExecutor = commandExecutor;
        }
        
        public ViewResult Index(int id)
        {
            return View();
        }


        [HttpGet]
        public ViewResult Create(int libraryId)
        {
            return View(new CreateBookViewModel { LibraryId = libraryId });
        }

        [HttpPost]
        public ActionResult Create(CreateBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = commandExecutor.Execute(new CreateBookCommand(model));

            if (result.IsSuccess())
            {
                return RedirectToAction("Index", new { id = result.ReturnValue.Id });
            }

            ModelState.AddModelError("", result.CombinedErrors());

            return View(model);

        }        
    }
}
