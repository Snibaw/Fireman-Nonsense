using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInput : MonoBehaviour
{
    private ParticleSystem Water_Steam;
    void Awake()
    {
        Water_Steam = GameObject.Find("Water Steam").GetComponent<ParticleSystem>();
    }
 // Update is called once per frame
 void Update()
 {
    if (Input.GetKey(KeyCode.Mouse0))
    {
         Water_Steam.Play();
    }
    else
    {
        Water_Steam.Stop();
    }
 }
}
