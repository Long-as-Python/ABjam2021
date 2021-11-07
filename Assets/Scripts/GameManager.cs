using Audio;
using Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private AudioController audioController;
    private EventManager eventManager;
    private IEventPublisher eventPublisher;
    public GameManager Instance;

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
    }

    public void StartGame()
    {
        eventPublisher.OnGameStart();
    }
}