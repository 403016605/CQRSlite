﻿using System;
using System.Collections.Generic;
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
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            Version = Version + _changes.Count;
            _changes.Clear();
        }

        public void LoadFromHistory(IEnumerable<Event> history)
        {
            foreach (var e in history) ApplyChange(e, false);
        }

        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(Event @event, bool isNew)
        {
            Id = @event.Id;
            this.AsDynamic().Apply(@event);
            if(isNew)
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