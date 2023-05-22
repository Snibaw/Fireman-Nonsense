using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class ElectricFenceWall : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] ps;
    [SerializeField] private GameObject Explosion;
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnParticleCollision(GameObject other) {
        if (other.name == "Water Steam")
        {
            currentHealth -= 1;
        }
        if (currentHealth <= 0)
        {
            foreach (ParticleSystem p in ps)
            {
                p.Stop();
                GameObject ExplosionInstance = Instantiate(Explosion, transform.position, Quaternion.identity);
                Destroy(ExplosionInstance, 2f);
                Destroy(gameObject);
            }
        }

    }   
}
