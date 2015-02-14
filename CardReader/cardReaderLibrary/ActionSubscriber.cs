using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardReaderLibrary
{
    public class ActionSubscriber<T> : ISubscriber<T>
    {
        public ActionSubscriber(Action<T> playAction)
        {
            this.PlayAction = playAction;
        }

        public void Play(T item)
        {
            PlayAction.Invoke(item);
        }

        Action<T> PlayAction;
    }
}
