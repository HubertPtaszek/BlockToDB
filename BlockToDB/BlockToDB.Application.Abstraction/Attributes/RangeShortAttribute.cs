using BlockToDB.Resources.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlockToDB.Application
{
    public class RangeShortAttribute : RangeAttribute, IClientValidatable
    {
        public RangeShortAttribute(double min, double max)
            : base(min, max)
        {
            base.ErrorMessage = ErrorResource.Range;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRangeRule(FormatErrorMessage(metadata.GetDisplayName()), Minimum, Maximum);
        }
    }
}
