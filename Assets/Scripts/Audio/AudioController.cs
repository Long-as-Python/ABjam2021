using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private AudioSource playerJumpSource;
        [SerializeField] private AudioSource playerDieSource;
        [SerializeField] private AudioSource buttonClickSource;
        [SerializeField] private AudioSource playerShootSource; 
        public void OnPlayerJump()
        {
            if (playerJumpSource)
                playerJumpSource.Play();
        }

        public void OnPlayerDie()
        {
            if (playerDieSource)
                playerDieSource.Play();
        }

        public void OnPlayerShoot()
        {
            if (playerShootSource)
                playerShootSource.Play();
        }

        public void OnGameStart()
        {
            //mixer.FindSnapshot("OnGameStart").TransitionTo(0.5f);
        }

        public void OnGameRestart()
        {
            // snapshot to game start
            
        }

        public void ButtonClick()
        {
            if (buttonClickSource)
                buttonClickSource.Play();
        }
    }
}