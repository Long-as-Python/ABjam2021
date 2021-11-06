using System;
using Obstacles;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerEssentials
{
    public class PlayerController : MonoBehaviour
    {
        public UnityEvent<PlayerController> Die;

        private void Start()
        {
            Die ??= new UnityEvent<PlayerController>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Obstacle>(out var obstacle))
            {
                if (!obstacle.isActiveObstacle) return;
                obstacle.Deactivate();
                Debug.Log("player dies");
                OnDie();
            }
        }

        private void OnDie()
        {
            Die.Invoke(this);
        }

        public void ActivateControl()
        {
            GetComponent<PlayerMovement>().isControlled = true;
        }

        public void Deactivate()
        {
            GetComponent<PlayerMovement>().isControlled = false;
            // TODO: change sprite
            this.gameObject.SetActive(false);
        }
    }
}