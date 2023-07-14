﻿namespace Model.DTO.Menu
{
    public class MenuAdd
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
        /// 父级
        /// </summary>
        public string ParentID { get; set; }

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
    }
}
