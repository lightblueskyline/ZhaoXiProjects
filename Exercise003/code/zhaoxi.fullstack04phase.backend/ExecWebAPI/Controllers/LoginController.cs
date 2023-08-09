using ExecWebAPI.Config;
using Interface;
using Microsoft.AspNetCore.Mvc;
using Model.DTO.Login;
using Model.DTO.User;
using Model.Other;

namespace ExecWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _ILogger;
        private readonly IUserService _IUserService;
        private readonly ICustomJWTService _ICustomJWTService;

        public LoginController(ILogger<LoginController> logger, IUserService userService, ICustomJWTService customJWTService)
        {
            _ILogger = logger;
            _IUserService = userService;
            _ICustomJWTService = customJWTService;
        }

        /// <summary>
        /// 取得 Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> GetToken([FromBody] LoginRequest request)
        {
            if (ModelState.IsValid) // 模型验证
            {
                UserResponse userResponse = await _IUserService.GetUser(request);
                if (userResponse == null)
                {
                    return ResultHelper.Error("账号不存在，用户名或密码错误！");
                }
                _ILogger.LogInformation("登录成功");
                return ResultHelper.Success(await _ICustomJWTService.GetToken(userResponse));
            }
            else
            {
                return ResultHelper.Error("参数错误！");
            }
        }

        /// <summary>
        /// 刷新 Token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> RefreshToken(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                return ResultHelper.Error("参数不可以为空！");
            }
            return ResultHelper.Success(await _ICustomJWTService.GetToken(await _IUserService.Get(userId)));
        }
    }
}
