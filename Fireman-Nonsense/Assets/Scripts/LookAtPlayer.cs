using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private GameObject player;
    void Awake()
    {
        player = GameObject.Find("Player");
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
    }
}
