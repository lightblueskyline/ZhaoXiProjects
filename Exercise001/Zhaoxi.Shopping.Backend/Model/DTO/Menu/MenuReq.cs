namespace Model.DTO.Menu
{
    public class MenuReq
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// 项目中的页面路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
