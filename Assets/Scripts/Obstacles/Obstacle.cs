using Cinemachine;
using UnityEngine;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        public bool isActiveObstacle { get; set; } = true; 
        public void Deactivate()
        {
            isActiveObstacle = false;

            var bodies = GetComponentsInChildren<Rigidbody2D>();
            foreach (var item in bodies)
            {
                item.simulated = true;
                item.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}