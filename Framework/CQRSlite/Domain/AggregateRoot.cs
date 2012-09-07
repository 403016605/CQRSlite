﻿using System;
using System.Collections.Generic;
using CQRSlite.Domain.Exception;
using CQRSlite.Eventing;
using CQRSlite.Infrastructure;

namespace CQRSlite.Domain
{
    public abstract class   AggregateRoot
    {
        private readonly List<Event> _changes = new List<Event>();

        public Guid Id { get; protected set; }
        public int Version { get; private set; }

        public IEnumerable<Event> GetUncommittedChanges()
        {
            lock (_changes)
            {
                return _changes.ToArray();
            }
        }

        public void MarkChangesAsCommitted()
        {
            Version = Version + _changes.Count;
            _changes.Clear();
        }

        public void LoadFromHistory(IEnumerable<Event> history)
        {
            foreach (var e in history)
            {
                if (e.Version != Version + 1)
                    throw new EventsOutOfOrderException();
                ApplyChange(e, false);
            }
        }

        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(Event @event, bool isNew)
        {
            lock (_changes)
            {
                Id = @event.Id;
                this.AsDynamic().Apply(@event);
                if (isNew)
                {
                    _changes.Add(@event);
                }
                else
                {
                    Version++;
                }
            }
        }
    }
}