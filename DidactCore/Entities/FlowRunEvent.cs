﻿using System;

namespace DidactCore.Entities
{
    public class FlowRunEvent
    {
        public long FlowRunEventId { get; set; }

        public long FlowRunId { get; set; }

        public int FlowRunEventTypeId { get; set; }

        public int OrganizationId { get; set; }

        public DateTime OccurredAt { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime LastUpdated { get; set; }

        public string LastUpdatedBy { get; set; } = null!;

        public byte[] RowVersion { get; set; } = null!;

        public virtual FlowRun FlowRun { get; set; } = null!;

        public virtual FlowRunEventType FlowRunEventType { get; set; } = null!;

        public virtual Organization Organization { get; set; } = null!;
    }
}
