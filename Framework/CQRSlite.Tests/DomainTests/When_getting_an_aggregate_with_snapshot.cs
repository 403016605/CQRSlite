﻿using System;
using CQRSlite.Domain;
using CQRSlite.Infrastructure;
using CQRSlite.Snapshotting;
using CQRSlite.Tests.TestSubstitutes;
using NUnit.Framework;

namespace CQRSlite.Tests.DomainTests
{
	[TestFixture]
    public class When_getting_an_aggregate_with_snapshot
    {
        private TestSnapshotAggregate _aggregate;

		[SetUp]
        public void Setup()
        {
            var eventStore = new TestEventStore();
            var eventPublisher = new TestEventPublisher();
            var snapshotStore = new TestSnapshotStore();
            var snapshotStrategy = new DefaultSnapshotStrategy();
            var session = new Session(new Repository(eventStore, eventPublisher, snapshotStore, snapshotStrategy));

            _aggregate = session.Get<TestSnapshotAggregate>(Guid.NewGuid());
        }

        [Test]
        public void Should_restore()
        {
            Assert.True(_aggregate.Restored);
        }
    }
}
