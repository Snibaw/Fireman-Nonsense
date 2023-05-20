using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    // [SerializeField] private float xKnockbackMultiplier = 15;
    private CameraShake cameraShake;
    private Rigidbody rb;
    public bool isDead = false;
    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        //rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Player took " + damage + " damage");
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
        else
        {
            //Shake the camera
            StartCoroutine(cameraShake.Shake(0.2f,0.3f));




            //Get knocked back
            // if(enemyPosition != Vector3.zero)
            // {
            //     rb.AddForce(new Vector3(-enemyPosition.x,0,0)*xKnockbackMultiplier,ForceMode.Impulse);
            // }
        }
    }
    private void Die() 
    {
        isDead = true;
        Destroy(gameObject);
    }
}
