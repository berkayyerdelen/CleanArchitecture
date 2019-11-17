using System;
using Entities.Abstract;

namespace Entities
{
    public class Audit : BaseEntity<int>
    {
        public string TableName { get; set; }
        public DateTime DateTime { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
    }
}