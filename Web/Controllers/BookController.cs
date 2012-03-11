using System;
using System.Web.Mvc;
using Core;
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

        [HttpGet]
        public ViewResult Create(int libraryId)
        {
            return View(new CreateBookViewModel { LibraryId = libraryId });
        }

        [HttpPost]
        public ViewResult Create(CreateBookViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
