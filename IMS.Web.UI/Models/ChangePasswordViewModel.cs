using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMS.Web.UI.ValidationAttributes;
using IMS.Business.ViewModel;

namespace IMS.Web.UI.Models
{
    public class ChangePasswordViewModel : ChangePasswordModel
    {
        [MeetsPasswordRules]
        public override string NewPassword { get; set; }

        [MeetsPasswordRules]
        public override string ConfirmPassword { get; set; }

        [MeetsPasswordRules]
        public override string Password { get; set; }
    }
}