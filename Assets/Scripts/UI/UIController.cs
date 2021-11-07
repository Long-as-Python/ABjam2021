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
        public GameObject MainMenu;
        public GameObject GameUI;

        private void Start()
        {
            var root = GameObject.Find("GameRoot");
            gameManager = root.GetComponent<GameManager>();
            events = root.GetComponent<EventManager>();
            PauseMenu.SetActive(false);
        }

        public void GameStartClick()
        {
            events.OnButtonClick();
            gameManager.StartGame();
            MainMenu.SetActive(false);
            GameUI.SetActive(true);
        }

        void GameRestartClick()
        {
            events.OnButtonClick();
            gameManager.RestartGame();
        }

        public void ExitGameClick()
        {
            events.OnButtonClick();
            gameManager.ExitGame();
        }

        public void LeaveGameClick()
        {
            events.OnButtonClick();
            gameManager.ExitGame();
            MainMenu.SetActive(true);
            PauseMenu.SetActive(false);
        }

        public void PauseClick()
        {
            events.OnButtonClick();
            gameManager.PauseGame();
            if (PauseMenu.active == true) PauseMenu.SetActive(false);
            else PauseMenu.SetActive(true);
        }

    }
}