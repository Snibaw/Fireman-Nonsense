using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using UnityEngine.UI;
using TMPro;

public class playerInput : MonoBehaviour
{
    // [SerializeField] private float manaLossPerFrame = 1;
    private Hovl_DemoLasers hovl_DemoLasers;
    [SerializeField] private bool isBossLevel = false;
    [SerializeField] private Slider manaBarSlider;
    [SerializeField] private Animator fillAnimator;
    private MeterScript meterScript;
    private Rigidbody rb;
    private CameraShake cameraShake;
    private float maxMana;
    public float currentMana;
    public bool isTesting = false;


    private float damageMultiplier = 1;
    private float damageAddition = 0.01f; // 0.01 for test, 0 else
    [SerializeField] private float xBorderCoo = 5f;

    private float testingTimer = 0.1f;
    private float timer = 0f;
    [SerializeField] private float maxMovement = 40f;

    private Touch touch;
    [SerializeField ] private float speedModifier = 0.01f;

    private Animator playerAnimator;
    public bool canMove = true;
    private float speed;
    

    [Header("INFINITE MODE")] 
    [SerializeField] public bool infiniteMode = false;
    [SerializeField] private float infiniteManaLoss = 0.1f;
    [SerializeField] private TMP_Text scoreText;
    private PauseMenuManager pauseMenuManager;
    private float moneyEarnedDuringInfiniteMode = 0;
    [SerializeField] private TMP_Text moneyEarnedText;
    private GameManager gameManager;


    void Awake()
    {
        meterScript = manaBarSlider.gameObject.GetComponent<MeterScript>();
        rb = GetComponent<Rigidbody>();
        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        maxMana = PlayerPrefs.GetFloat("UpgradeValue0",1000);
        currentMana = 0;
        // StopWaterSteam();
        timer = testingTimer;
        playerAnimator = GetComponent<Animator>();

        damageAddition = PlayerPrefs.GetFloat("UpgradeValue2",0.01f);

        hovl_DemoLasers = GetComponent<Hovl_DemoLasers>();
        if(infiniteMode) 
        {
            currentMana = maxMana;
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            pauseMenuManager = GameObject.Find("PauseMenu").GetComponent<PauseMenuManager>();
        }

    }
    private void FixedUpdate() { 
     
        // The player can't get out of the map
        PlayerMovement();
        
        PlayerShoot();

        //Update the mana bar, Smooth movement to the new value
        if(infiniteMode) 
        {
            currentMana -= infiniteManaLoss + transform.position.z/300;
            scoreText.text = ((int)transform.position.z).ToString();
        }

        manaBarSlider.value = Mathf.Lerp(manaBarSlider.value, currentMana/maxMana, 0.1f);
        if(currentMana <= 0)
        {
            if(infiniteMode) pauseMenuManager.OpenEndOfLevel(true,false);
            currentMana = 0;
        }
    }

    private void PlayerMovement()
    {
        speed = 10f+ transform.position.z/50;
        if(!isBossLevel && canMove) 
        {
            if(infiniteMode) rb.velocity = new Vector3(rb.velocity.x,0,speed);
            else rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y,speed);
        }

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
    }
    
    private void PlayerShoot()
    {
        if(Input.touchCount > 0 && canMove)
        {
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
            playerAnimator.SetBool("RightMovement", false);
            playerAnimator.SetBool("LeftMovement", false);
        }
    }


    public void ChangeCurrentMana(float manaGain, long vibrateTime = 0)
    {

        if(manaGain < 0) manaGain *= Mathf.Max(1-PlayerPrefs.GetFloat("UpgradeValue4",0),0.3f);
        else if(infiniteMode)
        {
            gameManager.EarnMoney((int)Mathf.Round(manaGain/10));
            manaGain *= 1.5f;
        }
        currentMana += manaGain;

        if(currentMana>=maxMana)
        {
            currentMana = maxMana;
            if(!infiniteMode) fillAnimator.SetBool("Fill", true);
        }
        else
        {
            fillAnimator.SetBool("Fill", false);
        }

        //Water animation top left corner
        meterScript.WaterAnimation(Mathf.Round(manaGain));


        //Change the scale of the steam
        hovl_DemoLasers.laserScale = 1 + 2f*currentMana/maxMana;
        hovl_DemoLasers.ResetSteam();

        Vibrator.Vibrate(vibrateTime);

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
        rb.velocity = new Vector3(0,rb.velocity.y,-5);
    }
    public void GetUp()
    {
        canMove = true;
    }
}
