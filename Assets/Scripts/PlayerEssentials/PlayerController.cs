using System;
using Events;
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
        private bool playerDied;
        private IEventPublisher _events;
        public bool isGrounded => _characterController.isGrounded;

        private void Awake()
        {
            Die ??= new UnityEvent<PlayerController>();
            _characterController = GetComponent<CharacterController2D>();
            _animator = GetComponent<Animator>();
            var root = GameObject.Find("GameRoot");
            _events = root.GetComponent<EventManager>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Obstacle>(out var obstacle))
            {
                if (!obstacle.isActiveObstacle || playerDied) return;
                OnDie();
                obstacle.Deactivate();
                playerDied = true;
                Debug.Log("player dies");
            }
        }

        private void OnDie()
        {
            if (playerDied) return;
            _events.OnPlayerDie();
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

            // this.gameObject.SetActive(false);
        }

        public void ApplySnapshot(Snapshot snap)
        {
            if (_characterController)
                if (_characterController.facingRight != snap.FacingRight)
                    _characterController.Flip();
            transform.position = snap.Position;
            _animator.Play(snap.AnimatorState);
        }
    }
}