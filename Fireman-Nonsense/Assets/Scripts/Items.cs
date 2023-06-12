using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private float manaGain = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<playerInput>().ChangeCurrentMana(manaGain);
            Destroy(gameObject);
        }
    }
}
