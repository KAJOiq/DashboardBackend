using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Dtos.Department;
public record GetDepartmentDto(
int Id,
string DepartmentName
);
