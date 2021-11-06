namespace Observable.Abstractions
{
    public interface IObserver
    { 
        void Update(IObservableSubject subject);
    }
}