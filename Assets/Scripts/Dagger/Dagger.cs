using UnityEngine;

public class Dagger : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float force;
    private bool rotate = true;

    private void Start()
    {
        //MoveRight();
    }

    private void Update()
    {
        if (!rotate)
            return;

        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.forward, speed);
    }

    public void MoveLeft()
    {
        MoveByVector(Vector3.left);
        speed *= -1;
    }
    public void MoveRight()
    {
        MoveByVector(Vector3.right);
    }
    private void MoveByVector(Vector3 v)
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = v * force;
    }
}
