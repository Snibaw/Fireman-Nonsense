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
    private playerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        playerInput = GameObject.Find("Player").GetComponent<playerInput>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void HitByRay()
    {
        currentHealth -= PlayerPrefs.GetFloat("UpgradeValue2",0.01f)+1.5f+Mathf.Min(3,playerInput.GetDamageAddition()*playerInput.GetDamageMultiplier());
        if (currentHealth <= 0)
        {
            foreach (ParticleSystem p in ps)
            {
                p.Stop();
                GameObject ExplosionInstance = Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                Destroy(ExplosionInstance, 2f);
                Destroy(gameObject);
            }
        }

    }   
}
