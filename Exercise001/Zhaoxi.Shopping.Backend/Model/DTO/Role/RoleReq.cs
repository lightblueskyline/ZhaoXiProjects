using System.ComponentModel;

namespace Model.DTO.Role
{
    public class RoleReq
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DefaultValue("Hello")]
        public string Description { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
