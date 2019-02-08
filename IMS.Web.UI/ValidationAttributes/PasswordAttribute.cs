using Compaid.Common.Ioc.TinyIoc;
using IMS.Business.Presenters;
using IMS.Business.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace IMS.Web.UI.ValidationAttributes
{
    public class MeetsPasswordRulesAttribute : ValidationAttribute, IClientValidatable
    {
        private IPasswordPresenter _passwordPresenter;

        public int MinCharacters { get; set; }
        public int MinSpecialCharacter { get; set; }
        public int MinUppercase { get; set; }
        public int MinLowercase { get; set; }
        public int MinNumbers { get; set; }

        public MeetsPasswordRulesAttribute() 
            : base(ErrorMessages.PasswordDoesNotMeetRequirements)
        {
            _passwordPresenter = TinyIocContainer.Current.Resolve<IPasswordPresenter>();
            SetPasswordRequirements();
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, 
                name,
                MinCharacters.ToString(), 
                MinSpecialCharacter.ToString(), 
                MinUppercase.ToString(), 
                MinLowercase.ToString(), 
                MinNumbers.ToString());
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;

            if(password == null)
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

            if (password.Length < MinCharacters)
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

            var successfulValidations = 0;

            if (password.Where(i => char.IsDigit(i)).Count() >= MinNumbers)
                ++successfulValidations;
            if (password.Where(i => char.IsUpper(i)).Count() >= MinUppercase)
                ++successfulValidations;
            if (password.Where(i => char.IsLower(i)).Count() >= MinLowercase)
                ++successfulValidations;
            if (password.Where(i => !char.IsLetterOrDigit(i)).Count() >= MinSpecialCharacter)
                ++successfulValidations;

            if (successfulValidations >= 3)
                return ValidationResult.Success;

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationType = "password";
            rule.ValidationParameters.Add("mincharacters", MinCharacters);
            rule.ValidationParameters.Add("minlowercase", MinLowercase);
            rule.ValidationParameters.Add("minnumbers", MinNumbers);
            rule.ValidationParameters.Add("minspecialcharacters", MinSpecialCharacter);
            rule.ValidationParameters.Add("minuppercase", MinUppercase);
            yield return rule;
        }

        private void SetPasswordRequirements()
        {
            var rules = _passwordPresenter.GetPasswordRules();
            MinCharacters = rules.NumLength;
            MinLowercase = rules.NumLowerCase;
            MinNumbers = rules.NumDigits;
            MinSpecialCharacter = rules.NumSpecialCharacter;
            MinUppercase = rules.NumUpperCase;
        }
    }
}