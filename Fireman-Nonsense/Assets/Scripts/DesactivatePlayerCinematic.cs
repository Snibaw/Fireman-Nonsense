using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivatePlayerCinematic : MonoBehaviour
{
    private GameObject player;
    private GameObject camHolder;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player.GetComponent<Animator>().enabled = false;
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<playerInput>().enabled = false;
        player.GetComponent<Hovl_DemoLasers>().enabled = false;

        camHolder = GameObject.Find("Main Camera").transform.parent.gameObject;
        camHolder.GetComponent<MainCamera>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
