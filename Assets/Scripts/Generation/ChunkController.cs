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

            while (currentChunks.Count < maxLoadedChunksCount)
            {
                LoadRandomChunk();
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

                // TODO: animate with lerp
                transform.position = transform.position - currentChunks[currentChunks.Count / 2].EndPoint.position;
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

        private void LoadRandomChunk()
        {
            var chunk = chunksToLoad.Random();
            LoadChunk(chunk);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                LoadRandomChunk();

                if (currentChunks.Count > maxLoadedChunksCount)
                    DestroyLastChunk();
            }
        }
    }
}