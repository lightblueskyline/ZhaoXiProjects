using System.ComponentModel.DataAnnotations;

namespace Model.DTO.Login
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string PassWord { get; set; }
    }
}
