using BlockToDB.Resources.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlockToDB.Application
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RequiredShortAttribute : RequiredAttribute, IClientValidatable
    {
        public RequiredShortAttribute() : base()
        {
            ErrorMessageResourceType = typeof(ErrorResource);
            ErrorMessageResourceName = "RequiredShort";
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRequiredRule(FormatErrorMessage(metadata.GetDisplayName()));
        }
    }
}