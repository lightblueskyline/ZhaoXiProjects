using System.ComponentModel.DataAnnotations;

namespace Model.DTO.Login
{
    public class LoginReq
    {
        [Required(ErrorMessage = "请输入用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        public string PassWord { get; set; }
    }
}
