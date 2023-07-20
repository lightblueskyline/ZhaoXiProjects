namespace Model.DTO.User
{
    public class UserReq
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户类型 (0=超级管理员 1=普通用户)
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 是否启用 (0=未启用 1=启用)
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
