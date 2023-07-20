using ExeWebApi.Config;
using Interface;
using Microsoft.AspNetCore.Mvc;
using Model.DTO.Menu;
using Model.Other;

namespace ExeWebApi.Controllers
{
    public class MenuController : MyBaseController
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpPost]
        public async Task<ApiResult> Add(MenuAdd req)
        {
            UserID = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _menuService.Add(req, UserID));
        }

        [HttpPost]
        public async Task<ApiResult> Edit(MenuEdit req)
        {
            UserID = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _menuService.Edit(req, UserID));
        }

        [HttpGet]
        public async Task<ApiResult> Delete(string id)
        {
            return ResultHelper.Success(await _menuService.Delete(id));
        }

        [HttpGet]
        public async Task<ApiResult> BatchDelete(string ids)
        {
            return ResultHelper.Success(await _menuService.BatchDelete(ids));
        }

        [HttpPost]
        public async Task<ApiResult> GetMenus(MenuReq req)
        {
            UserID = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _menuService.GetMenus(req, UserID));
        }

        [HttpGet]
        public async Task<ApiResult> SettingMenu(string roleID, string menuIDs)
        {
            return ResultHelper.Success(await _menuService.SettingMenu(roleID, menuIDs));
        }
    }
}
