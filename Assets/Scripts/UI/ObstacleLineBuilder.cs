using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ObstacleLineBuilder : MonoBehaviour
    {
        private List<GameObject> _tiles = new List<GameObject>();
        void Start()
        {
            // TODO: 
            // 1. get list of resources (building blocks)
            var sources = Resources.LoadAll<BuildingBlock>("LevelBlocks");

            // 2. calculate world bounds (maybe like this)
            var canvas = FindObjectOfType<Canvas>();
            var rectComponent = canvas.GetComponent<RectTransform>();
            var bounds = rectComponent.rect;

            // 3. build chain of sprites
            //   -  get random tile from resources 
            //   -  move to tile right bounds
            //   -  repeat until != world bounds + offset


            // TODO: test this code below
            BuildingBlock item = GetNextBlock(sources);
            var elementWidth = item.GetComponent<SpriteRenderer>().bounds.size.x;
            var boundsOffset = elementWidth * 5;

            var target = GameObject.Find("background_target");
            var targetRect = target.GetComponent<SpriteRenderer>();
            var min = targetRect.bounds.min;

            float currentOffset = target.transform.position.x;
            while (currentOffset < bounds.width + boundsOffset)
            {
                item = GetNextBlock(sources);
                var targetPosition = target.transform.position - min;
                elementWidth = item.GetComponent<SpriteRenderer>().bounds.size.x;
                currentOffset += elementWidth;
                var tile = Instantiate(item, targetPosition, Quaternion.identity);
                _tiles.Add(tile.gameObject);
            }


        }
        /// <summary>
        /// Returns random value from array of sources
        /// </summary>
        /// <returns></returns>
        private BuildingBlock GetNextBlock(BuildingBlock[] sources)
        {
            var index = (int)(Random.value * (sources.Length - 1));
            return sources[index];
        }
    }
}
