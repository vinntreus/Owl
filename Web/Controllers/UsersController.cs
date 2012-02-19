using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Core;

namespace Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IHandleUsers users;

        public UsersController(IHandleUsers users)
        {
            this.users = users;
        }

        public ActionResult Index()
        {
            return View(users.All());
        }

        //
        // Post: /User/
        [HttpGet]
        public ActionResult Create()
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

    public class UserViewModel : IUser
    {
        public string Username { get; set; }
    }

    public class CreateUserMessage : ICreateUser
    {
        [Required(ErrorMessage = "Must enter a username")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Must enter a password")]
        public string Password { get; set; }
        
    }
}
