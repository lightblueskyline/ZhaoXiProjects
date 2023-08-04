using Model.DTO.Login;
using Model.DTO.User;

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
    }
}