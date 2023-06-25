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
    private GameObject player;
    [SerializeField] private float damageToPlayer = 300f;
    [SerializeField] private float moneyEarnedWhenDestroyed = 150f;

    private void Start() 
    {
        currentHealth = maxHealth;
        player = GameObject.Find("Player");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player" && !isDead)
        {
            other.gameObject.GetComponent<playerInput>().ChangeCurrentMana(-damageToPlayer);
            other.gameObject.GetComponent<playerInput>().HitWall();
        }
    }
    private void HitByRay()
    {
        if(isDead) return;
        currentHealth -= 2f+Mathf.Min(3,player.GetComponent<playerInput>().GetDamageAddition()*player.GetComponent<playerInput>().GetDamageMultiplier());
        if(currentHealth <= 0)
        {
            player.GetComponent<playerInput>().ChangeCurrentMana(moneyEarnedWhenDestroyed);
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
