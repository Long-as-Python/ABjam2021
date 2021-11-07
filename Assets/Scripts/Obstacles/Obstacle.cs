using Cinemachine;
using UnityEngine;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        public bool isActiveObstacle { get; set; }
        //default obstacle

        public void Deactivate()
        {
            isActiveObstacle = false;
            
            // TODO: deactivate sprite 
            // this.gameObject.SetActive(false);
        }
    }
}