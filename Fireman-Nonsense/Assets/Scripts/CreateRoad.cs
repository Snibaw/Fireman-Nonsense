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
    private List<string> gateNames = new List<string> {"Range", "Triple", "Damage"};
    private GameObject road;
    private int doSpawn = 2;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < length; i++)
        {
            SpawnRoad(i);
            if(i >= length-3) continue;
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
        float value = Random.Range(0f,6.5f);
        if(Random.Range(0,3) == 0) value = -value;
        if(gateName == "Triple") gateNames.Remove("Triple");
        gate.transform.GetChild(0).GetComponent<GateBehaviour>().Initiate(value, gateName, Random.Range(0,3) == 0, Random.Range(0,3) == 0);
    }

    private void SpawnCardBoardOnRoad()
    {
        GameObject cardBoard = Instantiate(CardBoard, new Vector3(0, 1.1f, road.transform.position.z-3), Quaternion.identity);
    }

}
