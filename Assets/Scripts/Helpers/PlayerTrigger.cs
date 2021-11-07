using PlayerEssentials;
using UnityEngine;
using UnityEngine.Events;

namespace Helpers
{
    public class PlayerTrigger : MonoBehaviour
    {
        public UnityEvent OnPlayerTriggered;

        private void Awake()
        {
            if (OnPlayerTriggered == null)
                OnPlayerTriggered = new UnityEvent();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CharacterController2D>(out var ch))
            {
                OnPlayerTriggered?.Invoke();
            }
        }
    }
}