using Audio;
using Events;
using PlayerEssentials;
using UnityEngine;
using Generation;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private AudioController audioController;
    private EventManager eventManager;
    private IEventPublisher eventPublisher;
    public GameManager Instance { get; private set; }
    private ChunkController chunksPool;
    private PlayerPool playerPool;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else return;
        eventManager = GetComponent<EventManager>();
        eventPublisher = eventManager;
        audioController = GetComponent<AudioController>();
        eventManager.GameStart.AddListener(audioController.OnGameStart);
        eventManager.PlayerJump.AddListener(audioController.OnPlayerJump);
        eventManager.PlayerDie.AddListener(audioController.OnPlayerDie);
        eventManager.PlayerShoot.AddListener(audioController.OnPlayerShoot);
        eventManager.GameRestart.AddListener(audioController.OnGameRestart);
        eventManager.ButtonClick.AddListener(audioController.ButtonClick);
    }

    private void Start()
    {
        chunksPool = FindObjectOfType<ChunkController>();
        playerPool = FindObjectOfType<PlayerPool>();
    }

    public void StartGame()
    {
        chunksPool.StartGame();
        playerPool.StartGame();
        eventPublisher.OnGameStart();
    }

    public void RestartGame()
    {
        eventPublisher.OnGameRestart();
        StartGame();
    }

    public void OnPlayerDie(PlayerController arg0)
    {
        eventPublisher.OnPlayerDie();
    }

    public void ToMainMenu()
    {
        chunksPool.ResetPool();
        playerPool.ResetPool();
        eventPublisher.OnGameExit();
    }

    public void ExitGame()
    {
        eventPublisher.OnGameExit();

        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void PauseGame()
    {
        eventPublisher.OnGamePause();
    }
}