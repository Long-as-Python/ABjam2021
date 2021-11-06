using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    [SerializeField] private List<Chunk> chunksToLoad;

    [SerializeField] private List<Chunk> currentChunks;
    [SerializeField] private int maxLoadedChunksCount;

    private void LoadChunk(Chunk chunk)
    {
        var loaded = Instantiate(chunk.gameObject, transform).GetComponent<Chunk>();

        var lastChunk = currentChunks[currentChunks.Count - 1];
        loaded.transform.position = lastChunk.EndPoint.position + (loaded.transform.position - loaded.StartPoint.position);

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
        var chunk = chunksToLoad[Random.Range(0, chunksToLoad.Count)];
        LoadChunk(chunk);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            LoadRandomChunk();

            if(currentChunks.Count > maxLoadedChunksCount)
                DestroyLastChunk();
        }
        
    }
}
