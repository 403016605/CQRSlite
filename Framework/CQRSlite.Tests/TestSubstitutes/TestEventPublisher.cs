﻿using CQRSlite.Eventing;

namespace CQRSlite.Tests.TestSubstitutes
{
    public class TestEventPublisher: IEventPublisher {
        public void Publish<T>(T @event) where T : Event
        {
            Published++;
        }

        public int Published { get; set; }
    }
}