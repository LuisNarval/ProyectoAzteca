using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;

public class BolsaDeUnKilo : MonoBehaviour
{
    [Header("CONFIG")]
    public float fillRate = 1.0f;
    public float pourRate = 1.0f;
    public float pourAngle = 1.38f;
    public float maxKiloCapacity = 2.0f;
    public float minPivotValue = -0.55f;
    public float maxPivotValue = 2.0f;

    [Header("REFERENCE")]
    public ParticleSystem grainPS;
    public Transform closePivot;
    public Transform fillPivot;
    public GameObject fill;
    public Transform topOV;

    [Header("QUERY")]
    public float currentKilos = 0.0f;
    public bool isBagOpen = false;
    public bool isPouring;

    Vector3 normalVector;
    float orientationAngle;
    float fillAmount;
    float bagWeight;

    // Start is called before the first frame update
    void Start()
    {
        bagWeight = this.GetComponent<Rigidbody>().mass;
        currentKilos = 0.0f; 
        asignFillAmount();
        fill.gameObject.SetActive(false);
        isBagOpen = false;
        isPouring = false;
    }


    void Update()
    {
        if (isBagOpen)
            checkPourAngle();    
    }


    void checkPourAngle()
    {
        normalVector = this.transform.position + Vector3.up * 1;
        orientationAngle = Vector3.Angle(normalVector, topOV.position);

        if (isPouring){
            if (orientationAngle < pourAngle){
                isPouring = false;
                StopCoroutine("coroutine_pourGrains");
                grainPS.Stop();
            }
        } else {
            if (orientationAngle >= pourAngle){
                isPouring = true;
                StartCoroutine("coroutine_pourGrains");
            }
        }

    }



    public void CloseBag()
    {
        closePivot.localScale = new Vector3 (1.0f,1.0f,-1.2f);
        isBagOpen = false;
    }

    public void OpenBag()
    {
        closePivot.localScale = new Vector3(1.0f,1.0f,1.0f);
        isBagOpen = true;
    }

    public void fillBag()
    {
        if (isBagOpen){
            if (currentKilos == 0.0f)
                fill.SetActive(true);

            if (currentKilos < maxKiloCapacity)
                currentKilos += fillRate;

            asignFillAmount();
        }
    }

    IEnumerator coroutine_pourGrains()
    {
        grainPS.Play();

        while (currentKilos > 0){
            currentKilos -= Time.deltaTime*pourRate;
            asignFillAmount();
            yield return new WaitForEndOfFrame();
        }

        fill.SetActive(false);
        grainPS.Stop();
    }

    void asignFillAmount()
    {
        if (currentKilos > maxKiloCapacity)
            currentKilos = maxKiloCapacity;
        if (currentKilos < 0)
            currentKilos = 0;

        this.GetComponent<Rigidbody>().mass = currentKilos + bagWeight;

        fillAmount = (((Mathf.Abs(minPivotValue) + Mathf.Abs(maxPivotValue)) * currentKilos) / maxKiloCapacity) + minPivotValue;
        fillPivot.localScale = new Vector3(1.0f, fillAmount, 1.0f);
    }



}