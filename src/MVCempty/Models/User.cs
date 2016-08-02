using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamirMasic.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool RegistrationComplete { get; set; }
    }
}
