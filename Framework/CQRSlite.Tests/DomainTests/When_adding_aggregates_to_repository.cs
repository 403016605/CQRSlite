﻿using CQRSlite.Domain;
using CQRSlite.Eventing;
using CQRSlite.Tests.TestSubstitutes;
using NUnit.Framework;

namespace CQRSlite.Tests.DomainTests
{
    [TestFixture]
    public class When_adding_aggregates_to_repository
    {
        private Repository<TestSnapshotAggregate> _rep;

        [SetUp]
        public void SetUp()
        {
            var eventStore = new TestEventStore();
            var eventPublisher = new TestEventPublisher();
            var snapshotStore = new NullSnapshotStore();
            var session = new Session(eventStore, snapshotStore, eventPublisher);
            _rep = new Repository<TestSnapshotAggregate>(session, eventStore, snapshotStore);
        }

        [Test]
        public void Should_throw_if_different_object_with_tracked_guid_is_added()
        {
            var aggregate = new TestSnapshotAggregate();
            var aggregate2 = new TestSnapshotAggregate();
            aggregate2.SetId(aggregate.Id);
            _rep.Add(aggregate);
            Assert.Throws<TrackedAggregateAddedException>(() => _rep.Add(aggregate2));
        }

        [Test]
        public void Should_not_throw_if_object_already_tracked()
        {
            var aggregate = new TestSnapshotAggregate();
            _rep.Add(aggregate);
            _rep.Add(aggregate);
        }
    }
}