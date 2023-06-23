using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System;
using UnityEngine;

public class Hovl_DemoLasers : MonoBehaviour
{
    [SerializeField] private bool isForCinematic = false;
    public GameObject FirePoint;
    public float MaxLength = 10;
    public float laserScale = 1;
    public GameObject[] Prefabs;
    public bool isShooting = false;
    [SerializeField] private bool isLaserShop = false;

    [Header("GUI")]

    public int Prefab;
    public GameObject Instance;
    private Hovl_Laser LaserScript;
    private Hovl_Laser2 LaserScript2;

    void Start ()
    {
        Prefab = PlayerPrefs.GetInt("Laser",0);
    }

    void Update()
    {
        if(isForCinematic || isLaserShop) return;
        //Enable lazer
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Start");
            StartShooting();
        }

        //Disable lazer prefab
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Stop");
            StopShooting();
        }
    }
    public void StartShooting()
    {
        isShooting = true;
        Destroy(Instance);
        Instance = Instantiate(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation);
        if(!isLaserShop)
        {
            Instance.GetComponent<Hovl_Laser2>().MaxLength = MaxLength; 
        }
        Instance.transform.parent = transform;
        LaserScript = Instance.GetComponent<Hovl_Laser>();
        LaserScript2 = Instance.GetComponent<Hovl_Laser2>();
        if(LaserScript2 != null) LaserScript2.laserScale = laserScale;
    }
    public void StopShooting()
    {
        isShooting = false;
        if (LaserScript) LaserScript.DisablePrepare();
        if (LaserScript2) LaserScript2.DisablePrepare();
        Destroy(Instance,1);
    }
} 
