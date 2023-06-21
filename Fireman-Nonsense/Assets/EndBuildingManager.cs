using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBuildingManager : MonoBehaviour
{
    [SerializeField] private GameObject EndClip;
    // Start is called before the first frame update
    void Start()
    {
        EndClip.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)) 
        {
            Debug.Log("Space");
            EndClip.SetActive(true);
        }
    }
}
