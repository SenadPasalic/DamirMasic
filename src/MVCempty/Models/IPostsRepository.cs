//using DamirMasic.ViewModels;
using DamirMasic.ViewModels;
using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


namespace DamirMasic.Models
{
    public interface IPostsRepository
    {
        void AddPost(AddPostVM viewModel, string postedBy);
    }

    public class DbPostsRepository : IPostsRepository
    {
        DBContext _context;

        public DbPostsRepository(DBContext context)
        {
            _context = context;
        }



        //Skriv till db
        public void AddPost(AddPostVM viewModel, string postedBy)
        {            
            _context.Posts.Add(new Post
            {
                TimePosted = DateTime.Now.AddHours(2),
                Image = viewModel.Image
            });
            _context.SaveChanges();
        }
    }
}
