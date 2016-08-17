using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DamirMasic.Models
{
    public class EmailFormModel
    {
        [Required, Display(Name = "Vaše ime")]
        public string FromName { get; set; }
        [Required, Display(Name = "Vaš email"), EmailAddress]
        public string FromEmail { get; set; }
        [Required, Display(Name = "Poruka")]
        public string Message { get; set; }
    }
}
