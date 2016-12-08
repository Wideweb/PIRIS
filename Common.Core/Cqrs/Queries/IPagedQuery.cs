namespace Common.Core.Cqrs.Queries
{
    public interface IPagedQuery
    {
        string OrderBy { get; set; }
        long? Take { get; set; }
        long? Skip { get; set; }
    }
}
