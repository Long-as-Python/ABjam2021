using System;
using UnityEngine;

namespace UI
{
    public class ParalaxBackground : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1;
        private float length, startPosition;
        private Camera cam;

        private void Start()
        {
            cam = Camera.main;
            startPosition = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.x ;
        }

        private void Update()
        {
            float temp = cam.transform.position.x * (1 - moveSpeed);
            float dist = cam.transform.position.x * moveSpeed;
            transform.position = new Vector3(startPosition + dist, transform.position.y, transform.position.z);

            if (temp > startPosition + length) startPosition += length;
            else if (temp < startPosition - length) startPosition -= length;
            
        }
    }
}