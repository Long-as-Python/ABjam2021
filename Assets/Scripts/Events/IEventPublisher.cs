namespace Events
{
    interface IEventPublisher
    {
        void OnPlayerJump();

        void OnPlayerDie();

        void OnGameStart();
    }
}