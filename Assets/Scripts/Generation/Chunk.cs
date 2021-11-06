using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    public Transform StartPoint { get => startPoint; }
    public Transform EndPoint { get => endPoint; }
}
