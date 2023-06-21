using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFloors : MonoBehaviour
{
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private int numberOfFloors = 10;
    private float heightOfFloor = 2.7f;
    public float increment = 50f;
    // Start is called before the first frame update
    void Start()
    {
        //Spawn Floor on top of each others
        for(int i = 0; i < numberOfFloors; i++)
        {
            //rotate -90
            GameObject floor = Instantiate(floorPrefab, new Vector3(0, 4.8f + i*heightOfFloor,0), Quaternion.Euler(-90, 0, 0));
            floor.name = "Floor (" + i+")";
            floor.transform.parent = this.transform;
        }
    }

}
