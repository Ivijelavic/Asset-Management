using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using MVCappCoreWeb.Areas.Identity.Data;

namespace MVCappCoreWeb.Models
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<WebUser> Members { get; set; }
        public IEnumerable<WebUser> NonMembers { get; set; }
    }
}
