using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenBehindCamera : MonoBehaviour
{
    [SerializeField] private float distanceToDestroy = 10f;
    [SerializeField] private bool isRoad = false;
    private CreateRoad createRoad;
    private GameObject camHolder;
    // Start is called before the first frame update
    void Start()
    {
        camHolder = Camera.main.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(camHolder.transform.position.z - transform.position.z > distanceToDestroy)
        {
            if(isRoad)
            {
                createRoad = GameObject.Find("Road").GetComponent<CreateRoad>();
                createRoad.lastChildShowed--;
            }
            Destroy(gameObject);
        }

            
    }
}
