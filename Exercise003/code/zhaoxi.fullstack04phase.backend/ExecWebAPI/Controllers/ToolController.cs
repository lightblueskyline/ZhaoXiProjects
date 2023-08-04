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
            Users users = new Users()
            {
                Name = "Admin",
                NickName = "超级管理员",
                Password = "123456",
                UserType = 0,
                IsEnable = true,
                Description = "数据库初始化时默认添加的超级管理员",
                CreateDate = DateTime.Now,
                CreateUserId = 0,
            };
            // 取得 UserId
            long userId = await _dbClient.Insertable<Users>(users).ExecuteReturnBigIdentityAsync();
            //
            Menu menu1 = new Menu()
            {
                Name = "菜单管理",
                Index = "/menu",
                FilePath = "Menu.vue",
                ParentId = 0,
                Order = 1,
                IsEnable = true,
                Icon = "Folder",
                Description = "数据库初始化时默认添加的默认菜单",
                CreateDate = DateTime.Now,
                CreateUserId = userId
            };
            long menu1Id = await _dbClient.Insertable<Menu>(menu1).ExecuteReturnBigIdentityAsync();
            //
            Menu menu2 = new Menu()
            {
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
            int count = await _dbClient.Insertable<Menu>(menu2).ExecuteCommandAsync();
            #endregion

            return (count > 0);
        }
    }
}
