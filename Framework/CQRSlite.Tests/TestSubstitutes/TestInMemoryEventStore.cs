﻿using System;
using System.Collections.Generic;
using System.Linq;
using CQRSlite.Contracts.Bus.Events;
using CQRSlite.Contracts.Bus.Messages;

namespace CQRSlite.Tests.TestSubstitutes
{
    public class TestInMemoryEventStore : IEventStore 
    {
        public readonly List<Event> Events = new List<Event>();

        public void Save(Guid aggregateId, Event @event)
        {
            Events.Add(@event);
        }

        public IEnumerable<Event> Get(Guid aggregateId, int fromVersion)
        {
            return Events.Where(x => x.Version > fromVersion).OrderBy(x => x.Version);
        }

        public int GetVersion(Guid aggregateId)
        {
            if (!Events.Any()) return 0;
            return Events.Max(x => x.Version);
        }
    }
}