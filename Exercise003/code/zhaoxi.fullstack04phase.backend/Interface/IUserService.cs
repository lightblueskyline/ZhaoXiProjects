using Model.DTO.Login;
using Model.DTO.User;
using Model.Other;

namespace Interface
{
    public interface IUserService
    {
        /// <summary>
        /// 登录
        /// 生成 Token 使用
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        Task<UserResponse> GetUser(LoginRequest loginRequest);

        Task<UserResponse> Get(string id);

        /// <summary>
        /// 添加用户啊
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> AddUser(UserAdd request, string userId);

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> EditUser(UserEdit request, string userId);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteUser(string id);

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> BatchDeleteUser(string ids);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<PageInfo<UserResponse>> GetUsers(UserRequest request, string userId);

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<bool> SettingUserRole(string userId, string roleIds);

        /// <summary>
        /// 修改昵称或者密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditNickNameOrPassword(string userId, PersonEdit request);
    }
}