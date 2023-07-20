using Model.DTO.Menu;

namespace Interface
{
    public interface IMenuService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<bool> Add(MenuAdd req, string userID);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<bool> Edit(MenuEdit req, string userID);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(string id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> BatchDelete(string ids);

        /// <summary>
        /// 获取附带权限的菜单列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<List<MenuRes>> GetMenus(MenuReq req, string userID);

        /// <summary>
        /// 设置菜单
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="mids"></param>
        /// <returns></returns>
        Task<bool> SettingMenu(string rid, string mids);
    }
}
