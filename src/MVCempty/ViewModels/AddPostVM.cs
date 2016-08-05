using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DamirMasic.ViewModels
{
    public class AddPostVM
    {
        public int Id { get; set; }

        [Display(Name = "Picture link")]
        public string Image { get; set; }
    }
}
