using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace BlockToDB.Utils
{
    public class UtilMethods
    {
        public static bool IsList(object o)
        {
            if (o == null) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }


        public static TypeBuilder GetTypeBuilder(string namespaceName, string typeName)
        {
            var an = new AssemblyName(namespaceName);
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
            TypeBuilder tb = moduleBuilder.DefineType(namespaceName + "." + typeName,
                    TypeAttributes.Public |
                    TypeAttributes.Class |
                    TypeAttributes.AutoClass |
                    TypeAttributes.AnsiClass |
                    TypeAttributes.BeforeFieldInit |
                    TypeAttributes.AutoLayout,
                    null);           
            return tb;
        }

        public static FieldBuilder CreateProperty(TypeBuilder tb, string propertyName, Type propertyType, string columnNameAttribute = null, bool isKey = false)
        {
            FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr =
                tb.DefineMethod("set_" + propertyName,
                  MethodAttributes.Public |
                  MethodAttributes.SpecialName |
                  MethodAttributes.HideBySig,
                  null, new[] { propertyType });

            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropMthdBldr);
            propertyBuilder.SetSetMethod(setPropMthdBldr);
            if (!columnNameAttribute.IsNullOrEmpty())
            {
                var attrCtorParams = new Type[] { typeof(string) };
                var attrCtorInfo = typeof(ColumnAttribute).GetConstructor(attrCtorParams);
                var attrBuilder = new CustomAttributeBuilder(attrCtorInfo, new object[] { columnNameAttribute });
                propertyBuilder.SetCustomAttribute(attrBuilder);
            }

            if (isKey)
            {
                var attrCtorParams = new Type[] { };
                var attrCtorInfo = typeof(KeyAttribute).GetConstructor(attrCtorParams);
                var attrBuilder = new CustomAttributeBuilder(attrCtorInfo, new object[] { });
                propertyBuilder.SetCustomAttribute(attrBuilder);
            }

            return fieldBuilder;
        }

        public static void CreateConstructor(TypeBuilder typeBuilder, List<FieldBuilder> parameters = null)
        {
            if (parameters == null)
                parameters = new List<FieldBuilder>();
            List<Type> types = parameters.Select(x => x.FieldType).ToList();
            ConstructorBuilder myConstructorBuilder = typeBuilder.DefineConstructor(
                     MethodAttributes.Public,
                     CallingConventions.Standard,
                     types.ToArray());
            ILGenerator myConstructorIL = myConstructorBuilder.GetILGenerator();
            foreach (var item in parameters)
            {
                myConstructorIL.Emit(OpCodes.Ldarg_0);
                myConstructorIL.Emit(OpCodes.Ldarg_1);
                myConstructorIL.Emit(OpCodes.Stfld, item);
            }
            myConstructorIL.Emit(OpCodes.Ret);
        }
    }
}
