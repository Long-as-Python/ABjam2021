using System.Collections.Generic;
using System.Linq;
using Helpers;
using Obstacles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generation
{
    public class ChunkController : MonoBehaviour
    {
        private List<Chunk> chunksToLoad;
        private List<Obstacle> obstaclesToLoad;

        private List<Chunk> currentChunks;
        public int maxLoadedChunksCount;

        private void Start()
        {
            currentChunks = new List<Chunk>();
            chunksToLoad = Resources.LoadAll<Chunk>("Chunks").ToList();
            obstaclesToLoad = Resources.LoadAll<Obstacle>("Obstacles").ToList();
        }

        public void StartGame()
        {
            ResetPool();

            // fill pool with chunks
            while (currentChunks.Count < maxLoadedChunksCount)
            {
                LoadRandomChunk();
            }
            // move view to the center of the pool
            transform.position = transform.position - currentChunks[currentChunks.Count / 2].EndPoint.position;
        }

        public void ResetPool()
        {
            if (currentChunks.Any())
            {
                currentChunks.ForEach(GameObject.Destroy);
                currentChunks.Clear();
                // ensure everything destroyed
                foreach (Transform child in transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
        }

        private void LoadChunk(Chunk chunk)
        {
            var loaded = Instantiate(chunk.gameObject, transform).GetComponent<Chunk>();

            if (currentChunks.Any())
            {
                var lastChunk = currentChunks[currentChunks.Count - 1];
                loaded.transform.position =
                    lastChunk.EndPoint.position + (loaded.transform.position - loaded.StartPoint.position);
            }

            loaded.InitObstacles(obstaclesToLoad);
            currentChunks.Add(loaded);
        }

        private void DestroyLastChunk()
        {
            var chunk = currentChunks[0];
            currentChunks.RemoveAt(0);
            Destroy(chunk.gameObject);
        }

        public void LoadRandomChunk()
        {
            var chunk = chunksToLoad.Random();
            LoadChunk(chunk);
            // cleanup chunks pool 
            if (currentChunks.Count > maxLoadedChunksCount)
                DestroyLastChunk();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                LoadRandomChunk();
            }
        }

        public void TryLoadChunk(Chunk target)
        {
            var targetChunk = target.gameObject;
            // generate next chunk after player passes the middle of pool
            if (currentChunks.Skip(currentChunks.Count / 2).Any(x => x.gameObject.Equals(targetChunk)))
                LoadRandomChunk();
        }
    }
}