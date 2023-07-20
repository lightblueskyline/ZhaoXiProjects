using ExeWebApi.Config;
using Interface;
using Microsoft.AspNetCore.Mvc;
using Model.DTO.User;
using Model.Other;

namespace ExeWebApi.Controllers
{
    public class UserController : MyBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ApiResult> Add(UserAdd req)
        {
            UserID = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _userService.Add(req, UserID));
        }

        [HttpPost]
        public async Task<ApiResult> Edit(UserEdit req)
        {
            UserID = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _userService.Edit(req, UserID));
        }

        [HttpGet]
        public async Task<ApiResult> Delete(string id)
        {
            return ResultHelper.Success(await _userService.Delete(id));
        }

        [HttpGet]
        public async Task<ApiResult> BatchDelete(string ids)
        {
            return ResultHelper.Success(await _userService.BatchDelete(ids));
        }

        [HttpPost]
        public async Task<ApiResult> GetUsers(UserReq req)
        {
            UserID = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _userService.GetUsers(req, UserID));
        }

        [HttpGet]
        public async Task<ApiResult> SettingRole(string userID, string roleIDs)
        {
            return ResultHelper.Success(await _userService.SettingRole(userID, roleIDs));
        }

        [HttpPost]
        public async Task<ApiResult> EditNickNameOrPassword([FromBody] PersonEdit req)
        {
            UserID = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _userService.EditNickNameOrPassword(UserID, req));
        }
    }
}
