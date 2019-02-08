using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compaid.Common.Ioc.TinyIoc;
using IMS.Business.Presenters;
using IMS.Business.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IMS.Web.UI.ValidationAttributes
{
    public class TestablePasswordAttribute : MeetsPasswordRulesAttribute
    {
        public new ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return base.IsValid(value, validationContext);
        }
    }
}