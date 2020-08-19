using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace TypeQuery.Extensions
{
    public static class ReflectionExtensions
    {
        public static bool HasAttribute(this MemberInfo memberInfo, Type attributeType, out Attribute attribute)
        {
            attribute = memberInfo.GetCustomAttribute(attributeType);

            return attribute != null;
        }

        public static bool HasAttribute<T>(this MemberInfo memberInfo, out T attribute) where T : Attribute
        {
            attribute = memberInfo.GetCustomAttribute<T>();

            return attribute != null;
        }

        public static string GetColumnName(this PropertyInfo property)
        {
            if (property.HasAttribute(out SqlColumnAttribute columnAttribute))
            {
                return columnAttribute.Name;
            }

            if (property.HasAttribute(out DataMemberAttribute dataMemberAttribute))
            {
                return dataMemberAttribute.Name;
            }

            return property.Name;
        }

        public static string GetTableName(this Type type)
        {
            if (type.HasAttribute(out SqlTableAttribute tableAttribute))
            {
                return tableAttribute.Name;
            }

            if (type.HasAttribute(out DataContractAttribute dataContractAttribute))
            {
                return dataContractAttribute.Name;
            }

            return type.Name;
        }
    }
}
