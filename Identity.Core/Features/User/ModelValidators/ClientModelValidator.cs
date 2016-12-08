using FluentValidation;
using Identity.Core.Features.User.Models;

namespace Identity.Core.Features.User.ModelValidators
{
    using Common.Core.Constants;
    using Domain;
    using Infrastructure.Models.Identity;
    using Microsoft.Extensions.Localization;
    using Resources;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    public class ClientModelValidator : AbstractValidator<ClientModel>
    {
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IdentityContext context;

        public ClientModelValidator(
             IStringLocalizer<SharedResources> localizer,
             IdentityContext context)
        {
            this.localizer = localizer;
            this.context = context;
            SetValidationRules();
        }

        public void SetValidationRules()
        {
            RuleFor(it => it.FirstName)
                .NotEmpty().WithMessage(localizer["First_Name_Must_Be_Specified"]);

            RuleFor(it => it.LastName)
                .NotEmpty().WithMessage(localizer["Last_Name_Must_Be_Specified"]);

            RuleFor(it => it.MiddleName)
                .NotEmpty().WithMessage(localizer["Middle_Name_Must_Be_Specified"]);

            RuleFor(it => it.BirthDate)
                .NotNull().WithMessage(localizer["Birth_Date_Must_Be_Specified"]);

            RuleFor(it => it.PassportSeria)
                .NotEmpty().WithMessage(localizer["Passport_Seria_Must_Be_Specified"]);

            RuleFor(it => it.PassportNo)
                .NotEmpty().WithMessage(localizer["Passport_No_Must_Be_Specified"])
                .Matches(ValidationPatterns.PassportNoPattern).WithMessage("pattern");

            RuleFor(it => it.IssuedBy)
                .NotEmpty().WithMessage(localizer["Issued_By_Must_Be_Specified"]);

            RuleFor(it => it.IssueDate)
                .NotNull().WithMessage(localizer["Issue_Date_Must_Be_Specified"]);

            RuleFor(it => it.IdentificationNo)
                .NotEmpty().WithMessage(localizer["Identification_No_Must_Be_Specified"]);

            RuleFor(it => it.PlaceOfBirth)
                .NotEmpty().WithMessage(localizer["Place_Of_Birth_Must_Be_Specified"]);

            RuleFor(it => it.ActualResidenceCityId)
                .NotNull().WithMessage(localizer["Actual_Residence_City_Must_Be_Specified"]);

            RuleFor(it => it.ActualResidenceAddress)
                .NotEmpty().WithMessage(localizer["Actual_Residence_Address_Must_Be_Specified"]);

            RuleFor(it => it.HomePhone)
                .NotEmpty().WithMessage(localizer["Home_Phone_Must_Be_Specified"]);

            RuleFor(it => it.MobilePhone)
                .NotEmpty().WithMessage(localizer["Mobile_Phone_Must_Be_Specified"]);

            RuleFor(it => it.Email)
                .NotEmpty().WithMessage(localizer["Email_Must_Be_Specified"]);

            RuleFor(it => it.WorkPlace)
                .NotEmpty().WithMessage(localizer["Work_Place_Must_Be_Specified"]);

            RuleFor(it => it.WorkPosition)
                .NotEmpty().WithMessage(localizer["Work_Position_Must_Be_Specified"]);

            RuleFor(it => it.RegistrationCityId)
                .NotNull().WithMessage(localizer["Registration_City_Must_Be_Specified"]);

            RuleFor(it => it.MaritalStatusId)
                .NotNull().WithMessage(localizer["Marital_Status_Must_Be_Specified"]);

            RuleFor(it => it.CitizenshipCountryId)
                .NotNull().WithMessage(localizer["Citizenship_Country_Must_Be_Specified"]);

            RuleFor(it => it.DisabilityId)
                .NotNull().WithMessage(localizer["Disability_Must_Be_Specified"]);

            RuleFor(it => it.IsPensioner)
                .NotNull().WithMessage(localizer["Pensioner_Status_Must_Be_Specified"]);

            RuleFor(it => it.MonthlyIncome)
                .NotNull().WithMessage(localizer["Monthly_Income_Must_Be_Specified"]);
            
            RuleFor(it => it).MustAsync(UniquePassport).WithMessage(localizer["Client_With_The_Specified_Passport_Number_And_Seria_Is_Already_Registered"]);
            RuleFor(it => it).MustAsync(UniqueFullName).WithMessage(localizer["Client_With_The_Specified_Full_Name_Is_Already_Registered"]);
            RuleFor(it => it).MustAsync(UniqueIdentificationNo).WithMessage(localizer["Client_With_The_Specified_Identification_No_Is_Already_Registered"]);
        }

        private async Task<bool> UniqueIdentificationNo(ClientModel clientModel, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(clientModel.IdentificationNo))
            {
                return true;
            }

            return await UniquePropertiesCombination(it =>
                it.Id != clientModel.Id &&
                it.IdentificationNo == clientModel.IdentificationNo);
        }

        private async Task<bool> UniqueFullName(ClientModel clientModel, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(clientModel.FirstName) || 
                string.IsNullOrWhiteSpace(clientModel.LastName) ||
                string.IsNullOrWhiteSpace(clientModel.MiddleName))
            {   
                return true;
            }

            return await UniquePropertiesCombination(it =>
                it.Id != clientModel.Id &&
                it.FirstName == clientModel.FirstName && 
                it.LastName == clientModel.LastName &&
                it.MiddleName == clientModel.MiddleName);
        }

        private async Task<bool> UniquePassport(ClientModel clientModel, CancellationToken token)
        {
            if(string.IsNullOrWhiteSpace(clientModel.PassportNo) || string.IsNullOrWhiteSpace(clientModel.PassportSeria))
            {
                return true;
            }

            return await UniquePropertiesCombination(it => 
                it.Id != clientModel.Id && 
                it.PassportNo == clientModel.PassportNo && 
                it.PassportSeria == clientModel.PassportSeria);
        }

        private Task<bool> UniquePropertiesCombination(Expression<Func<Client, bool>> predicate)
        {
            var client = context.Clients.Where(predicate).FirstOrDefault();
            if(client == null)
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
