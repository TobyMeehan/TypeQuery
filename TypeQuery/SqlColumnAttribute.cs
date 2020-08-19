using System;
using System.Collections.Generic;
using System.Text;

namespace TypeQuery
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class SqlColumnAttribute : Attribute
    {
        public SqlColumnAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
