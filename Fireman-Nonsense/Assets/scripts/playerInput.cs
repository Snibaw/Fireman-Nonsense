using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using UnityEngine.UI;

public class playerInput : MonoBehaviour
{
    // [SerializeField] private float manaLossPerFrame = 1;
    
    [SerializeField] private PauseMenuManager pauseMenuManager;
    [SerializeField] private bool isBossLevel = false;
    [SerializeField] private Slider manaBarSlider;
    [SerializeField] private Animator fillAnimator;
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
    [SerializeField] private float xBorderCoo = 5f;

    private float testingTimer = 0.1f;
    private float timer = 0f;
    [SerializeField] private float maxMovement = 40f;

    private Touch touch;
    [SerializeField ] private float speedModifier = 0.01f;

    private Animator playerAnimator;
    private bool canMove = true;
    private float speed;
    [SerializeField] private ParticleSystem[] Water_Steam;
    [SerializeField] private int numberOfWaterSteam = 1;

    void Awake()
    {
        if(isTesting)
        {
            damageAddition = 3;
        }
        rb = GetComponent<Rigidbody>();
        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        currentMana = 0;
        StopWaterSteam();
        timer = testingTimer;
        playerAnimator = GetComponent<Animator>();

        damageAddition = PlayerPrefs.GetInt("DamageAddition",1)*0.01f;
        ChangeWaterSteamRange();
        // var ParticleMain = Water_Steam.main;
        // ParticleMain.startSpeed = PlayerPrefs.GetInt("RangeLevel",1)*3 + 17;
    }
    private void FixedUpdate() {      
        // The player can't get out of the map
        speed = 6.5f+ transform.position.z/50;
        if(!isBossLevel && canMove) rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y,speed);

        if(transform.position.x >= xBorderCoo)
        {
            transform.position = new Vector3(xBorderCoo,transform.position.y,transform.position.z);
            rb.velocity = new Vector3(0,rb.velocity.y,rb.velocity.z);
        }
        else if(transform.position.x <= -xBorderCoo)
        {
            transform.position = new Vector3(-xBorderCoo,transform.position.y,transform.position.z);
            rb.velocity = new Vector3(0,rb.velocity.y,rb.velocity.z);
        }
        
        if(Input.touchCount > 0 && canMove)
        {
            StartWaterSteam();
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved)
            {
                float deltaPosition = touch.deltaPosition.x;
                if(deltaPosition > maxMovement) deltaPosition = maxMovement;
                else if(deltaPosition < -maxMovement) deltaPosition = -maxMovement;


                //Smooth movement
                if(deltaPosition > 10 )
                {
                    playerAnimator.SetBool("RightMovement", true);
                    playerAnimator.SetBool("LeftMovement", false);
                }
                else if(deltaPosition < -10)
                {
                    playerAnimator.SetBool("RightMovement", false);
                    playerAnimator.SetBool("LeftMovement", true);
                }
                else
                {
                    playerAnimator.SetBool("RightMovement", false);
                    playerAnimator.SetBool("LeftMovement", false);
                }

                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + deltaPosition*speedModifier,transform.position.y,transform.position.z), 0.1f);
            }
            else
            {
                playerAnimator.SetBool("RightMovement", false);
                playerAnimator.SetBool("LeftMovement", false);
            }
        }
        else
        {
            StopWaterSteam();
            playerAnimator.SetBool("RightMovement", false);
            playerAnimator.SetBool("LeftMovement", false);
        }

        //Update the mana bar, Smooth movement to the new value
        manaBarSlider.value = Mathf.Lerp(manaBarSlider.value, currentRateOverTime, 0.1f);
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


    public void ChangeCurrentMana(float manaGain)
    {
        currentMana += manaGain;

        if(currentMana>maxMana)
        {
            currentMana = maxMana;
        }
        
        //Shake the camera
        CameraShaker.Instance.ShakeOnce(.5f,.5f,.1f,1f);

        currentRateOverTime = 200 + (currentMana/maxMana)*600;
        if(currentRateOverTime >= 800)
        {
            currentRateOverTime = 800;
            fillAnimator.SetBool("Fill", true);
        }
        else
        {
            fillAnimator.SetBool("Fill", false);
        }

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
            if(this.damageAddition<=0.1f) this.damageAddition = 0.1f;
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
    public void HitWall()
    {
        canMove = false;
        playerAnimator.SetTrigger("HitWall");
        rb.velocity = new Vector3(0,rb.velocity.y,-speed*1.5f);
    }
    public void GetUp()
    {
        canMove = true;
    }
    public void UpdateParticleRateOverTime()
    {
        for(int i =0; i < Water_Steam.Length; i++)
        {
            var ParticleEmission = Water_Steam[i].emission;
            ParticleEmission.rateOverTime = (currentRateOverTime + RateOverTimeAddition)*RateOverTimeMultiplier ;
        }
    }
    private void StartWaterSteam()
    {
        for(int i =0; i < numberOfWaterSteam; i++)
        {
            Water_Steam[i].Play();
        }
    }
    private void StopWaterSteam()
    {
        for(int i =0; i < Water_Steam.Length; i++)
        {
            Water_Steam[i].Stop();
        }
    }
    private void ChangeWaterSteamRange()
    {
        for(int i = 0; i< Water_Steam.Length; i++)
        {
            var ParticleMain = Water_Steam[i].main;
            ParticleMain.startSpeed = PlayerPrefs.GetInt("RangeLevel",1)*3 + 17;
        }
    }
    public void SetNumberOfWaterSteam(int number)
    {
        numberOfWaterSteam = number;
    }
}
