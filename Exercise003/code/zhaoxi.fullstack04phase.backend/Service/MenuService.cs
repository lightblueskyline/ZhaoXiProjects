using AutoMapper;
using Interface;
using Model.DTO.Menu;
using Model.Entity;
using SqlSugar;

namespace Service
{
    public class MenuService : IMenuService
    {
        private readonly IMapper _IMapper;
        private readonly ISqlSugarClient _ISqlSugarClient;

        public MenuService(IMapper mapper, ISqlSugarClient sqlSugarClient)
        {
            _IMapper = mapper;
            _ISqlSugarClient = sqlSugarClient;
        }

        public async Task<bool> AddMenu(MenuAdd request, string userId)
        {
            // PS: 务必在 AutoMapperConfig 创建映射
            Menu menu = _IMapper.Map<Menu>(request);
            menu.Id = Guid.NewGuid().ToString();
            menu.CreateUserId = userId;
            menu.CreateDate = DateTime.Now;
            // 删除的实现方式有两种：
            // 1. 物理删除 从数据库中删除
            // 2. 软删除 通过某字段来控制数据的显示和隐藏
            menu.IsDeleted = false;

            return await _ISqlSugarClient.Insertable<Menu>(menu)
                .ExecuteCommandIdentityIntoEntityAsync();
        }

        public async Task<bool> BatchDeleteMenu(string ids)
        {
            var list = _ISqlSugarClient.Queryable<Menu>().Where(x => ids.Contains(x.Id));

            return await _ISqlSugarClient.Deleteable<Menu>(list)
                .ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> DeleteMenu(string id)
        {
            Menu menu = await _ISqlSugarClient.Queryable<Menu>()
                .FirstAsync(x => x.Id == id);

            return await _ISqlSugarClient.Deleteable<Menu>(menu)
                .ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> EditMenu(MenuEdit request, string userId)
        {
            Menu menu = await _ISqlSugarClient.Queryable<Menu>()
                .FirstAsync(x => x.Id == request.Id);
            _IMapper.Map(request, menu);

            return await _ISqlSugarClient.Updateable<Menu>(menu)
                .ExecuteCommandHasChangeAsync();
        }

        public async Task<List<MenuResponse>> GetMenus(MenuRequest request, string userId)
        {
            // 查询用户信息
            Users user = await _ISqlSugarClient.Queryable<Users>()
                .FirstAsync(x => x.Id == userId);
            // 区别管理员和普通用户
            if (user.UserType == 0) // 超级管理员
            {
                var menuList = await _ISqlSugarClient.Queryable<Menu>()
                    .WhereIF(!string.IsNullOrEmpty(request.Name), x => x.Name.Contains(request.Name))
                    .WhereIF(!string.IsNullOrEmpty(request.Index), x => x.Index.Contains(request.Index))
                    .WhereIF(!string.IsNullOrEmpty(request.FilePath), x => x.FilePath.Contains(request.FilePath))
                    .WhereIF(!string.IsNullOrEmpty(request.Description), x => x.Description.Contains(request.Description))
                    .Select(x => new MenuResponse() { }, true)
                    .ToTreeAsync(x => x.Children, x => x.ParentId, "");
                return menuList;
            }
            else
            {
                // 从上至下关联
                var menuList = await _ISqlSugarClient.Queryable<Menu>()
                    .InnerJoin<MenuRoleRelation>((m, mrr) => m.Id == mrr.MenuId)
                    .InnerJoin<Role>((m, mrr, r) => r.Id == mrr.RoleId)
                    .InnerJoin<UserRoleRelation>((m, mrr, r, urr) => r.Id == urr.RoleId)
                    .InnerJoin<Users>((m, mrr, r, urr, u) => u.Id == urr.UserId && u.Id == userId)
                    .WhereIF(!string.IsNullOrEmpty(request.Name), x => x.Name.Contains(request.Name))
                    .WhereIF(!string.IsNullOrEmpty(request.Index), x => x.Index.Contains(request.Index))
                    .WhereIF(!string.IsNullOrEmpty(request.FilePath), x => x.FilePath.Contains(request.FilePath))
                    .WhereIF(!string.IsNullOrEmpty(request.Description), x => x.Description.Contains(request.Description))
                    .Select(x => new MenuResponse() { }, true)
                    .ToTreeAsync(x => x.Children, x => x.ParentId, "");
                return menuList;
            }
        }

        public async Task<bool> SettingMenu(string roleId, string menuIds)
        {
            string[] midArray = menuIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            // 先删除关系，之后批量新增关系
            await _ISqlSugarClient.Deleteable<MenuRoleRelation>(x => x.RoleId == roleId).ExecuteCommandAsync();
            var newList = new List<MenuRoleRelation>();
            foreach (var item in midArray)
            {
                newList.Add(new MenuRoleRelation()
                {
                    Id = Guid.NewGuid().ToString(),
                    RoleId = roleId,
                    MenuId = item
                });
            }

            return await _ISqlSugarClient.Insertable<MenuRoleRelation>(newList).ExecuteCommandAsync() > 0;
        }
    }
}
