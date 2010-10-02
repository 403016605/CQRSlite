﻿using System;
using SimpleCQRS.Domain;

namespace SimpleCQRS.Tests.TestSubstitutes
{
    public class TestAggregate : AggregateRoot
    {
        public int I;
        public override Guid Id
        {
            get { return Guid.NewGuid(); }
        }

        public void DoSomething()
        {
            ApplyChange(new TestAggregateDidSomething());
        }

        public void DoSomethingElse()
        {
            ApplyChange(new TestAggregateDidSomeethingElse());
        }

        public void Apply(TestAggregateDidSomething e)
        {
            I++;
        }
    }

    public class TestAggregateNoParameterLessConstructor : AggregateRoot
    {
        public TestAggregateNoParameterLessConstructor(int i)
        {
            
        }

        public override Guid Id
        {
            get { throw new NotImplementedException(); }
        }
    }
}
