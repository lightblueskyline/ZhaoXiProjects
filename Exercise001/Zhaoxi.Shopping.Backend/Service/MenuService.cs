using AutoMapper;
using Interface;
using Model.DTO.Menu;
using Model.Entity;
using SqlSugar;

namespace Service
{
    public class MenuService : IMenuService
    {
        private readonly IMapper _mapper;
        private ISqlSugarClient _db { get; set; }

        public MenuService(IMapper mapper, ISqlSugarClient db)
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
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> Add(MenuAdd req, string userID)
        {
            Menu info = _mapper.Map<Menu>(req);
            info.ID = Guid.NewGuid().ToString();
            info.CreateUserID = userID;
            info.CreateDate = DateTime.Now;
            info.IsDeleted = false;
            return await _db.Insertable<Menu>(info).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> Edit(MenuEdit req, string userID)
        {
            var info = _db.Queryable<Menu>().First(x => x.ID == req.ID);
            _mapper.Map(req, info);
            info.ModifyUserID = userID;
            info.ModifyDate = DateTime.Now;
            return await _db.Updateable<Menu>(info).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> Delete(string id)
        {
            var info = _db.Queryable<Menu>().First(x => x.ID == id);
            return await _db.Deleteable<Menu>(info).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> BatchDelete(string ids)
        {
            var list = _db.Queryable<Menu>().Where(x => ids.Contains(x.ID.ToString()));
            return await _db.Deleteable<Menu>(list).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 获取附带权限的菜单列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<MenuRes>> GetMenus(MenuReq req, string userID)
        {
            // 查询用户信息，判断是否未超级管理员
            var user = await _db.Queryable<Users>().FirstAsync(x => x.ID == userID);
            if (user.UserType == 0)
            {
                var list = await _db.Queryable<Menu>()
                    .WhereIF(!string.IsNullOrEmpty(req.Name), x => x.Name.Contains(req.Name))
                    .WhereIF(!string.IsNullOrEmpty(req.Index), x => x.Index.Contains(req.Index))
                    .WhereIF(!string.IsNullOrEmpty(req.FilePath), x => x.FilePath.Contains(req.FilePath))
                    .WhereIF(!string.IsNullOrEmpty(req.Description), x => x.Description.Contains(req.Description))
                    .OrderBy(x => x.Order)
                    .Select(x => new MenuRes() { }, true)
                    .ToTreeAsync(x => x.Children, x => x.ParentID, "");
                return list;
            }
            else
            {
                var list = await _db.Queryable<Menu>()
                    .InnerJoin<MenuRoleRelation>((m, mrr) => m.ID == mrr.MenuID)
                    .InnerJoin<Role>((m, mrr, r) => r.ID == mrr.RoleID)
                    .InnerJoin<UserRoleRelation>((m, mrr, r, urr) => r.ID == urr.RoleID)
                    .InnerJoin<Users>((m, mrr, r, urr, u) => u.ID == urr.UserID && u.ID == userID)
                    .WhereIF(!string.IsNullOrEmpty(req.Name), x => x.Name.Contains(req.Name))
                    .WhereIF(!string.IsNullOrEmpty(req.Index), x => x.Index.Contains(req.Index))
                    .WhereIF(!string.IsNullOrEmpty(req.FilePath), x => x.FilePath.Contains(req.FilePath))
                    .WhereIF(!string.IsNullOrEmpty(req.Description), x => x.Description.Contains(req.Description))
                    .OrderBy(x => x.Order)
                    .Select(x => new MenuRes() { }, true)
                    .ToTreeAsync(x => x.Children, x => x.ParentID, "");
                return list;
            }
        }

        /// <summary>
        /// 设置菜单
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="mids"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> SettingMenu(string rid, string mids)
        {
            string[] midArr = mids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            // 先删除管理，后批量新增关系
            await _db.Deleteable<MenuRoleRelation>(x => x.RoleID == rid).ExecuteCommandAsync();
            var newList = new List<MenuRoleRelation>();
            foreach (string mid in midArr)
            {
                newList.Add(new MenuRoleRelation()
                {
                    ID = Guid.NewGuid().ToString(),
                    RoleID = rid,
                    MenuID = mid,
                });
            }
            return await _db.Insertable(newList).ExecuteCommandAsync() > 0;
        }
    }
}
