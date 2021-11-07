using System;
using UnityEngine;

namespace PlayerEssentials
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController2D controller;

        public float runSpeed = 40f;

        float horizontalMove = 0f;
        bool jump = false;
        bool crouch = false;
        public bool isControlled;
        private Rigidbody2D _rigidBody;
        private Animator _animator;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (!isControlled)
                return;
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            if (horizontalMove != 0)
                _rigidBody.isKinematic = false;
            if (Input.GetButtonDown("Jump"))
            {
                _rigidBody.isKinematic = false;
                jump = true;
            }

            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }
        }

        void FixedUpdate()
        {
            // Move our character
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
    }
}