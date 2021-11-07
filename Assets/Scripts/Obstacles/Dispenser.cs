using UnityEngine;

namespace Obstacles
{
    public class Dispenser : MonoBehaviour
    {
        [SerializeField] private GameObject dispenseItem;
        [SerializeField] private Transform dispensePoint;
        [SerializeField] private Vector2 moveVector;
        [SerializeField] private float delay;
        [SerializeField] private float force;
        [SerializeField] private float destroyTime;

        private float timer;
        private bool isActive = true;

        private void Update()
        {
            if (isActive == false)
                return;

            if (timer >= delay)
            {
                timer = 0f;
                Dispense();
            }
        }

        private void FixedUpdate()
        {
            if (isActive == false)
                return;

            timer += Time.deltaTime;
        }

        private void Dispense()
        {
            var go = Instantiate(dispenseItem, transform);
            go.transform.position = dispensePoint.position;

            var rb = go.GetComponent<Rigidbody2D>();
            rb.velocity = moveVector.normalized * force;

            Destroy(go, destroyTime);
        }

        public void OnDisable()
        {
            isActive = false;
        }
    }
}
