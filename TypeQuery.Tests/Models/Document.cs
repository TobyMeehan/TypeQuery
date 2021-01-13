using System;
using System.Collections.Generic;
using System.Text;

namespace TypeQuery.Tests.Models
{
    [SqlTable("documents")]
    public class Document
    {
        [SqlColumn("id")]
        public string Id { get; set; }
        [SqlColumn("title")]
        public string Title { get; set; }
        [SqlColumn("body")]
        public string Body { get; set; }
    }
}
