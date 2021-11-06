using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerEssentials
{
    public class PlayerPool : MonoBehaviour
    {
        public int MaxPlayersInPool = 10;
        private PlayerController _activePlayer;
        private PlayerController _lastPlayer;
        private List<PlayerController> _playersPool;
        private List<PlayerController> _playerPrefabs;
        private SnapshotRecorder snapShotRecorder;

        private void Start()
        {
            _playersPool = new List<PlayerController>();

            _playerPrefabs = Resources.LoadAll<PlayerController>("Players").ToList();

            while (_playersPool.Count < MaxPlayersInPool)
            {
                GeneratePlayer();
            }

            ActivatePlayer(_playersPool.First());
        }

        private void ActivatePlayer(PlayerController player)
        {
            _activePlayer = player;
            player.ActivateControl();
        }

        void ChangeToNextPlayer()
        {
            _lastPlayer = _playersPool.First();
            _playersPool.RemoveAt(0);
            GeneratePlayer();
            ActivatePlayer(_playersPool.First());

            // TODO:  animate transition
        }

        private void FixedUpdate()
        {
            var snapshots = _activePlayer.GetComponent<SnapshotRecorder>();
            if (_playersPool.Count > 2)
                for (int i = 1; i < _playersPool.Count; i++)
                {
                    var player = _playersPool[i];
                    player.transform.position = snapshots.Partial(1f * i / _playersPool.Count);
                }
        }

        private void GeneratePlayer()
        {
            var prefab = _playerPrefabs.Random();
            var player = Instantiate(prefab, prefab.transform.position + transform.position, Quaternion.identity);
            player.transform.parent = this.transform;
            player.Die.AddListener(ChangeToNextPlayer);
            _playersPool.Add(player);
        }
    }
}