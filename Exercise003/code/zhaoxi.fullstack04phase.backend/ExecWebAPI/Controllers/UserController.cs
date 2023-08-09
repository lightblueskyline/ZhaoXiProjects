using ExecWebAPI.Config;
using Interface;
using Microsoft.AspNetCore.Mvc;
using Model.DTO.Login;
using Model.DTO.User;
using Model.Other;

namespace ExecWebAPI.Controllers
{
    public class UserController : ABaseController
    {
        private readonly IUserService _IUserService;

        public UserController(IUserService userService)
        {
            _IUserService = userService;
        }

        [HttpPost]
        public async Task<ApiResult> AddUser(UserAdd request)
        {
            userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _IUserService.AddUser(request, userId));
        }

        [HttpGet]
        public async Task<ApiResult> BatchDeleteUser(string ids)
        {
            return ResultHelper.Success(await _IUserService.BatchDeleteUser(ids));
        }

        [HttpGet]
        public async Task<ApiResult> DeleteUser(string id)
        {
            return ResultHelper.Success(await _IUserService.DeleteUser(id));
        }

        [HttpPost]
        public async Task<ApiResult> EditNickNameOrPassword(PersonEdit request)
        {
            userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _IUserService.EditNickNameOrPassword(userId, request));
        }

        [HttpPost]
        public async Task<ApiResult> EditUser(UserEdit request)
        {
            userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _IUserService.EditUser(request, userId));
        }

        [HttpGet]
        public async Task<ApiResult> Get(string id)
        {
            return ResultHelper.Success(await _IUserService.Get(id));
        }

        [HttpPost]
        public async Task<ApiResult> GetUser(LoginRequest loginRequest)
        {
            return ResultHelper.Success(await _IUserService.GetUser(loginRequest));
        }

        [HttpPost]
        public async Task<ApiResult> GetUsers(UserRequest request)
        {
            return ResultHelper.Success(await _IUserService.GetUsers(request, userId));
        }

        [HttpGet]
        public async Task<ApiResult> SettingUserRole(string userId, string roleIds)
        {
            return ResultHelper.Success(await _IUserService.SettingUserRole(userId, roleIds));
        }
    }
}
