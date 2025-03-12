using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jwt.Models
{
    public class User : IdentityUser
    {   
        public string name { get; set; }

    }   
}
