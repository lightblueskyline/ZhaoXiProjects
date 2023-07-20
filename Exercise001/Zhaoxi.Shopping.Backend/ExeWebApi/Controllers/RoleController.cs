using ExeWebApi.Config;
using Interface;
using Microsoft.AspNetCore.Mvc;
using Model.DTO.Role;
using Model.Other;

namespace ExeWebApi.Controllers
{
    public class RoleController : MyBaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<ApiResult> Add(RoleAdd req)
        {
            UserID = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _roleService.Add(req, UserID));
        }

        [HttpPost]
        public async Task<ApiResult> Edit(RoleEdit req)
        {
            UserID = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _roleService.Edit(req, UserID));
        }

        [HttpGet]
        public async Task<ApiResult> Delete(string id)
        {
            return ResultHelper.Success(await _roleService.Delete(id));
        }

        [HttpGet]
        public async Task<ApiResult> BatchDelete(string ids)
        {
            return ResultHelper.Success(await _roleService.BatchDelete(ids));
        }

        [HttpPost]
        public async Task<ApiResult> GetRoles(RoleReq req)
        {
            UserID = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _roleService.GetRoles(req, UserID));
        }
    }
}
