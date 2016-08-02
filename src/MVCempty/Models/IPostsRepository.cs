//using DamirMasic.ViewModels;
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

    }

    public class DbPostsRepository : IPostsRepository
    {
        DBContext _context;

        public DbPostsRepository(DBContext context)
        {
            _context = context;
        }

    }
}
