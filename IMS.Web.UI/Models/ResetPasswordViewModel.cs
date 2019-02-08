using IMS.Business.ViewModel;
using IMS.Web.UI.ValidationAttributes;
using System;

namespace IMS.Web.UI.Models
{
    public class ResetPasswordViewModel : ResetPasswordModel
    {
        [MeetsPasswordRules]
        public override string NewPassword { get; set; }

        [MeetsPasswordRules]
        public override string ConfirmPassword { get; set; }

        public override Guid ResetPasswordToken { get; set; }
    }
}