using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerWallElectricFence : MonoBehaviour
{
    [SerializeField] private GameObject InvisbleWall;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player touched the killer wall");
            GameObject player = other.gameObject;
            player.GetComponent<PlayerHealth>().TakeDamage(50f);
            Destroy(InvisbleWall);
        }
    }
}
