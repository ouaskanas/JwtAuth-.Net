using System.ComponentModel.DataAnnotations;

namespace jwt.Models
{
    public class LoginModel
    {
        
        public string email { get; set; }
        
        public string password { get; set; }
    }
}
