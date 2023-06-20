using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFlame : MonoBehaviour
{

    [SerializeField] private GameObject[] FlameEmitters;
    [SerializeField] private ParticleSystem Flame;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private GameObject Explosion;
    private float currentHealth;
    private bool isDead = false;
    [SerializeField] private float damageToPlayer = 300f;

    private void Start() 
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player")
        {
            other.GetComponent<playerInput>().ChangeCurrentMana(-damageToPlayer);
            other.GetComponent<playerInput>().HitWall();
        }
    }
    private void HitByRay()
    {
        if(isDead) return;
        currentHealth -= 1;
        if(currentHealth <= 0)
        {
            foreach (GameObject FlameEmitter in FlameEmitters)
            {
                FlameEmitter.SetActive(false);
                Flame.Stop();
                GameObject ExplosionInstance = Instantiate(Explosion, new Vector3(transform.position.x,transform.position.y+1f,transform.position.z), Quaternion.identity);
                Destroy(ExplosionInstance, 2f);
                isDead = true;
            }
        }
    }
}
