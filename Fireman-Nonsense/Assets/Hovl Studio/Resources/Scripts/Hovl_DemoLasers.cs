using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hovl_DemoLasers : MonoBehaviour
{
    [SerializeField] private bool isForCinematic = false;
    public GameObject FirePoint;
    public float MaxLength = 10;
    public float laserScale = 1;
    public GameObject[] Prefabs;
    public bool isShooting = false;
    public bool isTriple = false;
    [SerializeField] private bool isLaserShop = false;

    [Header("GUI")]

    public int Prefab;
    private GameObject[] InstanceSup;
    public GameObject Instance;
    private Hovl_Laser LaserScript;
    private Hovl_Laser2 LaserScript2;
    private Hovl_Laser2[] LaserScriptSup2;
    private Hovl_Laser[] LaserScriptSup;
    private bool isBossScene = false;
    private bool canMove;
    private bool isHovlLaser2 = true;

    private playerInput playerInput;
    private PlayerInputBossLevel playerInputBossLevel;
    void Start ()
    {
        if(!isForCinematic) MaxLength = PlayerPrefs.GetFloat("UpgradeValue3",20);
        isBossScene= SceneManager.GetActiveScene().name == "BossScene";
        Prefab = PlayerPrefs.GetInt("Laser",0);
        if(Prefab == 1 ||Prefab == 2 || Prefab == 3 ||Prefab == 4) isHovlLaser2 = false;

        if(isBossScene) playerInputBossLevel = GetComponent<PlayerInputBossLevel>();
        else playerInput = GetComponent<playerInput>();
        StopShooting();
        if(isLaserShop) StartShooting();
    }

    void Update()
    {
        if(isForCinematic || isLaserShop) return;

        if(isBossScene) canMove = playerInputBossLevel.canMove;
        else canMove = playerInput.canMove;

        //Enable lazer
        if (Input.GetMouseButtonDown(0) && canMove)
        {
            StartShooting();
        }

        //Disable lazer prefab
        if (Input.GetMouseButtonUp(0) || !canMove)
        {
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
            if(!isHovlLaser2) Instance.GetComponent<Hovl_Laser>().MaxLength = MaxLength; 
            else Instance.GetComponent<Hovl_Laser2>().MaxLength = MaxLength; 
        }
        Instance.transform.parent = transform;
        LaserScript = Instance.GetComponent<Hovl_Laser>();
        LaserScript2 = Instance.GetComponent<Hovl_Laser2>();
        if(LaserScript2 != null) LaserScript2.laserScale = laserScale;

        if(isTriple)
        {
            InstanceSup = new GameObject[2];
            LaserScriptSup2 = new Hovl_Laser2[2];
            LaserScriptSup = new Hovl_Laser[2];

            InstanceSup[0] = Instantiate(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation);
            InstanceSup[1] = Instantiate(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation);
            
            InstanceSup[0].transform.Rotate(0, 12,0);
            InstanceSup[1].transform.Rotate(0,-12,0);

            InstanceSup[0].transform.parent = transform;
            InstanceSup[1].transform.parent = transform;

            if(isHovlLaser2)
            {
                LaserScriptSup2[0] = InstanceSup[0].GetComponent<Hovl_Laser2>();
                LaserScriptSup2[1] = InstanceSup[1].GetComponent<Hovl_Laser2>();

                LaserScriptSup2[0].MaxLength = MaxLength;
                LaserScriptSup2[1].MaxLength = MaxLength;

                LaserScriptSup2[0].laserScale = laserScale;
                LaserScriptSup2[1].laserScale = laserScale;
            }
            else
            {
                LaserScriptSup[0] = InstanceSup[0].GetComponent<Hovl_Laser>();
                LaserScriptSup[1] = InstanceSup[1].GetComponent<Hovl_Laser>();

                LaserScriptSup[0].MaxLength = MaxLength;
                LaserScriptSup[1].MaxLength = MaxLength;
            }

        }
    }
    public void StopShooting()
    {
        isShooting = false;
        if (LaserScript) LaserScript.DisablePrepare();
        if (LaserScript2) LaserScript2.DisablePrepare();
        Destroy(Instance,0.1f);

        if(isTriple)
        {
            if(LaserScriptSup2[0]) LaserScriptSup2[0].DisablePrepare();
            if(LaserScriptSup2[1]) LaserScriptSup2[1].DisablePrepare();
            Destroy(InstanceSup[0],0.1f);
            Destroy(InstanceSup[1],0.1f);
        }
    }
    public void ResetSteam()
    {    
        if(isShooting)
        {
            StopShooting();
            StartShooting();
        } 
    }
} 
