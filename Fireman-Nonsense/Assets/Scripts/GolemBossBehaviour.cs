using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using System.Linq;
using UnityEngine.UI;

public class GolemBossBehaviour : MonoBehaviour
{
    [SerializeField] private PauseMenuManager pauseMenuManager;
    [SerializeField] private GameObject[] FireWorks;
    [SerializeField] private GameObject[] HitPoints;
    private int HitPointIndex;
    [SerializeField] private Slider slider;
    [SerializeField] private float maxHealth;
    private float currentHealth;

    [SerializeField] private GameObject[] targets;
    [SerializeField] private Vector3[] targetsPosition;
    [SerializeField] private GameObject EarthShatterEffect;
    [SerializeField] private Vector3 EarthShatterPosition;
    [SerializeField] private float timeBtwActions;
    private int[] listActions = new[] {1,2,3,4,5,6};
    private Animator animator;
    private bool isDoingAction = false;
    private bool isDefending = false;
    private int quality;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(WaitForTheStart());
        CameraShaker.Instance.ShakeOnce(5f,5f,2f,3f);
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
        quality = PlayerPrefs.GetInt("Quality", 0);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        for(int i = 0; i < HitPoints.Length; i++)
        {
            HitPoints[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isDoingAction == false)
        {
            StartCoroutine(DoAction());
            isDoingAction = true;
        }
    }
    public void TakeDamage(float damage)
    {
        if(isDefending == false)
        {
            currentHealth -= damage;
            slider.value = currentHealth;
            if(currentHealth <= 0)
            {
                StartCoroutine(Die());
            }
        }
    }



    private IEnumerator WaitForTheStart()
    {
        isDefending = true;
        isDoingAction = true;
        yield return new WaitForSeconds(2f);
        isDoingAction = false;
        isDefending = false;
    }
    private IEnumerator DoAction()
    {
        isDoingAction = true;
        yield return new WaitForSeconds(timeBtwActions);
        if(listActions.Length == 0)
        {
            listActions = new[] {1,2,3,4,5,6};
        }
        int actionIndex = Random.Range(0,listActions.Length);
        int action = listActions[actionIndex];
        HitPoints[HitPointIndex].SetActive(false);
        if(action <= 3)
        {
            HitPointIndex = Random.Range(0,HitPoints.Length);
            HitPoints[HitPointIndex].SetActive(true);
        }
        switch (action)
        {
            case 1:
                StartCoroutine(Attack1());
                break;
            case 2:
                StartCoroutine(Attack2());
                break;
            case 3:
                StartCoroutine(Attack3());
                break;
            case 4:
                StartCoroutine(Defense1());
                break;
            case 5:
                StartCoroutine(Defense2());
                break;
            case 6:
                StartCoroutine(Defense3());
                break;
            default:
                break;
            }
        listActions = listActions.Where(val => val != action).ToArray();
        yield return new WaitForSeconds(timeBtwActions);
        isDoingAction = false;
    }
    private IEnumerator Attack1()
    {
        Instantiate(targets[0],targetsPosition[0],Quaternion.identity);
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Attack1");
    }
    private IEnumerator Attack2()
    {
        Instantiate(targets[1],targetsPosition[1],Quaternion.Euler(0,90,0));
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Attack2");
    }
    private IEnumerator Attack3()
    {
        Instantiate(targets[2],targetsPosition[2],Quaternion.Euler(0,45,0));
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("Attack3");
        yield return new WaitForSeconds(0.8f);
        if(quality == 1)Instantiate(EarthShatterEffect,EarthShatterPosition,Quaternion.Euler(0,130,0));
    }
    private IEnumerator Defense1()
    {
        isDefending = true;
        animator.SetBool("Defense1",isDefending);
 
        CameraShaker.Instance.ShakeOnce(5,5f,2f,2f);
        yield return new WaitForSeconds(3f);
        CameraShaker.Instance.ShakeOnce(5,5f,1f,1f);
        isDefending = false;
        animator.SetBool("Defense1",isDefending);
    }
    private IEnumerator Defense2()
    {
        isDefending = true;
        animator.SetBool("Defense2",isDefending);
        CameraShaker.Instance.ShakeOnce(2,2f,1f,1f);
        yield return new WaitForSeconds(3f);
        CameraShaker.Instance.ShakeOnce(2,2f,1f,1f);
        isDefending = false;
        animator.SetBool("Defense2",isDefending);
    }
    private IEnumerator Defense3()
    {
        isDefending = true;
        animator.SetBool("Defense3",isDefending);
        CameraShaker.Instance.ShakeOnce(2,2f,1f,1f);
        yield return new WaitForSeconds(2f);
        CameraShaker.Instance.ShakeOnce(2,2f,1f,1f);
        isDefending = false;
        animator.SetBool("Defense3",isDefending);
    }

    public void ShakeCamera()
    {
        CameraShaker.Instance.ShakeOnce(2f,1f,1f,0.5f);
        Vibrator.Vibrate(Vibrator.vibrateTimeDamage);
    }
    private IEnumerator Die()
    {
        isDefending = true;
        for(int i =0; i< FireWorks.Length; i++)
        {
            FireWorks[i].SetActive(true);
        }
        slider.gameObject.SetActive(false);
        HitPoints[HitPointIndex].SetActive(false);
        animator.SetTrigger("Death");
        Destroy(gameObject,2.3f);
        yield return new WaitForSeconds(2f);
        pauseMenuManager.OpenEndOfLevel(false,true);
        float goldMultiplier = PlayerPrefs.GetFloat("UpgradeValue5", 0);
        float moneyEarned = 60*PlayerPrefs.GetInt("Level",1)*PlayerPrefs.GetFloat("UpgradeValue1",50)/10*(1+goldMultiplier);
        gameManager.EarnMoney((int)Mathf.Round(moneyEarned/10)*10);
    }
    public void HitByRay()
    {
        TakeDamage(0.5f + PlayerPrefs.GetFloat("UpgradeValue2",0.01f));
    }
}
