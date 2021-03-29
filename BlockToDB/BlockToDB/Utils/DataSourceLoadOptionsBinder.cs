using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.Helpers;
using System.Web.Mvc;

namespace BlockToDB.Utils
{
    [ModelBinder(typeof(DataSourceLoadOptionsBinder))]
    public class DataSourceLoadOptions : DataSourceLoadOptionsBase { }

    internal class DataSourceLoadOptionsBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var loadOptions = new DataSourceLoadOptions();
            DataSourceLoadOptionsParser.Parse(loadOptions, key => bindingContext.ValueProvider.GetValue(key)?.AttemptedValue);
            return loadOptions;
        }
    }
}