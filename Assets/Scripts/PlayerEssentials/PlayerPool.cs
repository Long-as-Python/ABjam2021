using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Events;
using Generation;
using Helpers;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerEssentials
{
    public class PlayerPool : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cineCamera;
        public int MaxPlayersInPool = 10;
        private PlayerController _activePlayer;
        private PlayerController _lastPlayer;
        private List<PlayerController> _playersPool;
        private List<PlayerController> _playerPrefabs;
        private SnapshotRecorder snapShotRecorder;
        private bool _freezeCamera;
        private bool canUpdatePlayers;
        private Vector3 _cameraTarget;
        private GameManager gameManager;

        private void Awake()
        {
            _playersPool = new List<PlayerController>();

            gameManager = GameObject.Find("GameRoot").GetComponent<GameManager>();

            _playerPrefabs = Resources.LoadAll<PlayerController>("Players").ToList();
        }

        public void ResetPool()
        {
            if (!_playersPool.Any()) return;

            _playersPool.ForEach(x => GameObject.Destroy(x.gameObject));
            _playersPool.Clear();
            // ensure everything destroyed
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public void StartGame()
        {
            ResetPool();

            while (_playersPool.Count < MaxPlayersInPool)
            {
                GeneratePlayer(_playersPool.Count * 1f / MaxPlayersInPool);
            }

            ActivatePlayer(_playersPool.First());
        }

        private void ActivatePlayer(PlayerController player)
        {
            _activePlayer = player;
            cineCamera.Follow = _activePlayer.transform;
            player.ActivateControl();
        }

        void ChangeToNextPlayer(PlayerController whoDied)
        {
            // only if our hero dies
            if (whoDied != _activePlayer) return;

            canUpdatePlayers = false;
            _lastPlayer = _playersPool.First();

            // last player died
            if (_playersPool.Count == 1)
            {
                gameManager.RestartGame();
                return;
            }

            _playersPool.RemoveAt(0);
            var candidate = _playersPool.First();
            ActivatePlayer(candidate);
            _lastPlayer.Deactivate();
        }

        private void FixedUpdate()
        {
            if (!_activePlayer) return;
            var snapshots = _activePlayer.GetComponent<SnapshotRecorder>();
            if (_playersPool.Count <= 1) return;

            for (var i = 1; i < _playersPool.Count; i++)
            {
                var player = _playersPool[i];
                var partial = snapshots.Partial(1 - 1f * i / _playersPool.Count);
                var snap = partial;
                player.ApplySnapshot(snap);
            }
        }

        private void GeneratePlayer(float tOffset)
        {
            var prefab = _playerPrefabs.Random();
            var chainOffset = tOffset * Vector3.left * 3;
            var player = Instantiate(prefab, prefab.transform.position + transform.position + chainOffset,
                Quaternion.identity);
            player.transform.parent = this.transform;
            player.GetComponent<CharacterController2D>().OnLandEvent.AddListener(OnPlayerLanded);
            player.Die.AddListener(ChangeToNextPlayer);
            player.Die.AddListener(gameManager.OnPlayerDie);
            player.GetComponent<Rigidbody2D>().isKinematic = true;
            _playersPool.Add(player);
        }

        private void OnPlayerLanded(CharacterController2D player, Chunk target)
        {
            if (_activePlayer.gameObject == player.gameObject)
            {
                FindObjectOfType<ChunkController>().TryLoadChunk(target);
            }
        }
    }
}