﻿using System;

namespace DidactCore.Entities
{
    public class LibrarySourceType
    {
        public int LibrarySourceTypeId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime LastUpdated { get; set; }

        public string LastUpdatedBy { get; set; } = null!;

        public bool Active { get; set; }

        public byte[] RowVersion { get; set; } = null!;
    }
}
