using Model.DTO.Role;
using Model.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IRoleService
    {
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> AddRole(RoleAdd request, string userId);

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> EditRole(RoleEdit request, string userId);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteRole(string id);

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> BatchDeleteRole(string ids);

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PageInfo<RoleResponse>> GetRoles(RoleRequest request, string userId);
    }
}
