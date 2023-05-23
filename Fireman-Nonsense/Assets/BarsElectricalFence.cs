using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsElectricalFence : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player touched the bars");
            GameObject player = other.gameObject;
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
    }
}
