using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using DamirMasic.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DamirMasic.Models;
using System.Net.Mail;
using System.Net;

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

            return RedirectToAction("LoginSuccess"); //då du blivit inloggad, hoppa till ... //.Index
        }

        //Gallery
        public IActionResult Gallery()
        {
            MasterOneVM model = new MasterOneVM();

            model.BlogPosts = repository.GetAllPosts();

            return View(model);
        }

        //Contact with mail
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("damir_sicma@hotmail.com"));  // replace with valid value 
                message.From = new MailAddress(model.FromEmail);  // replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    smtp.UseDefaultCredentials = false;

                    var credential = new NetworkCredential
                    {
                        UserName = "keepgeeking@gmail.com",  // replace with valid value
                        Password = "dinmAmma!!11!!"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com"; //"smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;

                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

        //Sent contact mail
        public ActionResult Sent()
        {
            return View();
        }
        //Login success
        public ActionResult LoginSuccess()
        {
            return View();
        }

        //OnePost
        public IActionResult OnePost(int myTitle, string search)
        {
            MasterOneVM model = new MasterOneVM();

            model.OnePost = repository.GetOnePost(myTitle);
            model.BlogPosts = repository.GetAllPosts();

            return View(model);
        }
    }
}
