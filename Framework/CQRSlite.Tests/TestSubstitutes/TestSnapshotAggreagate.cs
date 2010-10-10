﻿using CQRSlite.Domain;

namespace CQRSlite.Tests.TestSubstitutes
{
    public class TestSnapshotAggreagate : SnapshotAggregateRoot<TestSnapshotAggreagateSnapshot>
    {
        public bool Restored { get; set; }
        public bool Loaded { get; set; }

        protected override TestSnapshotAggreagateSnapshot CreateSnapshot()
        {
            return new TestSnapshotAggreagateSnapshot();
        }

        protected override void RestoreFromSnapshot(TestSnapshotAggreagateSnapshot snapshot)
        {
            Restored = true;
        }

        private void Apply(TestAggregateDidSomething e)
        {
            Loaded = true;
        }
    }

    public class TestSnapshotAggreagateSnapshot : Snapshot
    {
        public TestSnapshotAggreagateSnapshot()
        {
            Version = 2;
        }
    }
}
