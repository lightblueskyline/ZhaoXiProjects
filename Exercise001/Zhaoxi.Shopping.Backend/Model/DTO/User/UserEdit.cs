namespace Model.DTO.User
{
    public class UserEdit
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
        /// 是否启用 (0=未启用 1=启用)
        /// </summary>
        public bool IsEnable { get; set; }

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
