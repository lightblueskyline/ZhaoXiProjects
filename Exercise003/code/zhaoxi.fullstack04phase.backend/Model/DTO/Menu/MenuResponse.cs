using SqlSugar;

namespace Model.DTO.Menu
{
    public class MenuResponse
    {
        [SugarColumn(IsTreeKey = true)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Index { get; set; }

        public string FilePath { get; set; }

        public string ParentId { get; set; }

        public string ParentName { get; set; }

        public int Order { get; set; }

        public bool IsEnable { get; set; }

        public string Icon { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 子级
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<MenuResponse> Children { get; set; }
    }
}
