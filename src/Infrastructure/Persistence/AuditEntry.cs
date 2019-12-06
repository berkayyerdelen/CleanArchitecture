using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace Persistence
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry) 
            => Entry = entry;
        public EntityEntry Entry { get; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public Audit ToAudit()
        {
            var audit = new Audit()
            {
                TableName = TableName,
                DateTime = DateTime.UtcNow,
                KeyValues = JsonConvert.SerializeObject(KeyValues),
                OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues),
                NewValues= NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues)
            };
            return audit;
        }
    }
}