using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using DamirMasic.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Authorization;
using DamirMasic.ViewModels;
using System.Security.Claims;
using MVCempty.Controllers;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DamirMasic.Controllers
{
    public class AdminController : Controller
    {
        IPostsRepository _postsRepository;
        UserManager<IdentityUser> userManager; //klassvariabel
        SignInManager<IdentityUser> signInManager; //klassvaria
        DBContext context;
        RoleManager<IdentityRole> roleManager;

        public AdminController(IPostsRepository postsRepository,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            DBContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _postsRepository = postsRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            this.roleManager = roleManager;
        }

        // GET: /<controller>/
        //Admin/AddPost
        [Authorize]
        public IActionResult AddPost()
        {
            var model = new AddPostVM();

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddPost(AddPostVM viewModel, string command)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            _postsRepository.AddPost(viewModel, "Administrator");
            return RedirectToAction("gallery", "home");
        }

        //Register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            //Skapa DB-schemat
            await context.Database.EnsureCreatedAsync();

            
            
            //Skapa användaren
            var identityUser = new IdentityUser { Email = viewModel.Email, UserName = viewModel.UserName };
            var result = await userManager.CreateAsync(identityUser, viewModel.Password);
            // Visa ev. fel-meddelande
            if (!result.Succeeded)
            {
                ModelState.AddModelError(nameof(LoginVM.UserName),
                    result.Errors.FirstOrDefault().Description);

                return View(viewModel);
            }


            await signInManager.PasswordSignInAsync(
                viewModel.UserName, viewModel.Password, false, false);

            //Skapa Admin roll
            //var roleResult = await roleManager.CreateAsync(new IdentityRole("User"));
            //if (roleResult.Succeeded)
            //{
            var user = await userManager.FindByNameAsync(viewModel.UserName);
            //var userResult = await userManager.AddToRoleAsync(user, "User");
            await userManager.AddClaimAsync(user, claim: new Claim(ClaimTypes.Role.ToString(), "Admin"));
            //}

            //Logga in

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(HomeController.Index), "home"); //då du blivit inloggad, hoppa till Home/Index
        }

        //Delete BlogPost
        [HttpPost]
        public IActionResult DeleteBlogPost(int postId)
        {
            _postsRepository.DeleteBlogPost(postId);
            return RedirectToAction(nameof(HomeController.Gallery), "home");
        }

        //Edit BlogPost
        [HttpPost]
        public IActionResult EditBlogPost(int postId)
        {
            var model = _postsRepository.GetAllPosts().Where(o => o.Id == postId)
                .Select(o => new AddPostVM
                {
                    Id = postId,
                    Title = o.Title,
                    Text = o.Text,
                    Image = o.Image,
                    Color = o.Color
                })
                .Single();

            return View(model);
        }

        //Update BlogPost
        [HttpPost]
        public IActionResult UpdateBlogPost(GetPostVM model)
        {
            _postsRepository.UpdateBlogPost(model);            
            return RedirectToAction(nameof(HomeController.Gallery), "home");
        }

        //Logout
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "home");
        }
    }
}
