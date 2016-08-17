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

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title required.")]
        public string Title { get; set; }

        [Display(Name = "Content")]
        [Required(ErrorMessage = "Did you forget the content?")]
        [DataType(DataType.MultilineText)]
        [UIHint("tinymce_jquery_full")] //AllowHtml
        public string Text { get; set; }

        [Display(Name = "Choose color")]
        public string Color { get; set; }
    }
}
