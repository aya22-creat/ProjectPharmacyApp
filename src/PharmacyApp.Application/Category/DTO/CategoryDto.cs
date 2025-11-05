using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Category.DTO

{
    public record CategoryDto(Guid Id, string? Name, int ProductCount,  string? Description);
}