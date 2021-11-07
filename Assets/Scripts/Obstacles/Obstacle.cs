using Cinemachine;
using UnityEngine;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        public bool isActiveObstacle { get; set; } = true;
        [SerializeField] private bool immortal;
        //default obstacle

        public void Deactivate()
        {
            if (!immortal)
                isActiveObstacle = false;

            var bodies = GetComponentsInChildren<Rigidbody2D>();
            foreach (var item in bodies)
            {
                item.isKinematic = true;
                item.bodyType = RigidbodyType2D.Dynamic;
            } 
        }
    }
}