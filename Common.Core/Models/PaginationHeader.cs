namespace Common.Core.Models
{
    public class PaginationHeader
    {
        public int? Take { get; set; }
        public int? Skip { get; set; }
        public string OrderBy { get; set; }
    }
}
