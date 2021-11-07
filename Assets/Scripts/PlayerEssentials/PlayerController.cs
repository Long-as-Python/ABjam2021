using System;
using Obstacles;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerEssentials
{
    public class PlayerController : MonoBehaviour
    {
        public UnityEvent<PlayerController> Die;
        private CharacterController2D _characterController;
        private Animator _animator;

        private void Awake()
        {
            Die ??= new UnityEvent<PlayerController>();
            _characterController = GetComponent<CharacterController2D>();
            _animator = GetComponent<Animator>();
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
            _animator.SetTrigger("player_die");
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

        public void TryFlip(Snapshot snap)
        {
            if (_characterController)
                if (_characterController.facingRight != snap.FacingRight)
                    _characterController.Flip();
        }
    }
}