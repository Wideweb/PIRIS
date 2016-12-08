using Common.Core.Models;
using FluentValidation.Attributes;
using Identity.Core.Features.Finance.ModelValidators;
using Identity.Core.Features.Questionnaire.Attributes;

namespace Identity.Core.Features.Finance.Models
{
    [Validator(typeof(ClientAccountValidator))]
    public class ClientAccountModel : BaseModel
    {
        [DropDownQuestion(Label = "Client", OptionsUrl = "/api/Lookup/GetClientLookups", Required = true)]
        public long ClientId { get; set; }
        
        public long? MasterAccountId { get; set; }

        [DropDownQuestion(Label = "Account Plan", OptionsUrl = "/api/Lookup/GetAccountPlanLookups", Required = true)]
        public long AccountPlanId { get; set; }

        [TextBoxQuestion(Label = "Amount", Required = true, Pattern = @"^\d+(\.\d+)?$")]
        public decimal Amount { get; set; }

        [DropDownQuestion(Label = "Currency", OptionsUrl = "/api/Lookup/GetCurrencyLookups", Required = true)]
        public long CurrencyId { get; set; }
    }
}
