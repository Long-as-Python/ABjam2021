using System;
using Obstacles;
using PlayerEssentials;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float force;
    private bool rotate = true;

    private void Update()
    {
        if (!rotate)
            return;

        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
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

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.TryGetComponent<Obstacle>(out var obs))
        {
            obs.Deactivate();
            Destroy(gameObject);
        }
        else if (!collision.gameObject.TryGetComponent<CharacterController2D>(out var player)
            && !collision.gameObject.TryGetComponent<Dagger>(out var dagger))
        {
            rotate = false;
            var rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
        }
    }
}
