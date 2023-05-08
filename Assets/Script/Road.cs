using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : Terrain
{
    [SerializeField] Car carPrefab;
    [SerializeField] float minCarSpawnInterval;
    [SerializeField] float maxCarSpawnInterval;
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] GameObject placeRocketDestroyParticlesGM;

    bool checkPosWeaponPrefab = false;

    float timer;
    Vector3 carSpawnPosition;
    Quaternion carRotation;

    private void Start()
    {
        //cara bacanya :
        if (Random.value > 0.5f)
        {
            carSpawnPosition = new Vector3(horizontalSize / 2 + 1,
                                           0,
                                           this.transform.position.z);
            carRotation = Quaternion.Euler(0, -90, 0);
            var weaponSpawnPosition = Instantiate(weaponPrefab,
                                     new Vector3(horizontalSize / 2 + 1,
                                                 0,
                                                 this.transform.position.z),
                                     Quaternion.Euler(0, -90, 0),
                                     transform);
            var DestroyRocketPosition = Instantiate(placeRocketDestroyParticlesGM,
                                     new Vector3(-(horizontalSize / 2 + 1),
                                                 0,
                                                 this.transform.position.z),
                                     Quaternion.Euler(0, 90, 0),transform);
            
        }
        else
        {
            carSpawnPosition = new Vector3(-(horizontalSize / 2 + 1),
                                           0,
                                           this.transform.position.z);
            carRotation = Quaternion.Euler(0, 90, 0);

            var weaponSpawnPosition = Instantiate(weaponPrefab,
                                     new Vector3(-(horizontalSize / 2 + 1),
                                                 0,
                                                 this.transform.position.z),
                                     Quaternion.Euler(0, 90, 0),
                                     transform);
            var DestroyRocketPosition = Instantiate(placeRocketDestroyParticlesGM,
                                     new Vector3(horizontalSize / 2 + 1,
                                                 0,
                                                 this.transform.position.z),
                                     Quaternion.Euler(0, -90, 0),
                                     transform);
        }

    }

    private void Update()
    {
        if (timer <= 0)
        {
            //spawn
            timer = Random.Range(minCarSpawnInterval, maxCarSpawnInterval);

            var car = Instantiate(carPrefab, carSpawnPosition, carRotation);

            car.SetUpDistanceLimit(horizontalSize + 1);

            return;
        }
        timer -= Time.deltaTime;
    }

}
