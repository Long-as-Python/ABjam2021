using Audio;
using Events;
using PlayerEssentials;
using UnityEngine;
using Generation;

public class GameManager : MonoBehaviour
{
    private AudioController audioController;
    private EventManager eventManager;
    private IEventPublisher eventPublisher;
    public GameManager Instance { get; private set; }

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
        eventManager.ButtonClick.AddListener(FindObjectOfType<ChunkController>().StartGame);
        eventManager.ButtonClick.AddListener(FindObjectOfType<PlayerPool>().StartGame);
    }

    public void StartGame()
    {
        eventPublisher.OnGameStart();
    }

    public void RestartGame()
    {
        eventPublisher.OnGameRestart();
    }

    public void OnPlayerDie(PlayerController arg0)
    {
        eventPublisher.OnPlayerDie();
    }

    public void ExitGame()
    {
        eventPublisher.OnGameExit();
    }

    public void PauseGame()
    {
        eventPublisher.OnGamePause();
    }
}