﻿using System;
using CQRSlite.Domain;
using CQRSlite.Eventing;
using CQRSlite.Infrastructure;
using CQRSlite.Tests.TestSubstitutes;
using NUnit.Framework;

namespace CQRSlite.Tests.DomainTests
{
	[TestFixture]
    public class When_saving_a_snapshotable_aggregate_for_each_change
    {
        private TestInMemorySnapshotStore _snapshotStore;
        private Repository<TestSnapshotAggregate> _rep;

		[SetUp]
        public void Setup()        
		{
            IEventStore eventStore = new TestInMemoryEventStore();
            var eventpubliser = new TestEventPublisher();
            _snapshotStore = new TestInMemorySnapshotStore();
            var snapshotStrategy = new DefaultSnapshotStrategy();
            var session = new Session(eventStore, _snapshotStore, eventpubliser, snapshotStrategy);
            _rep = new Repository<TestSnapshotAggregate>(session, eventStore, _snapshotStore, snapshotStrategy);
            var aggregate = new TestSnapshotAggregate();

            for (int i = 0; i < 20; i++)
            {
                _rep.Add(aggregate);
                aggregate.DoSomething();
                session.Commit();
            }

        }

        [Test]
        public void Should_snapshot_15th_change()
        {
            Assert.AreEqual(15, _snapshotStore.SavedVersion);
        }

        [Test]
        public void Should_not_snapshot_first_event()
        {
            Assert.False(_snapshotStore.FirstSaved);
        }

        [Test]
        public void Should_get_aggregate_back_correct()
        {
            Assert.AreEqual(20, _rep.Get(Guid.Empty).Number);
        }
    }
}