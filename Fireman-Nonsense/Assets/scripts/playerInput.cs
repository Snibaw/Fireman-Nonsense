using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class playerInput : MonoBehaviour
{
    // [SerializeField] private float manaLossPerFrame = 1;
    [SerializeField] private PauseMenuManager pauseMenuManager;
    [SerializeField] private bool isBossLevel = false;
    public UIBarScript ManaBarScript;
    private ParticleSystem Water_Steam;
    private Rigidbody rb;
    private CameraShake cameraShake;
    private float maxMana = 1000;
    private float currentMana;
    public bool isTesting = false;


    private float currentRateOverTime = 200;
    private float RateOverTimeMultiplier = 1;
    private float RateOverTimeAddition = 0;
    private float damageMultiplier = 1;
    private float damageAddition = 0.01f; // 0.01 for test, 0 else
    private float xBorderCoo = 5f;

    private float testingTimer = 0.1f;
    private float timer = 0f;
    [SerializeField] private float maxMovement = 40f;

    private Touch touch;
    [SerializeField ] private float speedModifier = 0.01f;
    void Awake()
    {
        if(isTesting)
        {
            damageAddition = 3;
        }
        rb = GetComponent<Rigidbody>();
        Water_Steam = GameObject.Find("Water Steam").GetComponent<ParticleSystem>();
        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        currentMana = 0;
        Water_Steam.Stop();
        timer = testingTimer;
    }
    private void FixedUpdate() {      
        // The player can't get out of the map
        if(!isBossLevel) rb.velocity = new Vector3(rb.velocity.x,0,5);

        if(transform.position.x >= xBorderCoo)
        {
            transform.position = new Vector3(xBorderCoo,transform.position.y,transform.position.z);
            rb.velocity = new Vector3(0,0,rb.velocity.z);
        }
        else if(transform.position.x <= -xBorderCoo)
        {
            transform.position = new Vector3(-xBorderCoo,transform.position.y,transform.position.z);
            rb.velocity = new Vector3(0,0,rb.velocity.z);
        }
        
        if(Input.touchCount > 0)
        {
            
            Water_Steam.Play();
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved)
            {
                float deltaPosition = touch.deltaPosition.x;
                if(deltaPosition > maxMovement) deltaPosition = maxMovement;
                else if(deltaPosition < -maxMovement) deltaPosition = -maxMovement;


                //Smooth movement
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + deltaPosition*speedModifier,transform.position.y,transform.position.z), 0.1f);
            }
        }
        else
        {
            Water_Steam.Stop();
        }


        // Shoot water if left mouse button is pressed
        // if (Input.GetKey(KeyCode.Mouse0))
        // {   Water_Steam.Play();
        //     isShooting = true;
            // Make the player get knocked back when shooting water depending on its rotation
            // Vector3 direction = new Vector3(-transform.forward.x,0,0);
            // rb.AddForce(direction*directionMultiplier);

            //Lose mana
            // currentMana -= manaLossPerFrame;
            // ManaBarScript.UpdateValue((int)Mathf.Round(currentMana), (int)Mathf.Round(maxMana));

            
        // }
        // else
        // {
        //     Water_Steam.Stop();
        //     isShooting = false;
        // }
        if(currentMana <= 0)
        {
            currentMana = 0;
            // Water_Steam.Stop();
        }

        // if(!isShooting)
        // {
        //     transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(new Vector3(0,0,0)),Time.deltaTime*5);
        // }
    }


    public void PickUpItems(float manaGain)
    {
        currentMana += manaGain;
        //Update the mana bar
        ManaBarScript.UpdateValue(currentMana/maxMana);
        //Shake the camera
        CameraShaker.Instance.ShakeOnce(.5f,.5f,.1f,1f);

        currentRateOverTime = 200 + (currentMana/maxMana)*600;
        UpdateParticleRateOverTime();
    }
    public void ChangeParticleRateOverTimeValues(float rate, bool isMultiplier = false)
    {
        if(isMultiplier)
        {
            if(rate <= 0)
            {
                this.RateOverTimeMultiplier /= -rate;
            }
            else
            {
                this.RateOverTimeMultiplier *= rate;
            }
        }
        else
        {
            this.RateOverTimeAddition += rate;
        }
    }
    public void UpdateParticleRateOverTime()
    {
        var ParticleEmission = Water_Steam.emission;
        ParticleEmission.rateOverTime = (currentRateOverTime + RateOverTimeAddition)*RateOverTimeMultiplier ;
    }
    public void ChangeDamageValues(float damage, bool isMultiplier = false)
    {
        if(isMultiplier)
        {
            if(damage <= 0)
            {
                this.damageMultiplier /= -damage;
            }
            else
            {
                this.damageMultiplier *= damage;
            }
        }
        else
        {
            this.damageAddition += damage;
        }
    }
    public float GetDamageMultiplier()
    {
        return damageMultiplier;
    }
    public float GetDamageAddition()
    {
        return damageAddition;
    }
}
