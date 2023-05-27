using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraMovementCapCut : MonoBehaviour
{
    public GameObject[] checkPoints;
    public float[] speed;
    private int index = 0;
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position != checkPoints[index].transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, checkPoints[index].transform.position, speed[index] * Time.deltaTime);
        }
        else
        {
            index++;
            if (index >= checkPoints.Length)
            {
                index = 0;
            }
        }
    }

}
