using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class ElectricFenceWall : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] ps;
    [SerializeField] private GameObject Explosion;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private GameObject[] ElectricityFenceEffect;
    private int quality;
    private float currentHealth;
    private playerInput playerInput;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        playerInput = GameObject.Find("Player").GetComponent<playerInput>();
        audioSource = transform.parent.GetComponent<AudioSource>();
        quality = PlayerPrefs.GetInt("Quality",0);
        if(quality == 0)
        {
            foreach (GameObject g in ElectricityFenceEffect)
            {
                g.SetActive(false);
            }
        }
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
            audioSource.Play();
            foreach (ParticleSystem p in ps)
            {
                p.Stop();
                if(quality == 1)
                {
                    GameObject ExplosionInstance = Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                    Destroy(ExplosionInstance, 2f);
                }
                Destroy(gameObject);
            }
        }

    }   
}
