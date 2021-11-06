using UnityEngine;

namespace FUGAS.Vendetta
{
    public class PlayerSubject : AbstractObservableSubject
    {
        private readonly Transform _player;

        public PlayerSubject(Transform player)
        {
            _player = player;
        }
        public Vector2 Position => new Vector2(_player.position.x, _player.position.y);
    }
}