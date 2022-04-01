using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;


namespace MVCappCoreWeb.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the WebUser class
    public class WebUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Oib { get; set; }
        //public string Name { get; internal set; }
        /**********************************************************/
        //public bool IsAdmin { get; set; }
        //public string DataEventRecordsRole { get; set; }
        //public string SecuredFilesRole { get; set; }
    }
}
