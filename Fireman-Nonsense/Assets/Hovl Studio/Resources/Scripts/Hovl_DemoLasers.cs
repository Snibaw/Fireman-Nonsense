using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System;
using UnityEngine;

public class Hovl_DemoLasers : MonoBehaviour
{
    public GameObject FirePoint;
    // public Camera Cam;
    // public float MaxLength;
    public GameObject[] Prefabs;

    // private Ray RayMouse;
    // private Vector3 direction;
    // private Quaternion rotation;

    [Header("GUI")]
    // private float windowDpi;

    private int Prefab;
    private GameObject Instance;
    private Hovl_Laser LaserScript;
    private Hovl_Laser2 LaserScript2;

    //Double-click protection
    // private float buttonSaver = 0f;

    void Start ()
    {
        Prefab = PlayerPrefs.GetInt("Laser", 0);
    }

    public void StartShooting()
    {
        Destroy(Instance);
        Instance = Instantiate(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation);
        Instance.transform.parent = transform;
        LaserScript = Instance.GetComponent<Hovl_Laser>();
        LaserScript2 = Instance.GetComponent<Hovl_Laser2>();
    }
    
    public void StopShooting()
    {   
        if (LaserScript) LaserScript.DisablePrepare();
        if (LaserScript2) LaserScript2.DisablePrepare();
        Destroy(Instance,0.1f);
    }

}
