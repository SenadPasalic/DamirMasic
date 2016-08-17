using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamirMasic.ViewModels
{
    public class MasterOneVM
    {
        public GetPostVM[] BlogPosts { get; set; }
        public GetPostVM[] OnePost { get; set; }
        public bool IsUserAdmin { get; set; }
    }
}
