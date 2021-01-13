using System;
using System.Collections.Generic;
using System.Text;

namespace TypeQuery.Tests.Models
{
    [SqlTable("users")]
    public class User
    {
        [SqlColumn("id")]
        public string Id { get; set; }
        [SqlColumn("name")]
        public string Name { get; set; }
    }
}
