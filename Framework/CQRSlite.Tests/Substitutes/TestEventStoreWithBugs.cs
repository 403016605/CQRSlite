using System;
using System.Collections.Generic;
using CQRSlite.Events;

namespace CQRSlite.Tests.Substitutes
{
    public class TestEventStoreWithBugs : IEventStore
    {
        public TestEventStoreWithBugs()
        {
            SavedEvents = new List<IEvent>();
        }
        public IEnumerable<IEvent> Get(Guid aggregateId, int version)
        {
            if (aggregateId == Guid.Empty)
            {
                return new List<IEvent>();
            }

            return new List<IEvent>
                {
                    new TestAggregateDidSomething {Id = aggregateId, Version = 2},
                    new TestAggregateDidSomeethingElse {Id = aggregateId, Version = 1},
                    new TestAggregateDidSomething {Id = aggregateId, Version = 3},
                };

        }

        public int GetVersion(Guid aggregateId)
        {
            return aggregateId == Guid.Empty ? 0 : 2;
        }
        public void Save(Guid aggregateId, IEvent eventDescriptor)
        {
            SavedEvents.Add(eventDescriptor);
        }

        public List<IEvent> SavedEvents { get; set; }
    }
}