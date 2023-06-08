using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToDestroyKillWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.name == "Player")
        {
            //End of level
            Destroy(other.gameObject);
        }
    }
}
