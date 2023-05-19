using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    public bool isDead = false;
    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Player took " + damage + " damage");
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die() 
    {
        isDead = true;
        Destroy(gameObject);
    }
}
