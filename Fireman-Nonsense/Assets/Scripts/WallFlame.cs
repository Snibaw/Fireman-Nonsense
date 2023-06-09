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
    private AudioSource audioSource;
    [SerializeField] private GameObject[] UnactiveIfLowQuality;
    private int quality;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        player = GameObject.Find("Player");

        quality = PlayerPrefs.GetInt("Quality",0);
        if( quality == 0)
        {
            foreach (GameObject g in UnactiveIfLowQuality)
            {
                g.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player" && !isDead)
        {
            other.gameObject.GetComponent<playerInput>().ChangeCurrentMana(-damageToPlayer, Vibrator.vibrateTimeDamage);
            other.gameObject.GetComponent<playerInput>().HitWall();
            DestroyFlameOnWall(false);
        }
    }
    private void HitByRay()
    {
        if(isDead) return;
        currentHealth -= 2f+Mathf.Min(3,player.GetComponent<playerInput>().GetDamageAddition()*player.GetComponent<playerInput>().GetDamageMultiplier());
        if(currentHealth <= 0)
        {
            DestroyFlameOnWall(true);
        }
    }
    private void DestroyFlameOnWall(bool earnMana = true)
    {
        audioSource.Play();
        if(earnMana) 
        {
            player.GetComponent<playerInput>().ChangeCurrentMana(moneyEarnedWhenDestroyed, Vibrator.vibrateTimeItem);
            PlayerPrefs.SetInt("WaterWall",PlayerPrefs.GetInt("WaterWall",0)+1);
        }
        foreach (GameObject FlameEmitter in FlameEmitters)
        {
            FlameEmitter.SetActive(false);
            Flame.Stop();
            if(quality == 1)
            {
                GameObject ExplosionInstance = Instantiate(Explosion, new Vector3(transform.position.x,transform.position.y+1f,transform.position.z), Quaternion.identity);
                Destroy(ExplosionInstance, 2f);
            }
            isDead = true;
        }
    }
}
