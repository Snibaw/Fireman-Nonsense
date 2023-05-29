using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoucheIncendie : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // other.GetComponent<playerInput>().GetBoucheIncendie();
            Destroy(gameObject);
        }
    }
}
