﻿using System;
using SimpleCQRS.Domain;
using SimpleCQRS.Tests.TestSubstitutes;
using Xunit;

namespace SimpleCQRS.Tests.RepositoryTests
{
    public class WhenGettingAnAggregate
    {
        private TestAggregate _aggregate;

        public WhenGettingAnAggregate()
        {
            var eventStore = new TestEventStore();
            var rep = new Repository<TestAggregate>(eventStore);
            _aggregate = rep.GetById(Guid.NewGuid());
        }

        [Fact]
        public void ShouldGetAggreagateFromEventStore()
        {
            Assert.NotNull(_aggregate);
        }

        [Fact]
        public void ShouldApplyEvents()
        {
            Assert.Equal(2,_aggregate.I);
        }
    }
}