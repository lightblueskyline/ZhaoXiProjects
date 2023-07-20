using AutoMapper;
using Interface;
using Model.DTO.Role;
using Model.Entity;
using Model.Other;
using SqlSugar;

namespace Service
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private ISqlSugarClient _db { get; set; }

        public RoleService(IMapper mapper, ISqlSugarClient db)
        {
            _mapper = mapper;
            _db = db;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<bool> Add(RoleAdd req, string userID)
        {
            Role info = _mapper.Map<Role>(req);
            info.ID = Guid.NewGuid().ToString();
            info.CreateUserID = userID;
            info.CreateDate = DateTime.Now;
            info.IsDeleted = false;
            return await _db.Insertable<Role>(info).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<bool> Edit(RoleEdit req, string userID)
        {
            var info = _db.Queryable<Role>().First(x => x.ID == req.ID);
            _mapper.Map(req, info);
            info.ModifyUserID = userID;
            info.ModifyDate = DateTime.Now;
            return await _db.Updateable<Role>(info).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(string id)
        {
            var info = _db.Queryable<Role>().First(x => x.ID == id);
            return await _db.Deleteable<Role>(info).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> BatchDelete(string ids)
        {
            var list = _db.Queryable<Role>().Where(x => ids.Contains(x.ID.ToString()));
            return await _db.Deleteable<Role>(list).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<PageInfo<RoleRes>> GetRoles(RoleReq req, string userID)
        {
            PageInfo<RoleRes> res = new PageInfo<RoleRes>();
            int total = 0;
            var list = await _db.Queryable<Role>()
                .LeftJoin<Users>((r, u1) => r.CreateUserID == u1.ID)
                .LeftJoin<Users>((r, u1, u2) => r.ModifyUserID == u2.ID)
                .WhereIF(!string.IsNullOrEmpty(req.Name), x => x.Name.Contains(req.Name))
                .WhereIF(!string.IsNullOrEmpty(req.Description), x => x.Description.Contains(req.Description))
                .OrderBy((r) => r.Order)
                .Select((r, u1, u2) => new RoleRes()
                {
                    Name = r.Name,
                    CreateUserName = u1.Name,
                    ModifyUserName = u2.Name,
                }, true)
                .ToOffsetPageAsync(req.PageIndex, req.PageSize, total);
            res.Total = total;
            res.Data = list;
            return res;
        }
    }
}
