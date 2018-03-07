using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace VimDemo.Web.AnnotationInjection
{
    public class DataAnnotationsModelValidatorEx : DataAnnotationsModelValidator
    {
        private readonly bool _shouldHotwireValidationContextServiceProviderToDependencyResolver;

        public DataAnnotationsModelValidatorEx(
            ModelMetadata metadata, ControllerContext context, ValidationAttribute attribute,
            bool shouldHotwireValidationContextServiceProviderToDependencyResolver = false)
            : base(metadata, context, attribute)
        {
            _shouldHotwireValidationContextServiceProviderToDependencyResolver =
                shouldHotwireValidationContextServiceProviderToDependencyResolver;
        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            // Per the WCF RIA Services team, instance can never be null (if you have
            // no parent, you pass yourself for the "instance" parameter).
            var memberName = Metadata.PropertyName ?? Metadata.ModelType.Name;
            var context = new ValidationContext(container ?? Metadata.Model)
            {
                DisplayName = Metadata.GetDisplayName(),
                MemberName = memberName
            };

#if !THERE_IS_A_BETTER_EXTENSION_POINT
            if (_shouldHotwireValidationContextServiceProviderToDependencyResolver
                && Attribute.RequiresValidationContext)
                context.InitializeServiceProvider(DependencyResolver.Current.GetService);
#endif
            var result = Attribute.GetValidationResult(Metadata.Model, context);
            if (result != ValidationResult.Success)
            {
                // ModelValidationResult.MemberName is used by invoking validators (such as ModelValidator) to
                // construct the ModelKey for ModelStateDictionary. When validating at type level we want to append the
                // returned MemberNames if specified (e.g. person.Address.FirstName). For property validation, the
                // ModelKey can be constructed using the ModelMetadata and we should ignore MemberName (we don't want
                // (person.Name.Name). However the invoking validator does not have a way to distinguish between these two
                // cases. Consequently we'll only set MemberName if this validation returns a MemberName that is different
                // from the property being validated.

                var errorMemberName = result.MemberNames.FirstOrDefault();
                if (string.Equals(errorMemberName, memberName, StringComparison.Ordinal))
                {
                    errorMemberName = null;
                }

                var validationResult = new ModelValidationResult
                {
                    Message = result.ErrorMessage,
                    MemberName = errorMemberName
                };

                return new[] {validationResult};
            }

            return Enumerable.Empty<ModelValidationResult>();
        }
    }
}