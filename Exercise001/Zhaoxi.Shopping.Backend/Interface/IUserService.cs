using Model.DTO.Login;
using Model.DTO.User;

namespace Interface
{
    public interface IUserService
    {
        Task<UserRes> GetUser(LoginReq req);
    }
}
