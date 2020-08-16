using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pala : MonoBehaviour
{
    [Header("CONFIG")]
    public float grainAmount;
    public float pourAngle = -0.07f;

    [Header("REFERENCE")]
    public ParticleSystem grainPS;
    public ParticleSystem grainExplotionPS;
    public GameObject grains;
    public Animator grainAnimation;

    public Transform topOV;
    public Transform frontOV;


    [Header("QUERY")]
    public float currentGrainAmount;
    public bool canPour;
    public bool isPouring;
    public float shovelInclination;
    
    Vector3 vectorNormal;
    float orientationAngle;

    // Start is called before the first frame update
    void Start()
    {
        canPour = false;
        grainPS.Stop();
        grainExplotionPS.Stop();
        grains.SetActive(false);
        currentGrainAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPour){
            checkExplotionAngle();
            checkPourAngle();
        }
    }



    public void fillShovel()
    {
        grainExplotionPS.Stop();
        currentGrainAmount = grainAmount;
        grains.SetActive(true);
        grainAnimation.Play("", 0, 1);
    }



    IEnumerator coroutine_pourGrains()
    {
        grainPS.Play();

        while (currentGrainAmount > 0){
            currentGrainAmount -= Time.deltaTime;

            grainAnimation.Play("", 0, currentGrainAmount / grainAmount);

            yield return new WaitForEndOfFrame();
        }

        canPour = false;
        grainPS.Stop();
        grains.SetActive(false);
    }


    void checkExplotionAngle()
    {
        vectorNormal = this.transform.position + Vector3.up * 1;
        orientationAngle = Vector3.Angle(vectorNormal, topOV.position);

        if (orientationAngle > 1.45)
            explote();
    }

    void checkPourAngle()
    {
        shovelInclination = frontOV.transform.position.y - this.transform.position.y;

        if (isPouring) {
            if (shovelInclination > pourAngle){
                isPouring = false;
                StopCoroutine("coroutine_pourGrains");
                grainPS.Stop();
            }
        }
        
        else {
            if (shovelInclination <= pourAngle){
                isPouring = true;
                StartCoroutine("coroutine_pourGrains");
            }
        }
    }


    void explote()
    {
        grainAnimation.Play("", 0, 0);
        grains.SetActive(false);
        canPour = false;

        float explotionSize = currentGrainAmount / grainAmount;
  
        grainExplotionPS.transform.localScale = new Vector3(explotionSize, explotionSize, explotionSize);
        grainExplotionPS.Play();

        currentGrainAmount = 0;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("grainBag"))
            fillShovel();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("grainBag"))
            canPour = true;
    }


    



}







/*void printPosition(){
    vectorNormal =this.transform.position + Vector3.up * 1;

    Debug.DrawLine(this.transform.position, this.transform.position + Vector3.up * 1, Color.blue, Time.deltaTime);
    Debug.DrawLine(this.transform.position, topOV.position, Color.red, Time.deltaTime);

    Debug.DrawLine(vectorNormal, topOV.position, Color.green, Time.deltaTime);

    float angulo = Vector3.Angle(vectorNormal, topOV.position);
    Debug.Log(angulo.ToString("f2"));
}*/
