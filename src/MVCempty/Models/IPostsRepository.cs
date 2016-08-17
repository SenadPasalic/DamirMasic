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
        GetPostVM[] GetAllPosts();
        void DeleteBlogPost(int postId);
        void UpdateBlogPost(GetPostVM model);
        GetPostVM[] GetOnePost(int myTitle);
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
                Image = viewModel.Image,
                Text = viewModel.Text,
                Title = viewModel.Title,
                Color = viewModel.Color

            });
            _context.SaveChanges();
        }

        //Hämta alla poster från db
        public GetPostVM[] GetAllPosts()
        {
            return _context.Posts
                .OrderByDescending(o => o.TimePosted)
                .Select(o => new GetPostVM
                {
                    Id = o.Id,
                    TimePosted = o.TimePosted,
                    Image = o.Image,
                    Text = o.Text,
                    Title = o.Title,
                    Color = o.Color
                })
                .ToArray();
        }

        //Edit BlogPost
        public void UpdateBlogPost(GetPostVM model)
        {
            var post = _context.Posts.Single(o => o.Id == model.Id);
            post.Title = model.Title;
            post.Text = model.Text;
            post.Image = model.Image;
            post.Color = model.Color;
            _context.SaveChanges();
        }
        //Delete BlogPost
        public void DeleteBlogPost(int postId)
        {
            var removeThisPost = _context.Posts.Single(o => o.Id == postId);
            _context.Posts.Remove(removeThisPost);
            _context.SaveChanges();
        }

        //Hämta en post från db
        public GetPostVM[] GetOnePost(int myTitle)
        {
            return _context.Posts
                .OrderByDescending(o => o.TimePosted)
                .Where(o => o.Id == myTitle)
                .Select(o => new GetPostVM
                {
                    Id = o.Id,
                    Title = o.Title,
                    Text = o.Text,
                    Image = o.Image,
                    Color = o.Color,
                    TimePosted = o.TimePosted
                })
                .ToArray();
        }
    }
}
