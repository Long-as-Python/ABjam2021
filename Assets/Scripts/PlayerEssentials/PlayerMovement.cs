using System;
using UnityEngine;

namespace PlayerEssentials
{
    public class PlayerMovement : MonoBehaviour
    {
        public float runSpeed = 40f;

        float horizontalMove = 0f;
        bool jump = false;
        bool crouch = false;
        public bool isControlled;
        private Rigidbody2D _rigidBody;
        private Animator _animator;
        private CharacterController2D _controller;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _controller = GetComponent<CharacterController2D>();
        }

        void Update()
        {
            if (!isControlled)
                return;
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            if (horizontalMove != 0)
            {
                _animator.SetTrigger("player_run");
                _rigidBody.isKinematic = false;
            }
            else
            {
                _animator.SetTrigger("player_idle");
            }

            if (Input.GetButtonDown("Jump"))
            {
                _rigidBody.isKinematic = false;
                jump = true;
                _animator.SetTrigger("player_jump");
            }

            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }

            if (!_controller.isGrounded && _rigidBody.velocity.y < 0)
            {
                _animator.SetTrigger("player_landing");
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                _controller.Fire();
            }
        }

        void FixedUpdate()
        {
            // Move our character
            _controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
    }
}