using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.DTO.Login
{
    public class LoginRequest
    {
        [Required]
        [DefaultValue("Admin")]
        public string UserName { get; set; }

        [Required]
        [DefaultValue("123456")]
        public string PassWord { get; set; }
    }
}
