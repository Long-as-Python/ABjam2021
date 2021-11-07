using System;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        private GameManager gameManager;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        void GameStartClick()
        {
            gameManager.StartGame();

        }
    }
}