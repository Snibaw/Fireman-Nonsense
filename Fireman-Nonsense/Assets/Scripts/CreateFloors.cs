using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFloors : MonoBehaviour
{
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private int numberOfFloors = 10;
    private float heightOfFloor = 2.7f;
    [SerializeField] private GameObject[] flamePrefabs;
    private GameObject[] flamePrefabsToUse;
    [SerializeField] private float DecreasingWaitingTimeBtwFloor =0.1f;
    [SerializeField] private float MinWaitingTimeBtwFloor = 0.3f;
    public bool instantSpawn = false;
    [SerializeField] private int quality;
    // Start is called before the first frame update
    void Start()
    {
        flamePrefabsToUse = flamePrefabs;
        numberOfFloors = (int)PlayerPrefs.GetFloat("UpgradeValue1",50);
        quality = PlayerPrefs.GetInt("Quality",0);
    }
    private void SpawnFlame(int i)
    {
        int rdNumber = Random.Range(0,flamePrefabsToUse.Length);
        GameObject flame = Instantiate(flamePrefabsToUse[rdNumber], new Vector3(transform.position.x, 4.8f + i*heightOfFloor,transform.position.z), Quaternion.Euler(0, 0, 0));
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
        GameObject floor = Instantiate(floorPrefab, new Vector3(transform.position.x, 4.8f + i*heightOfFloor,transform.position.z), Quaternion.Euler(-90, 0, 0));
        floor.name = "Floor (" + i+")";
        floor.transform.parent = this.transform;
        if(Random.Range(0,3) != 0 && quality == 1 ) SpawnFlame(i);
    }
    public IEnumerator SpawnFloorWithDelay()
    {
        for(int i= 0; i < numberOfFloors; i++)
        {
            if(instantSpawn) SpawnFloor(i);
            else
            {
                yield return new WaitForSeconds(Mathf.Max(0.3f-i*DecreasingWaitingTimeBtwFloor,MinWaitingTimeBtwFloor));
                SpawnFloor(i);
            }
        }
    }

}
