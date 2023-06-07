using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardScript : MonoBehaviour
{
    private void OnParticleCollision(GameObject other) 
    {
        if (other.name == "Water Steam")
        {
            Destroy(gameObject,Mathf.Round(Random.Range(0.1f,0.3f)*10));
        }
    }
}
