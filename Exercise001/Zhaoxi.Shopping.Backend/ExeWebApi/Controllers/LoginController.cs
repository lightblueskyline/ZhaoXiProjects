using ExeWebApi.Config;
using Interface;
using Microsoft.AspNetCore.Mvc;
using Model.DTO.Login;
using Model.DTO.User;
using Model.Other;

namespace ExeWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUserService _userService;
        private readonly ICustomJWTService _customJWTService;

        public LoginController(ILogger<LoginController> logger, IUserService userService, ICustomJWTService customJWTService)
        {
            _logger = logger;
            _userService = userService;
            _customJWTService = customJWTService;
        }

        [HttpPost]
        public async Task<ApiResult> GetToken([FromBody] LoginReq req)
        {
            if (ModelState.IsValid) // 模型验证
            {
                UserRes user = await _userService.GetUser(req);
                if (user == null)
                {
                    return ResultHelper.Error("登录失败！账号不存在，请检查用户名和密码！");
                }
                _logger.LogInformation("登录成功！");
                return ResultHelper.Success(await _customJWTService.GetToken(user));
            }
            else
            {
                return ResultHelper.Error("输入参数错误！");
            }
        }
    }
}
