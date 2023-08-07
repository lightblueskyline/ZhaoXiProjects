using AutoMapper;
using Interface;
using Model.DTO.Login;
using Model.DTO.User;
using Model.Entity;
using Model.Other;
using SqlSugar; // 通过 <ProjectReference Include="..\Model\Model.csproj" /> 带入

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _IMapper;
        private readonly ISqlSugarClient _ISqlSugarClient;

        public UserService(IMapper mapper, ISqlSugarClient sqlSugarClient)
        {
            _IMapper = mapper;
            _ISqlSugarClient = sqlSugarClient;
        }

        public async Task<bool> AddUser(UserAdd request, string userId)
        {
            Users user = _IMapper.Map<Users>(request);
            user.Id = Guid.NewGuid().ToString();
            user.CreateUserId = userId;
            user.CreateDate = DateTime.Now;
            user.IsDeleted = false;
            user.UserType = 1; // 普通永华
            return await _ISqlSugarClient.Insertable<Users>(user).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> BatchDeleteUser(string ids)
        {
            var list = _ISqlSugarClient.Queryable<Users>()
                .Where(x => ids.Contains(x.Id));
            return await _ISqlSugarClient.Deleteable<Users>(list).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> DeleteUser(string id)
        {
            Users user = await _ISqlSugarClient.Queryable<Users>()
                .FirstAsync(x => x.Id == id);
            return await _ISqlSugarClient.Deleteable<Users>(user).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> EditNickNameOrPassword(string userId, PersonEdit request)
        {
            Users user = await _ISqlSugarClient.Queryable<Users>()
                .FirstAsync(x => x.Id == userId);
            if (user != null)
            {
                // 不为空则修改，为空则跳过
                if (!string.IsNullOrEmpty(request.NickName))
                {
                    user.NickName = request.NickName;
                }
                if (!string.IsNullOrEmpty(request.Password))
                {
                    user.Password = request.Password;
                }
                if (!string.IsNullOrEmpty(request.Image))
                {
                    user.Image = request.Image;
                }
                return await _ISqlSugarClient.Updateable<Users>(user).ExecuteCommandHasChangeAsync();
            }
            return false;
        }

        public async Task<bool> EditUser(UserEdit request, string userId)
        {
            Users user = await _ISqlSugarClient.Queryable<Users>()
                .FirstAsync(x => x.Id == userId);
            _IMapper.Map(request, user);
            user.ModifyUserId = userId;
            user.ModifyDate = DateTime.Now;
            return await _ISqlSugarClient.Updateable<Users>(user).ExecuteCommandAsync() > 0;
        }

        public async Task<UserResponse> Get(string id)
        {
            return await _ISqlSugarClient.Queryable<Users>()
                .Where(x => x.Id == id)
                .Select(x => new UserResponse() { }, true)
                .FirstAsync();
        }

        /// <summary>
        /// 登录
        /// 生成 Token 使用
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public async Task<UserResponse> GetUser(LoginRequest loginRequest)
        {
            return await _ISqlSugarClient.Queryable<Users>()
                .Where(x => x.Name == loginRequest.UserName && x.Password == loginRequest.PassWord)
                .Select(s => new UserResponse() { }, true) // true 表示两边同名字段，自动映射
                .FirstAsync();
        }

        public async Task<PageInfo<UserResponse>> GetUsers(UserRequest request, string userId)
        {
            PageInfo<UserResponse> result = new PageInfo<UserResponse>();
            // 异步分页
            RefAsync<int> total = 0;
            var list = await _ISqlSugarClient.Queryable<Users>()
                .LeftJoin<Users>((u1, u2) => u1.CreateUserId == u2.Id)
                .LeftJoin<Users>((u1, u2, u3) => u1.ModifyUserId == u3.Id)
                .WhereIF(!string.IsNullOrEmpty(request.Name), u1 => u1.Name.Contains(request.Name))
                .WhereIF(!string.IsNullOrEmpty(request.Description), u1 => u1.Description.Contains(request.Description))
                .OrderByDescending(u1 => u1.CreateDate)
                .Select((u1, u2, u3) => new UserResponse()
                {
                    Id = u1.Id,
                    Name = u1.Name,
                    Password = u1.Password,
                    IsDeleted = u1.IsDeleted,
                    Description = u1.Description,
                    CreateDate = u1.CreateDate,
                    CreateUserName = SqlFunc.IsNullOrEmpty(u2.Name) ? "Admin" : u2.Name,
                    ModifyDate = u1.ModifyDate,
                    ModifyUserName = SqlFunc.IsNullOrEmpty(u3.Name) ? "" : u3.Name, // SqlSugar 函数
                    RoleName = SqlFunc.Subqueryable<Role>()
                        .InnerJoin<UserRoleRelation>((r, urr) => r.Id == urr.RoleId && urr.UserId == u1.Id)
                        .SelectStringJoin(r => r.Name, ",") // 通过子查询实现角色名称的查询和拼接
                }, true)
                .ToOffsetPageAsync(request.PageIndex, request.PageSize, total);
            result.Data = list;
            result.Total = total;
            return result;
        }

        public async Task<bool> SettingUserRole(string userId, string roleIds)
        {
            string[] ridArray = roleIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            // 先删除，后批量新增
            await _ISqlSugarClient.Deleteable<UserRoleRelation>(x => x.UserId == userId)
                .ExecuteCommandAsync();
            List<UserRoleRelation> newList = new List<UserRoleRelation>();
            foreach (string rid in ridArray)
            {
                newList.Add(new UserRoleRelation()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    RoleId = rid
                });
            }
            return await _ISqlSugarClient.Insertable<UserRoleRelation>(newList).ExecuteCommandAsync() > 0;
        }
    }
}