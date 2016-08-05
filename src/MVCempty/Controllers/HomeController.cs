using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using DamirMasic.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DamirMasic.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCempty.Controllers
{
    public class HomeController : Controller
    {
        UserManager<IdentityUser> userManager; //klassvariabel
        SignInManager<IdentityUser> signInManager; //klassvariabel
        IdentityDbContext context; //klassvariabel      
        IPostsRepository repository;
        DBContext dbContext;

        //Konstruktor
        public HomeController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IdentityDbContext context,
            IPostsRepository repository,
            DBContext dbContext)
        {
            this.userManager = userManager; //sätter klassvariabeln
            this.signInManager = signInManager; //sätter klassvariabeln
            this.context = context; //sätter klassvariabeln 
            this.repository = repository;
            this.dbContext = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            //Logga in
            await signInManager.PasswordSignInAsync(
                viewModel.UserName, viewModel.Password, false, false);


            var user = User.Identity.Name; //namnet på den inloggade

            return RedirectToAction(nameof(HomeController.Index)); //då du blivit inloggad, hoppa till ... //.Index


        }

        //Gallery
        public IActionResult Gallery()
        {
            return View();
        }
    }
}
