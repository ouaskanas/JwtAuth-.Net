using System.ComponentModel.DataAnnotations;

namespace jwt.Models
{
    public class RegisterModel
    {
     
        public string name { get; set; }     
        
 
        public string email { get; set; }

        public string password { get; set; }
      
    }
}
