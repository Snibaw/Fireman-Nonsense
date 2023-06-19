using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallKillFlame : MonoBehaviour
{
    [SerializeField] private GameObject[] FlameEmitters;
    [SerializeField] private ParticleSystem Flame;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private GameObject Explosion;
    private float currentHealth;
    private bool isDead = false;

    private void Start() 
    {
        currentHealth = maxHealth;
    }

    private void OnParticleCollision(GameObject other) 
    {
        if(isDead) return;
        if (other.name == "Water Steam")
        {
            currentHealth -= 1;
        }
        foreach (GameObject FlameEmitter in FlameEmitters)
        {
            FlameEmitter.SetActive(false);
            Flame.Stop();
            GameObject ExplosionInstance = Instantiate(Explosion, new Vector3(transform.position.x,transform.position.y+0.5f,transform.position.z), Quaternion.identity);
            Destroy(ExplosionInstance, 2f);
            isDead = true;
        }
    }
}
