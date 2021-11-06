using UnityEngine;

namespace FUGAS.Vendetta
{
    class ManInCrowdObserver : IObserver
    {
        public Vector2 NextPosition { get; set; }
        public void Update(IObservableSubject subject)
        {
            var player =  subject as PlayerSubject;

            // update position relatively to player
            // this update should be delayed in time for crowd effect
            NextPosition = player.Position;
        }
    }
}