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
            if (TryGetComponent<Animator>(out var animator))
            {
                animator.SetTrigger("on_hit");
            }
            // TODO: deactivate sprite 
            // this.gameObject.SetActive(false);
        }
    }
}