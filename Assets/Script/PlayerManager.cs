using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Grass grassPrefab;
    [SerializeField] Road roadPrefab;
    [SerializeField] int initialGrassCount = 5;
    [SerializeField] int horizontalSize;
    [SerializeField] int backViewDistance = -5;
    [SerializeField] int forwardViewDistance = 15;
    [SerializeField, Range(0, 1)] float treeProbability;

    private void Start()
    {

        //Create initial Grass befor player in start game
        for (int zPos = backViewDistance; zPos < initialGrassCount; zPos++)
        {
            var grass = Instantiate(grassPrefab);
            grass.transform.position = new Vector3(0, 0, zPos);
            grass.SetTreePercentage(zPos < -1 ? 1 : 0);
            grass.Generate(horizontalSize);
        }

        //Create initial road after player in start game
        for (int zPos = initialGrassCount; zPos < forwardViewDistance; zPos++)
        {
            var terrain = Instantiate(roadPrefab);
            terrain.transform.position = new Vector3(0, 0, zPos);
            // grass.SetTreePercentage(zPos < -1 ? 1 : 0);
            terrain.Generate(horizontalSize);
        }
    }
}
