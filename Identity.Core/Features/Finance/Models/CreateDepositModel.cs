using Common.Core.Models;
using FluentValidation.Attributes;
using Identity.Core.Features.Finance.ModelValidators;
using System;

namespace Identity.Core.Features.Finance.Models
{
    [Validator(typeof(DepositModelValidator))]
    public class CreateDepositModel : BaseModel
    {
        public long? ClientId { get; set; }
        public long? MasterClientAccountId { get; set; }
        public DateTime? StartDate { get; set; }
        public decimal? Amount { get; set; }
        public long? DepositPlanId { get; set; }
        public long? DepositTypeId { get; set; }
    }
}
