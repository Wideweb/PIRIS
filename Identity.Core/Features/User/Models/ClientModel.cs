using Common.Core.Constants;
using Common.Core.Models;
using FluentValidation.Attributes;
using Identity.Core.Features.Questionnaire.Attributes;
using Identity.Core.Features.User.ModelValidators;
using System;

namespace Identity.Core.Features.User.Models
{
    [Validator(typeof(ClientModelValidator))]
    public class ClientModel : BaseModel
    {
        [TextBoxQuestion(Label = "First Name", Required = true, Pattern = @"^[a-zA-Z]{1,20}$")]
        public string FirstName { get; set; }

        [TextBoxQuestion(Label = "Last Name", Required = true, Pattern = @"^[a-zA-Z]{1,20}$")]
        public string LastName { get; set; }

        [TextBoxQuestion(Label = "Middle Name", Required = true, Pattern = @"^[a-zA-Z]{1,20}$")]
        public string MiddleName { get; set; }

        [DatePickerQuestion(Label = "Birth Date", Required = true)]
        public DateTime? BirthDate { get; set; }

        [TextBoxQuestion(Label = "Passport Seria", Required = true)]
        public string PassportSeria { get; set; }

        [TextBoxQuestion(Label = "Passport No", Required = true, Pattern = ValidationPatterns.PassportNoPattern)]
        public string PassportNo { get; set; }

        [TextBoxQuestion(Label = "Issued By", Required = true)]
        public string IssuedBy { get; set; }

        [DatePickerQuestion(Label = "Issued Date", Required = true)]
        public DateTime? IssueDate { get; set; }

        [TextBoxQuestion(Label = "Identification No", Required = true)]
        public string IdentificationNo { get; set; }

        [TextBoxQuestion(Label = "Place Of Birth", Required = true)]
        public string PlaceOfBirth { get; set; }

        [DropDownQuestion(Label = "Actual Residence City", OptionsUrl = "/api/Lookup/GetCityLookups", Required = true)]
        public long? ActualResidenceCityId { get; set; }

        [TextBoxQuestion(Label = "Actual Residence Address", Required = true)]
        public string ActualResidenceAddress { get; set; }

        [TextBoxQuestion(Label = "Home Phone", Required = true)]
        public string HomePhone { get; set; }

        [TextBoxQuestion(Label = "Mobile Phone", Required = true)]
        public string MobilePhone { get; set; }

        [TextBoxQuestion(Label = "Email", Required = true)]
        public string Email { get; set; }

        [TextBoxQuestion(Label = "Work Place", Required = true)]
        public string WorkPlace { get; set; }

        [TextBoxQuestion(Label = "Work Position", Required = true)]
        public string WorkPosition { get; set; }

        [DropDownQuestion(Label = "Registration City", OptionsUrl = "/api/Lookup/GetCityLookups", Required = true)]
        public long? RegistrationCityId { get; set; }

        [DropDownQuestion(Label = "Marital Status", OptionsUrl = "/api/Lookup/GetMaritalStatusLookups", Required = true)]
        public long? MaritalStatusId { get; set; }

        [DropDownQuestion(Label = "Citizenship Country", OptionsUrl = "/api/Lookup/GetCountryLookups", Required = true)]
        public long? CitizenshipCountryId { get; set; }

        [DropDownQuestion(Label = "Disability", OptionsUrl = "/api/Lookup/GetDisabilityLookups", Required = true)]
        public long? DisabilityId { get; set; }

        [CheckBoxQuestion(Label = "Is Pensioner", DefaultValue = false)]
        public bool IsPensioner { get; set; }

        [TextBoxQuestion(Label = "Monthly Income", Required = true, Pattern = @"^\d+(\.\d+)?$")]
        public decimal? MonthlyIncome { get; set; }
    }
}
