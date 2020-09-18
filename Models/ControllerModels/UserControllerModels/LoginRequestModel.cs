using System.ComponentModel.DataAnnotations;

namespace dyt_ecommerce.Models.ControllerModels.UserControllerModels
{
    public class LoginRequestModel
    {
        [Required, MinLength(4), MaxLength(128)]
        public string PIDorEmail { get; set; }
        
        [Required, MinLength(4), MaxLength(128)]
        public string Password { get; set; }
    }
}