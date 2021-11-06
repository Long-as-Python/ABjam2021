using System;
using System.Collections.Generic;

namespace FUGAS.Vendetta
{
    public class AbstractObservableSubject : IObservableSubject
    {
        private List<IObserver> _observers = new List<IObserver>();

        void IObservableSubject.Attach(IObserver observer)
        {
            this._observers.Add(observer);
        }

        void IObservableSubject.Detach(IObserver observer)
        {
            this._observers.Remove(observer);
        }

        void IObservableSubject.Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }
    }
}