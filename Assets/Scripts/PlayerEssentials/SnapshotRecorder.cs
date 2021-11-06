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
        public bool recorderEnabled;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _positions = new List<Snapshot>();
            maxSnapshotPoints = (int) Math.Round(maxSecondsToRecord * 1f / Time.fixedDeltaTime);
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (!recorderEnabled) return;
                // if (_rigidbody.IsSleeping()) return;
            
            _positions.Add(new Snapshot {Position = transform.position});
            while (_positions.Count > maxSnapshotPoints)
                _positions.RemoveAt(0);
        }

        public Vector3 Partial(float t)
        {
            if (!_positions.Any()) return transform.position;
            return _positions[(int) (_positions.Count * t)].Position;
        }
    }

    internal struct Snapshot
    {
        public Vector3 Position;
    }
}