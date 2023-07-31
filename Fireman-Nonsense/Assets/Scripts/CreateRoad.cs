using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoad : MonoBehaviour
{
    [SerializeField] private GameObject CrystalPrefab;
    [SerializeField] private GameObject[] roadPrefab;
    [SerializeField] private int length;
    private int lengthInfinite;
    private int normalRoadCompter = 0;
    [SerializeField] private int maxRoadCompter =10;
    private float roadLength = 10;
    // [SerializeField] private GameObject[] itemsToPickUp;
    [SerializeField] private GameObject[] itemPatterns1;
    [SerializeField] private GameObject[] itemPatterns2;
    [SerializeField] private GameObject[] itemPatterns3;
    [SerializeField] private GameObject[] itemPatterns4;
    [SerializeField] private GameObject[] itemPatterns5;
    [SerializeField] private GameObject[] itemPatterns6;
    private GameObject[] itemPatterns;
    [SerializeField] private GameObject[] gatePatterns;
    [SerializeField] private GameObject CardBoard;
    private List<string> gateNames = new List<string> {"Range", "Triple", "Damage"};
    private GameObject road;
    private int doSpawn = 2;

    [SerializeField] private GameObject EndBuilding;
    [SerializeField] private float distanceToShow = 50f;
    public int lastChildShowed = 0;
    private GameObject roadLastShow;
    
    [Header("City")]

    [SerializeField] private GameObject[] cityRoadPrefab;
    [SerializeField] private Vector3[] cityBasePosition;
    [SerializeField] private float DistanceToSpawnCityRoad;
    [SerializeField] private float cityLength;
    private int level;
    private List<int> levelList = new List<int> {1,3,5,7,9};
    
    private GameObject player;
    private bool infiniteMode = false;
    // private int cityCounter = 0;


    // Start is called before the first frame update
    void Start()
    {
        level = PlayerPrefs.GetInt("Level", 1);
        infiniteMode = PlayerPrefs.GetInt("Mode",0) == 1;
        player = GameObject.Find("Player");
        UpdateItemPatterns();

        if(!infiniteMode) length = 35 + 2*(int)PlayerPrefs.GetInt("UpgradeLevel0",0);
        else lengthInfinite = length;
        for (int i = 0; i < length; i++)
        {
            SpawnEverythingOneByOne(i);
            
        }
        EndBuilding.transform.position = new Vector3(0, 0, (length-1)*roadLength);
        if(infiniteMode) EndBuilding.SetActive(false);
        else EndBuilding.SetActive(true);
    }
    void Update()
    {
        if(Vector3.Distance(player.transform.position, cityBasePosition[0]) < DistanceToSpawnCityRoad)
        {
            SpawnCity();
        }
        if(roadLastShow.transform.position.z-player.transform.position.z < distanceToShow)
        {
            if(transform.childCount <= lastChildShowed) return;
            roadLastShow = transform.GetChild(lastChildShowed).gameObject;
            roadLastShow.SetActive(true);
            lastChildShowed++;
            if(infiniteMode)
            {
                SpawnEverythingOneByOne(lengthInfinite);
                lengthInfinite++;
            }
        }
    }
    private void SpawnEverythingOneByOne(int i)
    {
        SpawnRoad(i);
        //Show or not the road
        if(road.transform.position.z-player.transform.position.z > distanceToShow)
        {
            road.SetActive(false);
        }
        else
        {
            lastChildShowed = int.Parse(road.name[6..^1]);
            roadLastShow = road;
        }


        if(!infiniteMode && i >= length-3) return;
        if(i>2 && normalRoadCompter != 0 && doSpawn>0)
        {
            
            if(i%2 == 0) SpawnObjectsOnRoad(i);
            else 
            {
                int rdNumber = Random.Range(0, 4);
                if(rdNumber != 0) 
                {
                    SpawnGatesOnRoad();
                    SpawnCrystalOnRoad();
                }
                else SpawnCardBoardOnRoad();
            }
            doSpawn--;
        }
        else
        {
            doSpawn = 2;
        }
    }
    private void UpdateItemPatterns()
    {
        
        if(infiniteMode) 
        {
            itemPatterns = itemPatterns6;
            return;
        }
        if(level <= 2)
        {
            itemPatterns = itemPatterns1;
        }
        else if(level <= 4)
        {
            itemPatterns = itemPatterns2;
        }
        else if(level <= 6)
        {
            itemPatterns = itemPatterns3;
        }
        else if(level <= 8)
        {
            itemPatterns = itemPatterns4;
        }
        else
        {
            itemPatterns = itemPatterns5;
        }


    }
    private void SpawnCity()
    {
        GameObject cityRoad = Instantiate(cityRoadPrefab[0], cityBasePosition[0], Quaternion.identity);
        GameObject cityRoad2 = Instantiate(cityRoadPrefab[1], cityBasePosition[1], Quaternion.identity);
        cityRoad.transform.parent = transform.parent;
        cityRoad2.transform.parent = transform.parent;
        cityBasePosition[0] += new Vector3(0, 0, cityLength);
        cityBasePosition[1] += new Vector3(0, 0, cityLength);
        
        
        // if(cityCounter>=2)
        // {
        //     GameObject cityLeft = GameObject.Find("CityLeft ("+(cityCounter-2)+")");
        //     GameObject cityRight = GameObject.Find("CityRight ("+(cityCounter-2)+")");
        //     if(cityLeft != null) Destroy(cityLeft);
        //     if(cityRight != null) Destroy(cityRight);
        // }
        // cityCounter++;
        
    }

    private void SpawnRoad(int i)
    {
        if(normalRoadCompter == maxRoadCompter)
        {
            normalRoadCompter = 0;
            road = Instantiate(roadPrefab[1], new Vector3(0, 0, i * roadLength), Quaternion.identity);
            
        }
        else
        {
            normalRoadCompter++;
            road = Instantiate(roadPrefab[0], new Vector3(0, 0, i * roadLength), Quaternion.identity);
        }
        road.name = "Road ("+i+")";
        road.transform.parent = transform;
    }

    private void SpawnObjectsOnRoad(int i)
    {
        int rdNumber = Random.Range(0, itemPatterns.Length);
        if(i == 4 && levelList.Contains(level) && !infiniteMode) rdNumber = 0; // Spawn the new object at the 5th road
        GameObject item = Instantiate(itemPatterns[rdNumber], new Vector3(0, 0.5f, road.transform.position.z-3), Quaternion.identity);
        item.transform.parent = road.transform;
    }

    private void SpawnGatesOnRoad()
    {
        GameObject gate = Instantiate(gatePatterns[0], new Vector3(0, 1.8f, road.transform.position.z-5), Quaternion.identity);

        string gateName = gateNames[Random.Range(0, gateNames.Count)];
        float value = Random.Range(0f,6.5f);
        if(Random.Range(0,3) == 0) value = -value;
        if(gateName == "Triple") gateNames.Remove("Triple");
        gate.transform.GetChild(0).GetComponent<GateBehaviour>().Initiate(value, gateName, Random.Range(0,3) == 0, Random.Range(0,3) == 0);
        gate.transform.parent = road.transform;
    }

    private void SpawnCardBoardOnRoad()
    {
        GameObject cardBoard = Instantiate(CardBoard, new Vector3(0, 1.1f, road.transform.position.z-3), Quaternion.identity);
        cardBoard.transform.parent = road.transform;
    }
    private void SpawnCrystalOnRoad()
    {
        if(Random.Range(0, 40) != 0) return;
        GameObject crystal = Instantiate(CrystalPrefab, new Vector3(Random.Range(-2.9f,2.9f), 1f, road.transform.position.z-8f), Quaternion.identity);
        crystal.transform.parent = road.transform;
        crystal.transform.localScale = new Vector3(1f, 1f, 1f);
    }

}
