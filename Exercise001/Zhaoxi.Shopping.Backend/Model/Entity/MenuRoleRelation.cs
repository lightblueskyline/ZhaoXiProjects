using Model.Common;
using SqlSugar;

namespace Model.Entity
{
    /// <summary>
    /// 菜单角色关系表
    /// </summary>
    public class MenuRoleRelation : IBase
    {
        /// <summary>
        /// 菜单主键
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string MenuID { get; set; }

        /// <summary>
        /// 角色主键
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string RoleID { get; set; }
    }
}
