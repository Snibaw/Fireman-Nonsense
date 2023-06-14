using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFlame : MonoBehaviour
{
    [SerializeField] private float damageToPlayer = 300f;
    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player")
        {
            other.GetComponent<playerInput>().ChangeCurrentMana(-damageToPlayer);
            other.GetComponent<playerInput>().HitWall();
            // other.transform.position = new Vector3(other.transform.position.x,other.transform.position.y,other.transform.position.z+1);
        }
    }
}
