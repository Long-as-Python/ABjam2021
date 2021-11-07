namespace Events
{
    interface IEventPublisher
    {
        void OnPlayerJump();

        void OnPlayerDie();

        void OnGameStart();
        void OnPlayerShoot();
        void OnGameRestart();
        void OnGameExit();
        void OnButtonClick();
        void OnGamePause();
    }
}