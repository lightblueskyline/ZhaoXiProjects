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
        private readonly IMapper _IMapper;
        private readonly ISqlSugarClient _ISqlSugarClient;

        public RoleService(IMapper mapper, ISqlSugarClient sqlSugarClient)
        {
            _IMapper = mapper;
            _ISqlSugarClient = sqlSugarClient;
        }

        public async Task<bool> AddRole(RoleAdd request, string userId)
        {
            Role role = _IMapper.Map<Role>(request);
            role.Id = Guid.NewGuid().ToString();
            role.CreateUserId = userId;
            role.CreateDate = DateTime.Now;
            role.IsDeleted = false;
            return await _ISqlSugarClient.Insertable<Role>(role).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> BatchDeleteRole(string ids)
        {
            var list = _ISqlSugarClient.Queryable<Role>()
                .Where(x => ids.Contains(x.Id));
            return await _ISqlSugarClient.Deleteable<Role>(list).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> DeleteRole(string id)
        {
            var role = _ISqlSugarClient.Queryable<Role>()
                .Where(x => x.Id == id);
            return await _ISqlSugarClient.Deleteable<Role>(role).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> EditRole(RoleEdit request, string userId)
        {
            var role = await _ISqlSugarClient.Queryable<Role>()
                .FirstAsync(x => x.Id == request.Id);
            _IMapper.Map(request, role);
            role.ModifyUserId = userId;
            role.ModifyDate = DateTime.Now;
            return await _ISqlSugarClient.Updateable<Role>(role).ExecuteCommandAsync() > 0;
        }

        public async Task<PageInfo<RoleResponse>> GetRoles(RoleRequest request, string userId)
        {
            PageInfo<RoleResponse> result = new PageInfo<RoleResponse>();
            int total = 0;
            var list = await _ISqlSugarClient.Queryable<Role>()
                .LeftJoin<Users>((r, u1) => r.CreateUserId == u1.Id)
                .LeftJoin<Users>((r, u1, u2) => r.ModifyUserId == u2.Id)
                .WhereIF(!string.IsNullOrEmpty(request.Name), x => x.Name.Contains(request.Name))
                .WhereIF(!string.IsNullOrEmpty(request.Description), x => x.Description.Contains(request.Description))
                .OrderBy((r) => r.Order)
                .Select((r, u1, u2) => new RoleResponse()
                {
                    Name = r.Name,
                    CreateUserName = u1.Name,
                    ModifyUserName = u2.Name,
                }, true)
                .ToOffsetPageAsync(request.PageIndex, request.PageSize, total);
            result.Data = list;
            result.Total = total;
            return result;
        }
    }
}
