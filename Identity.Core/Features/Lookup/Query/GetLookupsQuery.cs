using Common.Core.Cqrs.Queries;
using Identity.Core.Features.Lookup.Models;
using System.Linq;

namespace Identity.Core.Features.Lookup.Query
{
    public class GetLookupsQuery : PaginalQueryBase<IQueryable<LookupModel>>
    {
        public string TableName { get; set; }
    }
}
