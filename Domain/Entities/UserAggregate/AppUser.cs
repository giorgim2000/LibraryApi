using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserAggregate
{
    public class AppUser : IdentityUser<int>
    {
        //public ICollection<AppUserRole> AppUserRoles { get; set; }
    }
}
