namespace Common.Core.Cqrs.Queries
{
    public class PaginalQueryBase<T> : IQuery<T>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; } 
        public string OrderBy { get; set; }
    }
}
