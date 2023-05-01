using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // [SerializeField] Grass grassPrefab;
    // [SerializeField] Road roadPrefab;
    [SerializeField] Duck duck;
    [SerializeField] List<Terrain> terrainList;
    [SerializeField] int initialGrassCount = 2;
    [SerializeField] int horizontalSize;
    [SerializeField] int backViewDistance = -5;
    [SerializeField] int forwardViewDistance = 15;
    [SerializeField] int maxTerrain = -3;
    [SerializeField, Range(0, 1)] float treeProbability;

    Dictionary<int, Terrain> activeTerrainDict = new Dictionary<int, Terrain>(20);
    [SerializeField] private int travelDistance;
    private void Start()
    {



        //Create initial Grass befor player in start game
        for (int zPos = backViewDistance; zPos < initialGrassCount; zPos++)
        {
            var terrain = Instantiate(terrainList[0]);
            terrain.transform.position = new Vector3(0, 0, zPos);
            //mengecek apakah list dari terrain merupakan grass
            if (terrain is Grass grass)
            {
                grass.SetTreePercentage(zPos < -1 ? 1 : 0);
            }
            terrain.Generate(horizontalSize);
            activeTerrainDict[key: zPos] = terrain;

        }

        //Create initial road after player in start game
        for (int zPos = initialGrassCount; zPos < forwardViewDistance; zPos++)
        {
            var terrain = SpawnRandomTerrain(zPos);
            terrain.Generate(horizontalSize);

            //zpost = key, terrain =  value
            activeTerrainDict[zPos] = terrain;
        }
    }

    private Terrain SpawnRandomTerrain(int zPos)
    {
        Terrain terrainCheck = null;
        int randomIndex;
        Terrain terrain = null;
        for (int z = -1; z >= maxTerrain; z--)
        {
            var checkPos = zPos + z;
            if (terrainCheck == null)
            {
                terrainCheck = activeTerrainDict[checkPos];
                continue;
            }
            else if (terrainCheck.GetType() != activeTerrainDict[checkPos].GetType())
            {
                randomIndex = Random.Range(0, terrainList.Count);
                terrain = Instantiate(terrainList[randomIndex]);
                terrain.transform.position = new Vector3(0, 0, zPos);
                return terrain;
            }
            else
            {
                continue;
            }
        }
        //mengcopy list sehingga terdapat 2 list dengan isi yang sama
        var candidateTerrain = new List<Terrain>(terrainList);
        for (int i = 0; i < candidateTerrain.Count; i++)
        {
            if (terrainCheck.GetType() == candidateTerrain[i].GetType())
            {
                candidateTerrain.Remove(candidateTerrain[i]);
                break;
            }
        }

        randomIndex = Random.Range(0, candidateTerrain.Count);
        terrain = Instantiate(candidateTerrain[randomIndex]);
        terrain.transform.position = new Vector3(0, 0, zPos);
        return terrain;


    }

    private void Update() {
        
    }

    public void UpdateTravelDistance(Vector3 targetPosition)
    {
        if(targetPosition.z > travelDistance){
            travelDistance = Mathf.CeilToInt(targetPosition.z);
        }
    }

}
