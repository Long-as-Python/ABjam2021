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
            gameManager = GameObject.Find("GameRoot").GetComponent<GameManager>();
        }

        private void Start()
        {
            _playersPool = new List<PlayerController>();

            _playerPrefabs = Resources.LoadAll<PlayerController>("Players").ToList();

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
            // var camera = Camera.main;
            // camera.transform.SetParent(_activePlayer.transform, true);
            //SetCameraTargetDynamic();
            _freezeCamera = true;
            //StartCoroutine(LerpFromTo(camera.transform, camera.transform.position, 1));
            player.ActivateControl();
        }

        private void SetCameraTargetDynamic()
        {
            var targetPos = _activePlayer.transform.position;
            var camera = Camera.main;
            _cameraTarget = new Vector3(targetPos.x, targetPos.y, camera.transform.position.z);
        }

        IEnumerator LerpFromTo(Transform target, Vector3 pos1, float duration)
        {
            _freezeCamera = false;
            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                SetCameraTargetDynamic();
                target.position = Vector3.Lerp(pos1, _cameraTarget, t / duration);
                yield return null;
                if (_freezeCamera) yield break;
            }

            target.position = _cameraTarget;
            canUpdatePlayers = true;
        }


        void ChangeToNextPlayer(PlayerController whoDied)
        {
            // only if our hero dies
            if (whoDied == _activePlayer)
            {
                canUpdatePlayers = false;
                _lastPlayer = _playersPool.First();
                _playersPool.RemoveAt(0);
                // GeneratePlayer(1);
                var candidate = _playersPool.First();
                ActivatePlayer(candidate);
                _lastPlayer.Deactivate();
            }

            // TODO:  animate transition
        }

        private void FixedUpdate()
        {
            var snapshots = _activePlayer.GetComponent<SnapshotRecorder>();
            if (_playersPool.Count > 1)
                for (int i = 1; i < _playersPool.Count; i++)
                {
                    var player = _playersPool[i];
                    var snap = snapshots.Partial(1 - 1f * i / _playersPool.Count);
                    player.transform.position = snap.Position;
                    player.TryFlip(snap);
                    // todo: animator state change
                }
        }

        private void GeneratePlayer(float tOffset)
        {
            var prefab = _playerPrefabs.Random();
            var chainOffset = tOffset * Vector3.left * 1;
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