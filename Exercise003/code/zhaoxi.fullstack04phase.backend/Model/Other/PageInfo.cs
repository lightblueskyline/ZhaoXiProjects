namespace Model.Other
{
    /// <summary>
    /// 分页模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageInfo<T> where T : class, new()
    {
        public int Total { get; set; }

        public List<T> Data { get; set; }
    }
}
