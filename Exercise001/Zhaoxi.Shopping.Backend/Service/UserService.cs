using Interface;
using Model.DTO.Login;
using Model.DTO.User;
using Model.Entity;
using SqlSugar;

namespace Service
{
    public class UserService : IUserService
    {
        private ISqlSugarClient _db { get; set; }

        public UserService(ISqlSugarClient db)
        {
            _db = db;
        }

        public async Task<UserRes> GetUser(LoginReq req)
        {
            var user = await _db.Queryable<Users>()
                .Where(x => x.Name == req.UserName && x.Password == req.PassWord)
                .Select(x => new UserRes() { }, true) // true 表示自动映射
                .FirstAsync();
            return user;
        }
    }
}
