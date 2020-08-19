using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TypeQuery.Extensions;

namespace TypeQuery.Collections
{
    public class ParameterDictionary : Dictionary<string, object>
    {
        public ParameterDictionary() { }

        public ParameterDictionary(IEnumerable<KeyValuePair<string, object>> dictionary)
        {
            foreach (var item in dictionary)
            {
                Add(item.Key, item.Value);
            }
        }
    }
}
