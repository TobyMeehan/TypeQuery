using System;
using System.Collections.Generic;
using System.Text;

namespace TypeQuery
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class SqlTableAttribute : Attribute
    {
        public SqlTableAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
