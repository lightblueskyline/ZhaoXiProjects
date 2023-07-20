using Model.DTO.Login;
using Model.DTO.Role;
using Model.DTO.User;
using Model.Other;

namespace Interface
{
    public interface IUserService
    {
        /// <summary>
        /// 登陆时获取用户信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<UserRes> GetUser(LoginReq req);

        /// <summary>
        /// 刷新 Token
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserRes> Get(string id);

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<bool> EditNickNameOrPassword(string userID, PersonEdit req);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<bool> Add(UserAdd req, string userID);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<bool> Edit(UserEdit req, string userID);

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
        ///  获取用户列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<PageInfo<UserRes>> GetUsers(UserReq req, string userID);

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        Task<bool> SettingRole(string userID, string roleID);
    }
}
