using System;
using System.Web.Mvc;
using Core;
using Core.Books;
using Core.Queries;
using Web.Models;

namespace Web.Controllers
{
    public class BookController : Controller
    {
        private ICommandExecutor commandExecutor;
        private IStore store;        

        public BookController(ICommandExecutor commandExecutor, IStore store)
        {
            this.commandExecutor = commandExecutor;
            this.store = store;
        }
        
        public ViewResult Index(int id, int fromLibrary)
        {
            return View(store.Execute(new BookQuery(id, fromLibrary)));
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
                return RedirectToAction("Index", new { id = result.ReturnValue.Book.Id, fromLibrary = result.ReturnValue.Library.Id });
            }

            ModelState.AddModelError("", result.CombinedErrors());

            return View(model);

        }        
    }
}
