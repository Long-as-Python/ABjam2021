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
            if(!immortal)
                isActiveObstacle = false;
            
            // TODO: deactivate sprite 
            // this.gameObject.SetActive(false);
        }
    }
}