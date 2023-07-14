using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using SqlSugar;
using System.Reflection;

namespace ExeWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToolController : ControllerBase
    {
        private readonly ISqlSugarClient _db;

        public ToolController(ISqlSugarClient db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<bool> CreateDatabaseTable()
        {
            // 1. 创建数据库
            // _db.DbMaintenance.CreateDatabase();
            // 2. 通过反射，加载程序集，读取到所有类型，然后根据实体创建表
            string nameSpace = "Model.Entity";
            Type[] assembly = Assembly.LoadFrom(AppContext.BaseDirectory + "Model.dll").GetTypes()
                .Where(x => x.Namespace == nameSpace).ToArray();
            _db.CodeFirst.SetStringDefaultLength(200).InitTables(assembly);

            #region 初始化数据
            // 超级管理员
            Users users = new Users()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "Administrator",
                NickName = "超级管理员",
                Password = "P@ssw0rd",
                UserType = 0,
                IsEnable = true,
                Description = "数据库初始化时默认添加的超级管理员",
                CreateDate = DateTime.Now,
                CreateUserID = "",
            };
            string userID = (await _db.Insertable<Users>(users).ExecuteReturnEntityAsync()).ID;
            // 菜单
            Menu menu1 = new Menu()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "菜单管理",
                Index = "/menu",
                FilePath = "menu.vue",
                ParentID = "",
                Order = 1,
                IsEnable = true,
                Icon = "folder",
                Description = "数据库初始化时添加的菜单",
                CreateDate = DateTime.Now,
                CreateUserID = userID,
            };
            string menu1ID = (await _db.Insertable<Menu>(menu1).ExecuteReturnEntityAsync()).ID;
            Menu menu2 = new Menu()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "菜单列表",
                Index = "/menu",
                FilePath = "menu.vue",
                ParentID = menu1ID,
                Order = 1,
                IsEnable = true,
                Icon = "notebook",
                Description = "数据库初始化时添加的菜单",
                CreateDate = DateTime.Now,
                CreateUserID = userID,
            };
            await _db.Insertable(menu2).ExecuteReturnEntityAsync();
            Menu menu3 = new Menu()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "角色管理",
                Index = "/role",
                FilePath = "role.vue",
                ParentID = "",
                Order = 1,
                IsEnable = true,
                Icon = "folder",
                Description = "数据库初始化时添加的菜单",
                CreateDate = DateTime.Now,
                CreateUserID = userID,
            };
            string menu3ID = (await _db.Insertable<Menu>(menu3).ExecuteReturnEntityAsync()).ID;
            Menu menu4 = new Menu()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "角色列表",
                Index = "/role",
                FilePath = "role.vue",
                ParentID = menu3ID,
                Order = 1,
                IsEnable = true,
                Icon = "notebook",
                Description = "数据库初始化时添加的菜单",
                CreateDate = DateTime.Now,
                CreateUserID = userID,
            };
            await _db.Insertable(menu4).ExecuteReturnEntityAsync();
            Menu menu5 = new Menu()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "用户管理",
                Index = "/user",
                FilePath = "user.vue",
                ParentID = "",
                Order = 1,
                IsEnable = true,
                Icon = "folder",
                Description = "数据库初始化时添加的菜单",
                CreateDate = DateTime.Now,
                CreateUserID = userID,
            };
            string menu5ID = (await _db.Insertable<Menu>(menu5).ExecuteReturnEntityAsync()).ID;
            Menu menu6 = new Menu()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "角色列表",
                Index = "/user",
                FilePath = "user.vue",
                ParentID = menu5ID,
                Order = 1,
                IsEnable = true,
                Icon = "notebook",
                Description = "数据库初始化时添加的菜单",
                CreateDate = DateTime.Now,
                CreateUserID = userID,
            };
            await _db.Insertable(menu6).ExecuteReturnEntityAsync();
            #endregion

            return true;
        }
    }
}
