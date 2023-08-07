using Model.DTO.Menu;

namespace Interface
{
    public interface IMenuService
    {
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> AddMenu(MenuAdd request, string userId);

        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> EditMenu(MenuEdit request, string userId);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteMenu(string id);

        /// <summary>
        /// 批量删除菜单
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> BatchDeleteMenu(string ids);

        /// <summary>
        /// 取得菜单列表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<MenuResponse>> GetMenus(MenuRequest request, string userId);

        /// <summary>
        /// 设置菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        Task<bool> SettingMenu(string roleId, string menuIds);
    }
}
