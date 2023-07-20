using Model.DTO.Role;
using Model.Other;

namespace Interface
{
    public interface IRoleService
    {

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<bool> Add(RoleAdd req, string userID);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<bool> Edit(RoleEdit req, string userID);

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
        /// 获取角色列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<PageInfo<RoleRes>> GetRoles(RoleReq req, string userID);
    }
}
