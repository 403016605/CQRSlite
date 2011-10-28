﻿using System;
using CQRSlite.Domain;
using CQRSlite.Eventing;
using CQRSlite.Infrastructure;
using CQRSlite.Tests.TestSubstitutes;
using NUnit.Framework;

namespace CQRSlite.Tests.DomainTests
{
	[TestFixture]
    public class When_getting_an_aggregate
    {

        private Repository<TestAggregate> _rep;
		
		[SetUp]
        public void Setup()
        {
            var eventStore = new TestEventStore();
            var testEventPublisher = new TestEventPublisher();
            var snapshotStore = new NullSnapshotStore();
            var snapshotStrategy = new DefaultSnapshotStrategy();
            var session = new Session(eventStore, snapshotStore, testEventPublisher, snapshotStrategy);
            _rep = new Repository<TestAggregate>(session, eventStore, snapshotStore, snapshotStrategy);
        }

        [Test]
        public void Should_get_aggregate_from_eventstore()
        {
            var aggregate = _rep.Get(Guid.NewGuid());
            Assert.NotNull(aggregate);
        }

        [Test]
        public void Should_apply_events()
        {
            var aggregate = _rep.Get(Guid.NewGuid());
            Assert.AreEqual(2,aggregate.I);
        }

        [Test]
        public void Should_fail_if_aggregate_do_not_exist()
        {
            Assert.Throws<AggregateNotFoundException>(() => _rep.Get(Guid.Empty));
        }

        [Test]
	    public void Should_track_changes()
	    {
            var agg = new TestAggregate(Guid.NewGuid());
            _rep.Add(agg);
	        var aggregate = _rep.Get(agg.Id);
            Assert.AreEqual(agg,aggregate);
	    }

        [Test]
        public void Should_get_from_session_if_tracked()
        {
            var id = Guid.NewGuid();
            var aggregate = _rep.Get(id);
            var aggregate2 = _rep.Get(id);

            Assert.AreEqual(aggregate, aggregate2);
        }

        [Test]
        public void Should_throw_concurrency_exception_if_tracked()
        {
            var id = Guid.NewGuid();
            _rep.Get(id);

            Assert.Throws<ConcurrencyException>(() => _rep.Get(id, 100));
        }

        [Test]
        public void Should_get_correct_version()
        {
            var id = Guid.NewGuid();
            var aggregate = _rep.Get(id);

            Assert.AreEqual(3,aggregate.Version);
        }

        [Test]
        public void Should_throw_concurrency_exception()
        {
            var id = Guid.NewGuid();
            Assert.Throws<ConcurrencyException>(() => _rep.Get(id,1));
        }

    }
}