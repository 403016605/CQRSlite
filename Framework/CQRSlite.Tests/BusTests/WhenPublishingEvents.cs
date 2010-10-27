using System.Threading;
using CQRSlite.Bus;
using CQRSlite.Tests.TestSubstitutes;
using Xunit;

namespace CQRSlite.Tests.BusTests
{
    public class WhenPublishingEvents
    {
        private InProcessBus _bus;

        public WhenPublishingEvents()
        {
            _bus = new InProcessBus();
        }

        [Fact]
        public void ShouldPublishToAllHandlers()
        {
            var handler = new TestAggregateDidSomethingHandler();
            _bus.RegisterHandler<TestAggregateDidSomething>(handler.Handle);
            _bus.RegisterHandler<TestAggregateDidSomething>(handler.Handle);
            _bus.Publish(new TestAggregateDidSomething());
            Thread.Sleep(50);
            Assert.Equal(2, handler.TimesRun);
        }

        [Fact]
        public void ShouldWorkWithNoHandlers()
        {
            _bus.Publish(new TestAggregateDidSomething());
        }
    }
}