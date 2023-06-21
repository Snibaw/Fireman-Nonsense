using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoad : MonoBehaviour
{
    [SerializeField] private GameObject[] roadPrefab;
    [SerializeField] private float length;
    private int normalRoadCompter = 0;
    // [SerializeField] private int maxRoadCompter =10;
    private float roadLength = 10;
    // [SerializeField] private GameObject[] itemsToPickUp;
    [SerializeField] private GameObject[] itemPatterns;
    [SerializeField] private GameObject[] gatePatterns;
    [SerializeField] private GameObject CardBoard;
    private List<string> gateNames = new List<string> {"Fire Rate", "Triple", "Damage"};
    private GameObject road;
    private int doSpawn = 2;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < length; i++)
        {
            SpawnRoad(i);
            if(i>2 && normalRoadCompter != 0 && doSpawn>0)
            {
                
                if(i%2 == 0) SpawnObjectsOnRoad();
                else 
                {
                    int rdNumber = Random.Range(0, 4);
                    if(rdNumber != 0) SpawnGatesOnRoad();
                    else SpawnCardBoardOnRoad();
                }
                doSpawn--;
            }
            else
            {
                doSpawn = 2;
            }
            
        }
    }
    private void SpawnRoad(int i)
    {
        // if(normalRoadCompter == maxRoadCompter)
        // {
        //     normalRoadCompter = 0;
        //     int rdNumber = 1+Random.Range(0, 2);
        //     road = Instantiate(roadPrefab[rdNumber], new Vector3(0, 0, i * roadLength), Quaternion.identity);
            
        // }
        // else
        // {
            // normalRoadCompter++;
            // road = Instantiate(roadPrefab[0], new Vector3(0, 0, i * roadLength), Quaternion.identity);
        // }
        normalRoadCompter++;
        road = Instantiate(roadPrefab[0], new Vector3(0, 0, i * roadLength), Quaternion.identity);
        road.transform.parent = transform;
    }
    

    private void SpawnObjectsOnRoad()
    {
        int rdNumber = Random.Range(0, itemPatterns.Length);
        GameObject item = Instantiate(itemPatterns[rdNumber], new Vector3(0, 0.5f, road.transform.position.z-3), Quaternion.identity);
    }

    private void SpawnGatesOnRoad()
    {
        GameObject gate = Instantiate(gatePatterns[0], new Vector3(0, 1.8f, road.transform.position.z-5), Quaternion.identity);

        string gateName = gateNames[Random.Range(0, gateNames.Count)];
        if(gateName == "Triple") gateNames.Remove("Triple");
        gate.transform.GetChild(0).GetComponent<GateBehaviour>().Initiate(Random.Range(-25,26), gateName, Random.Range(0,3) == 0, Random.Range(0,3) == 0);
    }

    private void SpawnCardBoardOnRoad()
    {
        GameObject cardBoard = Instantiate(CardBoard, new Vector3(0, 1.1f, road.transform.position.z-3), Quaternion.identity);
    }

    // private void SpawnARandomItem(float rdPosition = -1, int indexException = -1)
    // {
    //     //Spawn an item or not (2 chances out of 3 to spawn)
    //     if(Random.Range(0, 3) == 0) return;

    //     //Do we need to spawn an item on the other side of the road after the first one ?
    //     bool spawnAnotherOne = rdPosition == -1;

    //     //Index of the item to spawn
    //     int rdIndex = Random.Range(0, itemsToPickUp.Length);
    //     // We don't want the same TYPE of item to spawn twice in a row;
    //     while (rdIndex%2 == indexException%2)
    //     {
    //         rdIndex = Random.Range(0, itemsToPickUp.Length);
    //     }
    //     // Position of the item to spawn, if -1, it will be random
    //     rdPosition = rdPosition == -1 ? -3.5f*Random.Range(0, 2) : rdPosition;
    //     switch (rdIndex)
    //     {
    //         case 0:
    //             Spawn4WaterDrop(rdPosition);
    //             break;
    //         case 1:
    //             Spawn4Barrel(rdPosition);
    //             break;
    //         case 2:
    //             SpawnBigWaterDrop(rdPosition);
    //             break;
    //         case 3:
    //             SpawnBigBarrel(rdPosition);
    //             break;
    //         default:
    //             break;
    //     }
    //     if(spawnAnotherOne) SpawnARandomItem(-rdPosition-3.5f, rdIndex);
    // }


    // private void Spawn4WaterDrop(float x)
    // {
    //     GameObject item = Instantiate(itemsToPickUp[0], new Vector3(x, 0.5f, road.transform.position.z-3), Quaternion.identity);
    // }
    // private void Spawn4Barrel(float x)
    // {
    //     GameObject item = Instantiate(itemsToPickUp[1], new Vector3(x, 0.5f, road.transform.position.z-3), Quaternion.identity);
    // }
    // private void SpawnBigWaterDrop(float x)
    // {
    //     GameObject item = Instantiate(itemsToPickUp[2], new Vector3(x+2, 0.5f, road.transform.position.z-3.5f), Quaternion.identity);
    // }
    // private void SpawnBigBarrel(float x)
    // {
    //     GameObject item = Instantiate(itemsToPickUp[3], new Vector3(x+2, 0.5f, road.transform.position.z-3.5f), Quaternion.identity);
    // }

}
