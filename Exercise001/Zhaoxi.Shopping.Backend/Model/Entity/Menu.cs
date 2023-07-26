﻿using Model.Common;
using SqlSugar;

namespace Model.Entity
{
    /// <summary>
    /// 菜单表
    /// </summary>
    public class Menu : IEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Name { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 500)]
        public string Index { get; set; }

        /// <summary>
        /// 项目中的页面路径
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string FilePath { get; set; }

        /// <summary>
        /// 父级
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string ParentID { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int Order { get; set; }

        /// <summary>
        /// 是否启用 (0=未启用 1=启用)
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Icon { get; set; }
    }
}