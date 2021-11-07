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
        public GameObject PauseMenu;

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

        public void PauseClick()
        {
            //events.OnButtonClick();
            //gameManager.PauseGame();
            if (PauseMenu.active == true) PauseMenu.SetActive(false);
            else PauseMenu.SetActive(true);
        }

    }
}