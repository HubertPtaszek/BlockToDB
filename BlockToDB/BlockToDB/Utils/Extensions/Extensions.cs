using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Web;
using System.Web.Mvc;

namespace BlockToDB.Utils
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this HtmlString st)
        {
            if (st == null)
                return true;
            return st == new HtmlString("");
        }

        public static PropertyInfo GetPropertyInfo(this Type type, string propertyName)
        {
            foreach (var property in type.GetProperties())
            {
                if (property.Name == propertyName)
                {
                    return property;
                }
                else if (!property.PropertyType.IsPrimitive && property.PropertyType != typeof(string))
                {
                    return GetPropertyInfo(property.PropertyType, propertyName);
                }
            }
            return null;
        }

        public static object GetPropertyValue(this object instance, string propertyName)
        {
            Type type = instance.GetType();
            foreach (var property in type.GetProperties())
            {
                var value = property.GetValue(instance, null);
                if (property.Name == propertyName)
                {
                    return value;
                }
                else if (!property.PropertyType.IsPrimitive && property.PropertyType != typeof(string))
                {
                    return GetPropertyValue(value, propertyName);
                }
            }
            return null;
        }

        public static string ToScriptStringSafe(this string ob)
        {
            if (ob == null)
                ob = string.Empty;
            return JavaScriptEncoder.Default.Encode(ob);
        }

        public static string ToScriptStringSafe(this object ob)
        {
            return ToScriptStringSafe(ob?.ToString());
        }

        public static string GetPropertyNameFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            return memberExpression.Member.Name;
        }
    }
}