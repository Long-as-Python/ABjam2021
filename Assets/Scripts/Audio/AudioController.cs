using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;
        [SerializeField]   private AudioSource playerJumpSource;
        [SerializeField]   private AudioSource playerDieSource;

        private void Start()
        {
        }

        private void Update()
        {
        }

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

        public void OnGameStart()
        {
            //mixer.FindSnapshot("OnGameStart").TransitionTo(0.5f);
        }
    }
}