using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    public GameObject healthBar;
    // [SerializeField] private float xKnockbackMultiplier = 15;
    private Rigidbody rb;
    public bool isDead = false;
    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
        //rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Player took " + damage + " damage");
        currentHealth -= damage;

        //Update the health bar
        healthBar.GetComponent<UIBarScript>().UpdateValue((int)Mathf.Round(currentHealth), (int)Mathf.Round(maxHealth));

        if(currentHealth <= 0)
        {
            Die();
        }
        else
        {
            //Shake the camera
            CameraShaker.Instance.ShakeOnce(5f,5f,.1f,1f);
        }
    }
    private void Die() 
    {
        isDead = true;
        Destroy(gameObject);
    }
}
