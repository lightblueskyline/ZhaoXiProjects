using ExecWebAPI.Config;
using Interface;
using Microsoft.AspNetCore.Mvc;
using Model.DTO.Role;
using Model.Other;

namespace ExecWebAPI.Controllers
{
    public class RoleController : ABaseController
    {
        private readonly IRoleService _IRoleService;

        public RoleController(IRoleService roleService)
        {
            _IRoleService = roleService;
        }

        [HttpPost]
        public async Task<ApiResult> AddRole(RoleAdd request)
        {
            userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _IRoleService.AddRole(request, userId));
        }

        [HttpGet]
        public async Task<ApiResult> BatchDeleteRole(string ids)
        {
            return ResultHelper.Success(await _IRoleService.BatchDeleteRole(ids));
        }

        [HttpGet]
        public async Task<ApiResult> DeleteRole(string id)
        {
            return ResultHelper.Success(await _IRoleService.DeleteRole(id));
        }

        [HttpPost]
        public async Task<ApiResult> EditRole(RoleEdit request)
        {
            userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _IRoleService.EditRole(request, userId));
        }

        [HttpPost]
        public async Task<ApiResult> GetRoles(RoleRequest request)
        {
            userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _IRoleService.GetRoles(request, userId));
        }
    }
}
