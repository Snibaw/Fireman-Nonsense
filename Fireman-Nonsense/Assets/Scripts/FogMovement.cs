using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogMovement : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    private Transform playerTransform;
    // Update is called once per frame
    void Update()
    {
        playerTransform = GameObject.Find("Player").transform;
        transform.position = new Vector3(offset.x, offset.y, playerTransform.position.z+offset.z);
    }
}
