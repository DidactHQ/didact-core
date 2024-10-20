using System;

namespace DidactCore.Entities
{
    public class LibrarySource
    {
        public long LibrarySourceId { get; set; }

        public int LibrarySourceTypeId { get; set; }

        public long LibraryId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime LastUpdated { get; set; }

        public string LastUpdatedBy { get; set; } = null!;

        public bool Active { get; set; }

        public byte[] RowVersion { get; set; } = null!;
    }
}
