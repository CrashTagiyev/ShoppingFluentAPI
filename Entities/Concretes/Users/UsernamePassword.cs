using Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes.Users
{
    public class UsernamePassword:BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        //Foreign key

        //Navigation Property
    }
}
