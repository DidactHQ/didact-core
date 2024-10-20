using System;

namespace DidactCore.Entities
{
    public class LibrarySourceLocalFileSystem
    {
        public long LibrarySourceLocalFileSystemId { get; set; }

        public long LibrarySourceId { get; set; }

        public int OrganizationId { get; set; }

        public string FolderPath { get; set; } = null!;

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime LastUpdated { get; set; }

        public string LastUpdatedBy { get; set; } = null!;

        public bool Active { get; set; }

        public byte[] RowVersion { get; set; } = null!;
    }
}
