using System;

namespace DidactCore.Entities
{
    public class ExecutionVersion
    {
        public long ExecutionVersionId { get; set; }

        public long LibraryVersionId { get; set; }

        public long FlowVersionId { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime LastUpdated { get; set; }

        public string LastUpdatedBy { get; set; } = null!;

        public bool Active { get; set; }

        public byte[] RowVersion { get; set; } = null!;
    }
}
