﻿using System;

namespace DidactCore.Entities
{
    public class FlowRunLogEvent
    {
        public long FlowRunLogEventId { get; set; }

        public long FlowRunEventId { get; set; }

        public int OrganizationId { get; set; }

        public string LogLevel { get; set; } = null!;

        public string Message { get; set; } = null!;

        public DateTime Timestamp { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime LastUpdated { get; set; }

        public string LastUpdatedBy { get; set; } = null!;

        public byte[] RowVersion { get; set; } = null!;

        public virtual FlowRunEvent FlowRunEvent { get; set; } = null!;

        public virtual Organization Organization { get; set; } = null!;
    }
}
