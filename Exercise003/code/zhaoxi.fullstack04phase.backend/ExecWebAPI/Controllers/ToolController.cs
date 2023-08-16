using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using SqlSugar;
using System.Reflection;

namespace ExecWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToolController : ControllerBase
    {
        private readonly ILogger<ToolController> _logger;
        private readonly ISqlSugarClient _dbClient;

        public ToolController(ILogger<ToolController> logger, ISqlSugarClient dbClient)
        {
            // 构造函数注入
            _logger = logger;
            _dbClient = dbClient;
        }

        /// <summary>
        /// 通过代码生成数据库、表、资料
        /// PS: 请谨慎使用，此方法仅用于首次初始化数据库、数据表
        /// 初始化完成之后若再次使用，会将已有数据表删除，重新初始化为原始数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> CodeFirstGenerateDB_Table()
        {
            #region 1. 生成数据库
            _dbClient.DbMaintenance.CreateDatabase();
            List<DbTableInfo> listTable = _dbClient.DbMaintenance.GetTableInfoList();
            // 存在表，删除表，清空资料
            if (listTable != null && listTable.Count > 0)
            {
                listTable.ForEach(t =>
                {
                    _dbClient.DbMaintenance.DropTable(t.Name);
                });
            }
            #endregion

            #region 2. 生成表
            string entityNS = "Model.Entity"; // 实体命名空间
            Type[] entityAssembly = Assembly.LoadFrom(AppContext.BaseDirectory + "Model.dll")
                .GetTypes().Where(x => x.Namespace == entityNS).ToArray();
            // 为所有字符串类型的属性指定一个长度(默认为 100)，或者通过特性在数据库实体中添加
            _dbClient.CodeFirst.SetStringDefaultLength(300).InitTables(entityAssembly);
            #endregion

            #region 3. 添加测试数据(初始化超级管理员和菜单)

            #region 初始化超级管理员
            Users users = new Users()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                NickName = "超级管理员",
                Password = "123456",
                UserType = 0,
                IsEnable = true,
                Description = "数据库初始化时默认添加的超级管理员",
                CreateDate = DateTime.Now,
                CreateUserId = "",
            };
            // 取得 UserId
            string userId = (await _dbClient.Insertable<Users>(users).ExecuteReturnEntityAsync()).Id;
            #endregion

            #region 菜单
            Menu menu1 = new Menu()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "菜单管理",
                Index = "/menu",
                FilePath = "Menu.vue",
                ParentId = "",
                Order = 1,
                IsEnable = true,
                Icon = "Folder",
                Description = "数据库初始化时默认添加的默认菜单",
                CreateDate = DateTime.Now,
                CreateUserId = userId
            };
            string menu1Id = (await _dbClient.Insertable<Menu>(menu1).ExecuteReturnEntityAsync()).Id;
            //
            Menu menu1_1 = new Menu()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "菜单列表",
                Index = "/menu",
                FilePath = "Menu.vue",
                ParentId = menu1Id,
                Order = 1,
                IsEnable = true,
                Icon = "Notebook",
                Description = "数据库初始化时默认添加的默认菜单",
                CreateDate = DateTime.Now,
                CreateUserId = userId
            };
            int count = await _dbClient.Insertable<Menu>(menu1_1).ExecuteCommandAsync();
            #endregion

            #region 角色
            Menu menu2 = new Menu()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "角色管理",
                Index = "/role",
                FilePath = "Role.vue",
                ParentId = "",
                Order = 1,
                IsEnable = true,
                Icon = "Folder",
                Description = "数据库初始化时默认添加的默认菜单",
                CreateDate = DateTime.Now,
                CreateUserId = userId
            };
            string menu2Id = (await _dbClient.Insertable<Menu>(menu2).ExecuteReturnEntityAsync()).Id;
            //
            Menu menu2_1 = new Menu()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "角色列表",
                Index = "/role",
                FilePath = "Role.vue",
                ParentId = menu2Id,
                Order = 1,
                IsEnable = true,
                Icon = "Notebook",
                Description = "数据库初始化时默认添加的默认菜单",
                CreateDate = DateTime.Now,
                CreateUserId = userId
            };
            count = await _dbClient.Insertable<Menu>(menu2_1).ExecuteCommandAsync();
            #endregion

            #region 用户
            Menu menu3 = new Menu()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "用户管理",
                Index = "/user",
                FilePath = "User.vue",
                ParentId = "",
                Order = 1,
                IsEnable = true,
                Icon = "Folder",
                Description = "数据库初始化时默认添加的默认菜单",
                CreateDate = DateTime.Now,
                CreateUserId = userId
            };
            string menu3Id = (await _dbClient.Insertable<Menu>(menu3).ExecuteReturnEntityAsync()).Id;
            //
            Menu menu3_1 = new Menu()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "用户列表",
                Index = "/user",
                FilePath = "User.vue",
                ParentId = menu3Id,
                Order = 1,
                IsEnable = true,
                Icon = "Notebook",
                Description = "数据库初始化时默认添加的默认菜单",
                CreateDate = DateTime.Now,
                CreateUserId = userId
            };
            count = await _dbClient.Insertable<Menu>(menu3_1).ExecuteCommandAsync();
            #endregion

            #endregion

            return (count > 0);
        }
    }
}
