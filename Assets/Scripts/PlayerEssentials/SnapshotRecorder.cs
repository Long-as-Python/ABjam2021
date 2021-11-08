using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayerEssentials
{
    public class SnapshotRecorder : MonoBehaviour
    {
        private List<Snapshot> _positions;
        private int maxSecondsToRecord = 10;
        [SerializeField] private int maxSnapshotPoints;
        private Rigidbody2D _rigidbody;
        [SerializeField] private bool isSleeping;
        [SerializeField] private int positionsCount;
        private CharacterController2D _characterController;
        private Animator _animator;

        private void Awake()
        {
            _positions = new List<Snapshot>();
            maxSnapshotPoints = (int) Math.Round(maxSecondsToRecord * 1f / Time.fixedDeltaTime);
            _rigidbody = GetComponent<Rigidbody2D>();
            _characterController = GetComponent<CharacterController2D>();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            isSleeping = _rigidbody.IsSleeping();
            positionsCount = _positions.Count;
            if (isSleeping) return;

            _positions.Add(new Snapshot
            {
                Position = transform.position,
                FacingRight = _characterController.facingRight,
                AnimatorState = _animator
                    .GetCurrentAnimatorStateInfo(0).shortNameHash
            });
            while (_positions.Count > maxSnapshotPoints)
                _positions.RemoveAt(0);
        }

        public Snapshot Partial(float t)
        {
            if (!_positions.Any()) return new Snapshot {Position = transform.position};
            return _positions[(int) (_positions.Count * t)];
        }

        public IEnumerable<Snapshot> PartialAfter(float t)
        {
            if (!_positions.Any()) return new[] {new Snapshot {Position = transform.position}};
            return _positions.Skip((int) (_positions.Count * t));
        }
    }

    public struct Snapshot
    {
        public Vector3 Position;
        public bool FacingRight;
        public int AnimatorState;
    }
}