using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0,0,0);
    void Awake()
    {
        player = GameObject.Find("Player");
    }
    // Update is called once per frame
    void Update()
    {
        // Make the camera smoothly follow the player
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + cameraOffset.x,player.transform.position.y + cameraOffset.y,player.transform.position.z + cameraOffset.z), 0.5f);


        // transform.position = new Vector3(player.transform.position.x + cameraOffset.x,player.transform.position.y + cameraOffset.y,player.transform.position.z + cameraOffset.z);
    }
}
