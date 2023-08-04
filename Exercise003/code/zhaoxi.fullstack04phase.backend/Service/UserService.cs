using Interface;
using Model.DTO.Login;
using Model.DTO.User;
using Model.Entity;
using SqlSugar; // 通过 <ProjectReference Include="..\Model\Model.csproj" /> 带入

namespace Service
{
    public class UserService : IUserService
    {
        private readonly ISqlSugarClient _dbClient;

        public UserService(ISqlSugarClient dbClient)
        {
            _dbClient = dbClient;
        }

        public Task<UserResponse> Get(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登录
        /// 生成 Token 使用
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public async Task<UserResponse> GetUser(LoginRequest loginRequest)
        {
            return await _dbClient.Queryable<Users>()
                .Where(x => x.Name == loginRequest.UserName && x.Password == loginRequest.PassWord)
                .Select(s => new UserResponse() { }, true) // true 表示两边同名字段，自动映射
                .FirstAsync();
        }
    }
}