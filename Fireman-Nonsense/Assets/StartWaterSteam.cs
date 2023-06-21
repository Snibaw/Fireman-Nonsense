using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWaterSteam : MonoBehaviour
{
    [SerializeField] private GameObject[] sphereWaterSteam;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < sphereWaterSteam.Length; i++)
        {
            sphereWaterSteam[i].GetComponent<Hovl_DemoLasers>().StartShooting();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
