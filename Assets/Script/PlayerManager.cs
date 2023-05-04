using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    // [SerializeField] Grass grassPrefab;
    // [SerializeField] Road roadPrefab;
    [SerializeField] List<Terrain> terrainList;
    [SerializeField] int initialGrassCount = 2;
    [SerializeField] int horizontalSize;
    [SerializeField] int backViewDistance = -5;
    [SerializeField] int forwardViewDistance = 15;
    [SerializeField] int maxTerrain = -3;

    Dictionary<int, Terrain> activeTerrainDict = new Dictionary<int, Terrain>(20);
    [SerializeField] private int travelDistance;
    [SerializeField] private int coin;

    public UnityEvent <int,int> OnUpdateTerrainLimit;
    public UnityEvent <int> OnScoreUpdate;

    private void Start()
    {
        //Untuk membuat pohon penuh antara backViewDistance sampai initianGrassCount
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

        //Membuat intial grass(terrain kosong tanpa pohon) agar player bisa mencobanya terlebih dahulu
        for (int zPos = initialGrassCount; zPos < forwardViewDistance; zPos++)
        {
            var terrain = SpawnRandomTerrain(zPos);
        }
        OnUpdateTerrainLimit.Invoke(horizontalSize,travelDistance+backViewDistance);

    }

    private Terrain SpawnRandomTerrain(int zPos)
    {
        Terrain terrainCheck = null;
        int randomIndex;
        //fungsi dari maxterrain adalah untuk membatasi maksimal terrain yg respon secara bersamaan sesuai dengan datanya
        for (int z = -1; z >= maxTerrain; z--)
        {
            var checkPos = zPos + z;
            if (terrainCheck == null)
            {
                //Memasukkan data di activeTerrainDict dengan key checkpos kedalam terrainCheck
                terrainCheck = activeTerrainDict[checkPos];
                //terrainCheck sudah tidak null lagi
                continue;
            }
            //Mengecek tipedata terrainCheck dengan activeTerrainDict dengan key checkPos
            else if (terrainCheck.GetType() != activeTerrainDict[checkPos].GetType())
            {
                randomIndex = Random.Range(0, terrainList.Count);
                return SpawnTerrain(terrainList[randomIndex],zPos);

            }
            else
            {
                continue;
            }
        }
        //mengcopy list sehingga terdapat 2 list dengan isi yang sama
        var candidateTerrain = new List<Terrain>(terrainList);
        //Menghapus terrain yang sudah dipanggil sebanyak max terrain (pada code sebelumnya), sehingga terrain yang lain (yang belum kespawn) dapat dipanggil
        for (int i = 0; i < candidateTerrain.Count; i++)
        {
            if (terrainCheck.GetType() == candidateTerrain[i].GetType())
            {
                candidateTerrain.Remove(candidateTerrain[i]);
                break;
            }
        }
        //Memilih secara acak terrain yang tersisa pada candidateTerrain setelah ada yang dihapus
        randomIndex = Random.Range(0, candidateTerrain.Count);

        return SpawnTerrain(candidateTerrain[randomIndex],zPos);
    }

    public Terrain SpawnTerrain(Terrain terrain, int zPos){
        terrain = Instantiate(terrain);
        terrain.transform.position = new Vector3(0, 0, zPos);
        terrain.Generate(horizontalSize);
        activeTerrainDict[zPos] = terrain; 
        return terrain;
    }


    //Mengupdate traveldistance/skor ketika character bergerak maju ke depan
    public void UpdateTravelDistance(Vector3 targetPosition)
    {
        if (targetPosition.z > travelDistance)
        {
            travelDistance = Mathf.CeilToInt(targetPosition.z);
            UpdateTerrain();
            
        }
    }

    public void AddCoin(int value=1){
        this.coin += value;
    }

    private int GetScore(){
        return travelDistance + coin*3;
    }

    //Menambah dan menghapus terrain ketika character maju kedepan
    public void UpdateTerrain()
    {
        //Remove Game Object depend on the last distance
        var destroyPos = travelDistance - 1 + backViewDistance;
        Destroy(activeTerrainDict[destroyPos].gameObject);
        //Remove component
        activeTerrainDict.Remove(destroyPos);

        //Add/Spawn Terrain
        SpawnRandomTerrain(travelDistance - 1 + forwardViewDistance);
    
        OnUpdateTerrainLimit.Invoke(horizontalSize,travelDistance+backViewDistance);
    }

}
