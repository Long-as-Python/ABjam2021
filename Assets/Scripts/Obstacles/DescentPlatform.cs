using UnityEngine;

namespace Obstacles
{
    public class DescentPlatform : MonoBehaviour
    {
        private Collider collider;

        public void Deactivate()
        {
            collider.isTrigger = true;
        }
    }
}