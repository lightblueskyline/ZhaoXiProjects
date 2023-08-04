using System.ComponentModel;

namespace Model.DTO.Role
{
    public class RoleRequest
    {
        public string Name { get; set; }

        [DefaultValue("M_0v0_M")]
        public string Description { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
