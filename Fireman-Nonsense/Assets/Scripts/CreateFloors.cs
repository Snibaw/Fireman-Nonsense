using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFloors : MonoBehaviour
{
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private int numberOfFloors = 10;
    private float heightOfFloor = 2.7f;
    public float increment = 50f;
    [SerializeField] private GameObject[] flamePrefabs;
    private GameObject[] flamePrefabsToUse;
    [SerializeField] private float DecreasingWaitingTimeBtwFloor =0.1f;
    [SerializeField] private float MinWaitingTimeBtwFloor = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        flamePrefabsToUse = flamePrefabs;
    }
    private void SpawnFlame(int i)
    {
        int rdNumber = Random.Range(0,flamePrefabsToUse.Length);
        GameObject flame = Instantiate(flamePrefabsToUse[rdNumber], new Vector3(0, 4.8f + i*heightOfFloor,0), Quaternion.Euler(0, 0, 0));
        flame.name = "Flame (" + i+")";
        flame.transform.parent = this.transform;
        //Remove the flame from the list
        List<GameObject> tempList = new List<GameObject>();
        for(int j = 0; j < flamePrefabsToUse.Length; j++)
        {
            if(j != rdNumber) tempList.Add(flamePrefabsToUse[j]);
        }
        flamePrefabsToUse = tempList.ToArray();
        if( flamePrefabsToUse.Length == 0) flamePrefabsToUse = flamePrefabs;
    }
    private void SpawnFloor(int i)
    {
        //rotate -90
        GameObject floor = Instantiate(floorPrefab, new Vector3(0, 4.8f + i*heightOfFloor,0), Quaternion.Euler(-90, 0, 0));
        floor.name = "Floor (" + i+")";
        floor.transform.parent = this.transform;
        if(Random.Range(0,3) != 0 ) SpawnFlame(i);
    }
    public IEnumerator SpawnFloorWithDelay()
    {
        for(int i= 0; i < numberOfFloors; i++)
        {
            yield return new WaitForSeconds(Mathf.Max(0.3f-i*DecreasingWaitingTimeBtwFloor,MinWaitingTimeBtwFloor));
            SpawnFloor(i);
        }
    }

}
