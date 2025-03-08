﻿using System;

namespace DidactCore.Entities
{
    public class FifoQueueInbound
    {
        public long FifoQueueInboundId { get; set; }

        public long EnvironmentId { get; set; }

        public int FifoQueueId { get; set; }

        public long FlowRunId { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime LastUpdated { get; set; }

        public string LastUpdatedBy { get; set; } = null!;

        public byte[] RowVersion { get; set; } = null!;

        public virtual Environment Environment { get; set; } = null!;

        public virtual FifoQueue FifoQueue { get; set; } = null!;

        public virtual FlowRun FlowRun { get; set; } = null!;
    }
}
