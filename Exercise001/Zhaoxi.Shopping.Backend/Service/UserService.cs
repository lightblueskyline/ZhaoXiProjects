using AutoMapper;
using Interface;
using Model.DTO.Login;
using Model.DTO.User;
using Model.Entity;
using Model.Other;
using SqlSugar;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private ISqlSugarClient _db { get; set; }

        public UserService(IMapper mapper, ISqlSugarClient db)
        {
            _mapper = mapper;
            _db = db;
        }

        /// <summary>
        /// 登陆时获取用户信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<UserRes> GetUser(LoginReq req)
        {
            var user = await _db.Queryable<Users>()
                .Where(x => x.Name == req.UserName && x.Password == req.PassWord)
                .Select(x => new UserRes() { }, true) // true 表示自动映射，实体转换为 DTO
                .FirstAsync();
            return user;
        }

        /// <summary>
        /// 刷新 Token
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserRes> Get(string id)
        {
            return await _db.Queryable<Users>()
                .Where(x => x.ID == id)
                .Select(x => new UserRes() { }, true)
                .FirstAsync();
        }

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> EditNickNameOrPassword(string userID, PersonEdit req)
        {
            var info = await _db.Queryable<Users>().FirstAsync(x => x.ID == userID);
            if (info != null)
            {
                // 不为空则修改，为空则不修改
                if (!string.IsNullOrEmpty(req.NickName))
                {
                    info.NickName = req.NickName;
                }
                if (!string.IsNullOrEmpty(req.Password))
                {
                    info.Password = req.Password;
                }
                if (!string.IsNullOrEmpty(req.Image))
                {
                    info.Image = req.Image;
                }
                return await _db.Updateable<Users>(info).ExecuteCommandHasChangeAsync();
            }

            return false;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<bool> Add(UserAdd req, string userID)
        {
            Users info = _mapper.Map<Users>(req);
            info.ID = Guid.NewGuid().ToString();
            info.CreateUserID = userID;
            info.CreateDate = DateTime.Now;
            info.IsDeleted = false;
            info.UserType = 1;
            return await _db.Insertable<Users>(info).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<bool> Edit(UserEdit req, string userID)
        {
            var info = _db.Queryable<Users>().First(x => x.ID == req.ID);
            _mapper.Map(req, info);
            info.ModifyUserID = userID;
            info.ModifyDate = DateTime.Now;
            return await _db.Updateable<Users>(info).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(string id)
        {
            var info = _db.Queryable<Users>().First(x => x.ID == id);
            return await _db.Deleteable<Users>(info).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> BatchDelete(string ids)
        {
            var list = _db.Queryable<Users>().Where(x => ids.Contains(x.ID.ToString()));
            return await _db.Deleteable<Users>(list).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        ///  获取用户列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<PageInfo<UserRes>> GetUsers(UserReq req, string userID)
        {
            PageInfo<UserRes> result = new PageInfo<UserRes>();
            // 异步分页
            int total = 0;
            var list = await _db.Queryable<Users>()
                .LeftJoin<Users>((u1, u2) => u1.CreateUserID == u2.ID)
                .LeftJoin<Users>((u1, u2, u3) => u1.ModifyUserID == u2.ID)
                .WhereIF(!string.IsNullOrEmpty(req.Name), u1 => u1.Name.Contains(req.Name))
                .WhereIF(!string.IsNullOrEmpty(req.Description), u1 => u1.Description.Contains(req.Description))
                .OrderByDescending(u1 => u1.CreateDate)
                .Select((u1, u2, u3) => new UserRes()
                {
                    ID = u1.ID,
                    Name = u1.Name,
                    NickName = u1.NickName,
                    Password = u1.Password,
                    IsDeleted = u1.IsDeleted,
                    Description = u1.Description,
                    CreateDate = u1.CreateDate,
                    ModifyDate = u1.ModifyDate,
                    CreateUserName = SqlFunc.IsNullOrEmpty(u2.Name) ? "Administrator" : u2.Name,
                    ModifyUserName = u3.Name,
                    // 通过子查询实现角色名称的查询和拼接
                    RoleName = SqlFunc.Subqueryable<Role>()
                        .InnerJoin<UserRoleRelation>((r, urr) => r.ID == urr.RoleID && urr.UserID == u1.ID)
                        .SelectStringJoin(r => r.Name, ",")
                }, true)
                .ToOffsetPageAsync(req.PageIndex, req.PageSize, total);
            result.Data = list;
            result.Total = total;
            return result;
        }

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleIDs"></param>
        /// <returns></returns>
        public async Task<bool> SettingRole(string userID, string roleIDs)
        {
            string[] ridArr = roleIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            // 先删除关系，后批量新增关系
            await _db.Deleteable<UserRoleRelation>(x => x.UserID == userID).ExecuteCommandAsync();
            var newList = new List<UserRoleRelation>();
            foreach (var item in ridArr)
            {
                newList.Add(new UserRoleRelation()
                {
                    ID = Guid.NewGuid().ToString(),
                    UserID = userID,
                    RoleID = item
                });
            }
            return await _db.Insertable<UserRoleRelation>(newList).ExecuteCommandAsync() > 0;
        }
    }
}
