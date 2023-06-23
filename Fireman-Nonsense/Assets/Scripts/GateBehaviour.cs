using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateBehaviour : MonoBehaviour
{
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;

    [SerializeField] private float value;
    [SerializeField] private string gateName;
    [SerializeField] private bool isValueMultiplier;
    [SerializeField] private TMP_Text bottomText;
    [SerializeField] private TMP_Text topText;
    private playerInput playerInput;
    [SerializeField] private Transform[] gateTransform;
    private int indexGateTransform = 0;
    [SerializeField] private bool canMove = false;
    private Hovl_DemoLasers hovl_DemoLasers;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GameObject.Find("Player").GetComponent<playerInput>();
        hovl_DemoLasers = GameObject.Find("Player").GetComponent<Hovl_DemoLasers>();
        UpdateBottomText();
        topText.text = gateName;
        indexGateTransform = Random.Range(0, gateTransform.Length);
        transform.position = gateTransform[indexGateTransform].position;
    }

    void Update()
    {
        if(!canMove) return;
        //Move toward the next gateTransform
        transform.position = Vector3.MoveTowards(transform.position, gateTransform[indexGateTransform].position, 0.01f);
        if(transform.position == gateTransform[indexGateTransform].position)
        {
            indexGateTransform = 1-indexGateTransform;
        }

    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            if(gateName == "Range")
            {
                if(value > 5) value = 5;
                hovl_DemoLasers.MaxLength += value;
                if(hovl_DemoLasers.isShooting)
                {
                    hovl_DemoLasers.StopShooting();
                    hovl_DemoLasers.StartShooting();
                }

                // playerInput.UpdateParticleRateOverTime();
            }
            if(gateName == "Damage")
            {
                if(value > 0.05f && !isValueMultiplier) value = 0.05f;
                if(value > 2 && isValueMultiplier) value = 2;
                playerInput.ChangeDamageValues(value,isValueMultiplier);
            }
            if(gateName == "Triple")
            {
                hovl_DemoLasers.StopShooting();
                hovl_DemoLasers.isTriple = true;
                hovl_DemoLasers.StartShooting();
            }
            Destroy(gameObject);
        }

    }
    private void HitByRay()
    {
        if (topText.text != "Triple")
        {
            if(isValueMultiplier)
            {
                value += (0.005f + playerInput.GetDamageAddition())*playerInput.GetDamageMultiplier();
            }
            else
            {
                value += (0.01f + playerInput.GetDamageAddition())*playerInput.GetDamageMultiplier();
            }
            if(value > 10) value = 10;
            UpdateBottomText();
            ModifyValueExceptionCases();
        }
    }   

    private void UpdateBottomText()
    {
        if(gateName == "Triple")
        {
            bottomText.text = "";
            transform.GetChild(0).GetComponent<Renderer>().material = greenMaterial;
            transform.GetChild(1).GetComponent<Renderer>().material = greenMaterial;
            return;
        }
        if(value>=0)
        {
            bottomText.text = isValueMultiplier ? "x" + (Mathf.Round(value*10)/10).ToString() : "+" + (Mathf.Round(value*10)/10).ToString();
            bottomText.color = Color.green;
            transform.GetChild(0).GetComponent<Renderer>().material = greenMaterial;
            transform.GetChild(1).GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            bottomText.text = isValueMultiplier ? "/" + (Mathf.Round(-value*10)/10).ToString() : (Mathf.Round(value*10)/10).ToString();
            bottomText.color = Color.red;
            transform.GetChild(0).GetComponent<Renderer>().material = redMaterial;
            transform.GetChild(1).GetComponent<Renderer>().material = redMaterial;
        }
    }
    private void ModifyValueExceptionCases()
    {
        if(isValueMultiplier && value > -1 && value < 0)
        {
            value = -value;
        }
    }
    public void Initiate(float value, string gateName, bool isValueMultiplier, bool canMove)
    {
        this.value = value;
        this.gateName = gateName;
        this.isValueMultiplier = isValueMultiplier;
        isValueMultiplier = gateName == "Range" ? false : isValueMultiplier;
        this.canMove = canMove;
        UpdateBottomText();
        topText.text = gateName;
    }
}