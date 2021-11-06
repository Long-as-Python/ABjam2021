using System.Collections;
using UnityEngine;

namespace FUGAS.Vendetta
{
    public interface IObserver
    { 
        void Update(IObservableSubject subject);
    }
}