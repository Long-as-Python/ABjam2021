using System;
using Audio;
using Events;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        private GameManager gameManager;
        private IEventPublisher events;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            events = FindObjectOfType<EventManager>();
        }

        void GameStartClick()
        {
            events.OnButtonClick();
            gameManager.StartGame();
        }

        void GameRestartClick()
        {
            events.OnButtonClick();
            gameManager.RestartGame();
        }

        void ExitClick()
        {
            events.OnButtonClick();
            gameManager.ExitGame();
        }
    }
}