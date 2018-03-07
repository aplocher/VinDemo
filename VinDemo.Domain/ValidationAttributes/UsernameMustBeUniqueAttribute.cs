using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace VinDemo.Domain.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class UsernameMustBeUniqueAttribute : ValidationAttribute , IClientValidatable
    {
        private string _memberIdField;
        private int _memberId;
        public UsernameMustBeUniqueAttribute(string memberIdField)
        {
            _memberIdField = memberIdField;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var property = validationContext.ObjectType.GetProperty(_memberIdField);
            //if (property == null)
            //{
            //    return new ValidationResult(string.Format("Unknown property: {0}", _memberIdField));
            //}
            //object memberIdObj = property.GetValue(validationContext.ObjectInstance, null);
            //_memberId = (int)memberIdObj;

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var mvr = new ModelClientValidationRule
            {
                ErrorMessage = "Username is already being used. Try another one",
                ValidationType = "validusername"
            };
            //mvr.ValidationParameters.Add("memberid", _memberId);
            return new[] { mvr };
        }
    }
}
