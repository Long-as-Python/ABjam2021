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

        void IEventPublisher.OnPlayerShoot()
        {
            PlayerShoot.Invoke();
        }

        void IEventPublisher.OnPlayerDie()
        {
            PlayerDie.Invoke();
        }

        void IEventPublisher.OnGameStart()
        {
            GameStart.Invoke();
        }
        void IEventPublisher.OnGameRestart()
        {
            GameRestart.Invoke();
        }

        public void OnGameExit()
        {
            GameExit.Invoke();
        }

        public void OnButtonClick()
        {
            ButtonClick.Invoke();
        }

        public void OnGamePause()
        {
            GamePause.Invoke();
        }

        public UnityEvent GameExit = new UnityEvent();
        public UnityEvent PlayerJump = new UnityEvent();
        public UnityEvent ButtonClick = new UnityEvent();
        public UnityEvent GamePause = new UnityEvent();
        public UnityEvent PlayerShoot = new UnityEvent();
        public UnityEvent GameStart = new UnityEvent();
        public UnityEvent GameRestart = new UnityEvent();
        public UnityEvent PlayerDie = new UnityEvent();
    }
}