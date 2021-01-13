using System;
using System.Collections.Generic;
using System.Text;

namespace TypeQuery.Tests.Models
{
    [SqlTable("comments")]
    public class Comment
    {
        [SqlColumn("id")]
        public string Id { get; set; }
        [SqlColumn("userid")]
        public string UserId { get; set; }
        [SqlColumn("documentid")]
        public string DocumentId { get; set; }
        [SqlColumn("sent")]
        public DateTime Sent { get; set; }
        [SqlColumn("body")]
        public string Body { get; set; }
    }
}
