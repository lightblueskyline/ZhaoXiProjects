using Model.Common;
using SqlSugar;

namespace Model.Entity
{
    [SugarTable(TableName = "UserRoleRelation")]
    public class UserRoleRelation : IBase
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string UserId { get; set; }

        /// <summary>
        /// 角色主键
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string RoleId { get; set; }
    }
}
