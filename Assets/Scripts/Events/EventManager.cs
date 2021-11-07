using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    class EventManager : MonoBehaviour, IEventPublisher
    {
        void IEventPublisher.OnPlayerJump()
        {
            PlayerJump.Invoke();
        }

        void IEventPublisher.OnPlayerDie()
        {
            PlayerDie.Invoke();
        }

        void IEventPublisher.OnGameStart()
        {
            GameStart.Invoke();
        }

        public UnityEvent PlayerJump = new UnityEvent();
        public UnityEvent GameStart = new UnityEvent();
        public UnityEvent PlayerDie = new UnityEvent();
    }
}