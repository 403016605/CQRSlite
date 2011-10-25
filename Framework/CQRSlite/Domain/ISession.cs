using System;

namespace CQRSlite.Domain
{
    public interface ISession
    {
        void Track(AggregateRoot aggregate);
        T Get<T>(Guid id) where T : AggregateRoot;
        void Commit();
        bool IsTracked(Guid id);
    }
}