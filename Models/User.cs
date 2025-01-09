using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Ticket.Models
{
    public class User : IdentityUser
    {
        public required string SecondPhone {set; get;} = string.Empty;
        public required string Position {set; get;} = string.Empty;
        public List<UserDepartment> UserDepartments {set; get;} = [];
    }
}