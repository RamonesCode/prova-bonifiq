namespace ProvaPub.Models
{
    public abstract class PagedResponseBase<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
    }
}
