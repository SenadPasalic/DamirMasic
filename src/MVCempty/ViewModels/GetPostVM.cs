using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamirMasic.ViewModels
{
    public class GetPostVM
    {
        public int Id { get; set; }
        public DateTime TimePosted { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Color { get; set; }

    }
}
