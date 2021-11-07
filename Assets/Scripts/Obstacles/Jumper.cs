using PlayerEssentials;
using UnityEngine;

namespace Obstacles
{
    public class Jumper : MonoBehaviour
    {
        private Animator animator;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CharacterController2D>(out var ch))
            {
                ch.Jump();

                //jumper anim
            }
        }
    }
}