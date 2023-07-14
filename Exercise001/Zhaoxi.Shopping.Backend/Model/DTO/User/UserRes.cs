namespace Model.DTO.User
{
    public class UserRes
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户类型 (0=超级管理员 1=普通用户)
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 创建者名称
        /// </summary>
        public string CreateUserName { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改者名称
        /// </summary>
        public string ModifyUserName { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? ModifyDate { get; set; }

        /// <summary>
        /// 是否启用 (0=未启用 1=启用)
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Image { get; set; }
    }
}
