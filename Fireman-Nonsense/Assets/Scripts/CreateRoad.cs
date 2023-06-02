using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoad : MonoBehaviour
{
    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private float length;
    private float roadLength = 10;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < length; i++)
        {
            GameObject road = Instantiate(roadPrefab, new Vector3(0, 0, i * roadLength), Quaternion.identity);
            road.transform.parent = transform;
        }
    }

}
