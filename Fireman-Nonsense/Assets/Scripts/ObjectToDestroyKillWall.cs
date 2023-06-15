using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToDestroyKillWall : MonoBehaviour
{
    private PauseMenuManager pauseMenuManager;

    private void Start()
    {
        pauseMenuManager = GameObject.Find("PauseMenu").GetComponent<PauseMenuManager>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.name == "Player")
        {
            pauseMenuManager.OpenEndOfLevel(true);
        }
    }
}
