using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardReaderLibrary
{
    public abstract class Publisher<T> : IPublisher<T>
    {
        public void Subscribe(ISubscriber<T> subscriber)
        {
            Subscribers.Add(subscriber);
        }

        public void Unsubscribe(ISubscriber<T> subscriber)
        {
            if (!Subscribers.TryTake(out subscriber))
            {
                throw new ArgumentException("subscriber not found.");
            }
        }

        public void Clear()
        {
            Subscribers = new ConcurrentBag<ISubscriber<T>>();
        }

        internal ConcurrentBag<ISubscriber<T>> Subscribers = new ConcurrentBag<ISubscriber<T>>();
    }
}
