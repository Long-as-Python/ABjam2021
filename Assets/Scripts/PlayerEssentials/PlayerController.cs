using System;
using Obstacles;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerEssentials
{
    public class PlayerController : MonoBehaviour
    {
        public UnityEvent Die;

        private void Start()
        {
            Die ??= new UnityEvent();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Obstacle>(out _))
            {
                OnDie();
            }
        }

        private void OnDie()
        {
            Die.Invoke();
        }

        public void ActivateControl()
        {
            GetComponent<PlayerMovement>().isControlled = true;
            GetComponent<SnapshotRecorder>().recorderEnabled = true;
        }
    }
}