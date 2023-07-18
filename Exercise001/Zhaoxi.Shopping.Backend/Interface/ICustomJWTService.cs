using Model.DTO.User;

namespace Interface
{
    public interface ICustomJWTService
    {
        /// <summary>
        /// 获取 Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<string> GetToken(UserRes user);
    }
}
