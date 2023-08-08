using ExecWebAPI.Config;
using Interface;
using Microsoft.AspNetCore.Mvc;
using Model.DTO.Menu;
using Model.Other;

namespace ExecWebAPI.Controllers
{
    public class MenuController : ABaseController
    {
        private readonly IMenuService _IMenuService;

        public MenuController(IMenuService menuService)
        {
            _IMenuService = menuService;
        }

        [HttpPost]
        public async Task<ApiResult> AddMenu(MenuAdd request)
        {
            userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _IMenuService.AddMenu(request, userId));
        }

        [HttpGet]
        public async Task<ApiResult> BatchDeleteMenu(string ids)
        {
            return ResultHelper.Success(await _IMenuService.BatchDeleteMenu(ids));
        }

        [HttpGet]
        public async Task<ApiResult> DeleteMenu(string id)
        {
            return ResultHelper.Success(await _IMenuService.DeleteMenu(id));
        }

        [HttpPost]
        public async Task<ApiResult> EditMenu(MenuEdit request)
        {
            userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _IMenuService.EditMenu(request, userId));
        }

        [HttpPost]
        public async Task<ApiResult> GetMenus(MenuRequest request)
        {
            userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _IMenuService.GetMenus(request, userId));
        }

        [HttpGet]
        public async Task<ApiResult> SettingMenu(string roleId, string menuIds)
        {
            return ResultHelper.Success(await _IMenuService.SettingMenu(roleId, menuIds));
        }
    }
}
