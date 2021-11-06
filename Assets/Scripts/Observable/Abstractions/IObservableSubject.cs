namespace FUGAS.Vendetta
{
    public interface IObservableSubject
    { 
        void Attach(IObserver observer);
 
        void Detach(IObserver observer);
 
        void Notify();
    }
}