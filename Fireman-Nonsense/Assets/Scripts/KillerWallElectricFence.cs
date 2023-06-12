using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerWallElectricFence : MonoBehaviour
{
    [SerializeField] private float damageToPlayer = 400;
    [SerializeField] private GameObject InvisbleWall;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player touched the killer wall");
            GameObject player = other.gameObject;
            player.GetComponent<playerInput>().ChangeCurrentMana(-damageToPlayer);
            Destroy(InvisbleWall);
        }
    }
}
