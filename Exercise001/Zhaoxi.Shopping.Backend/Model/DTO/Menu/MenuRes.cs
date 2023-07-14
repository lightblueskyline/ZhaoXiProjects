using SqlSugar;

namespace Model.DTO.Menu
{
    public class MenuRes
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsTreeKey = true)]
        public string ID { get; set; }

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
        /// 父级
        /// </summary>
        public string ParentID { get; set; }

        public string ParentName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 是否启用 (0=未启用 1=启用)
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<MenuRes> Children { get; set; }
    }
}
