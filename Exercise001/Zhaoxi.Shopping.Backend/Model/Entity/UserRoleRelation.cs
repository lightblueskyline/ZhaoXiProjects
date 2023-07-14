using Model.Common;
using SqlSugar;

namespace Model.Entity
{
    /// <summary>
    /// 用户角色关系表
    /// </summary>
    public class UserRoleRelation : IBase
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string UserID { get; set; }

        /// <summary>
        /// 角色主键
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string RoleID { get; set; }
    }
}
