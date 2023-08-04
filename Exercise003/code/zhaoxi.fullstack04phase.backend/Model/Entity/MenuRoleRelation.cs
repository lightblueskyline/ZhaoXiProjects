using Model.Common;
using SqlSugar;

namespace Model.Entity
{
    [SugarTable(TableName = "MenuRoleRelation")]
    public class MenuRoleRelation : IBase
    {
        /// <summary>
        /// 菜单主键
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long MenuId { get; set; }

        /// <summary>
        /// 角色主键
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long RoleId { get; set; }
    }
}
