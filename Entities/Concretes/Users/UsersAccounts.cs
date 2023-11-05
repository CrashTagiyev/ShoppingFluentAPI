using Entities.Abstracts;
using Entities.Enums.UsersEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Entities.Concretes.Users
{
    public class UsersAccounts :BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public UserStatus? UserStatus { get; set; }
        public UserAccessModifiers? UserAccess { get; set; }

        public int UsernamePasswordId { get; set; }
        //Navigation Property
        public virtual UsernamePassword UsernamePasswrodIdNavigation { get; set; }

        public override string ToString()
        {
            return $"{Firstname} {Lastname} {Username} {Email}";
        }
    }
}
