using System.Collections.Generic;
using Obstacles;
using UnityEngine;

namespace Generation
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private List<Transform> obstaclePoints;

        public Transform StartPoint { get => startPoint; }
        public Transform EndPoint { get => endPoint; }
        public List<Transform> ObstaclePoints { get => obstaclePoints; }

        public void InitObstacles(List<Obstacle> obstacles)
        {
            obstaclePoints.ForEach(p =>
            {
                var toLoad = obstacles[Random.Range(0, obstacles.Count - 1)];
                var loaded = Instantiate(toLoad, transform);
                loaded.transform.position = p.position;
            });
        }
    }
}
