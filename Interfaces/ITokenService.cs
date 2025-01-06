using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Models;

namespace Ticket.Interfaces;
public interface ITokenService
{
    string CreateToken(User user);
    

}
