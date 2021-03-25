using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlockToDB.Application
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RemoteAppAttribute : ValidationAttribute, IClientValidatable
    {
        public string Area { get; set; }
        private string _action { get; set; }
        private string _controller { get; set; }
        private List<string> _parameters { get; set; }

        public RemoteAppAttribute(string action, string controller, params string[] additionalParameters)
        {
            _action = action;
            _controller = controller;
            _parameters = new List<string>();
            foreach (string param in additionalParameters)
            {
                _parameters.Add(param);
            }


        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata,
            ControllerContext context)
        {
            List<object> parameters = new List<object>();
            parameters.Add(new { Name = metadata.PropertyName, IsMain = true });
            foreach (string param in _parameters)
            {
                parameters.Add(new { Name = param, IsMain = false });
            }
            var urlHelper = new UrlHelper(context.RequestContext);
            string url = string.Empty;
            if (Area != null)
            {
                url = urlHelper.Action(_action, _controller, new { area = Area });
            }
            else
            {
                url = urlHelper.Action(_action, _controller, new { });
            }
            var rule = new ModelClientValidationRule();

            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationParameters.Add("validationcallback", "remoteApp");
            rule.ValidationParameters.Add("url", url);
            rule.ValidationParameters.Add("parameters", parameters);
            rule.ValidationType = "custom";
            yield return rule;
        }
    }
}
