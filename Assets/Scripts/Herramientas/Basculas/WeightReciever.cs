using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightReciever : MonoBehaviour
{
    [Header("CONFIG")]
    public float rateUpdate;

    [Header("REFERENCE")]
    public bascula codeBascula;

    [Header("QUERY")]
    public float currentWeight;
    public List<Rigidbody> bodies;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(coroutine_SendWeight());
    }


    private void makeSum()
    {
        currentWeight = 0;
        for (int i = 0; i < bodies.Count; i++){
            currentWeight += bodies[i].mass;
        }
    }


    private IEnumerator coroutine_SendWeight()
    {
        while (true){
            makeSum();
            codeBascula.recieveCurrentWeight(currentWeight);
            yield return new WaitForSeconds(rateUpdate);
        }
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        try{
            Rigidbody cuerpo = collision.gameObject.GetComponent<Rigidbody>();
            Debug.Log("Tiene cuerpo: " + cuerpo.name);

            if (!bodies.Contains(cuerpo))
                bodies.Add(cuerpo);
        }
        catch{
            Debug.Log("Sin Cuerpo Rigido");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        try{
            if (!bodies.Contains(collision.gameObject.GetComponent<Rigidbody>()))
                bodies.Add(collision.gameObject.GetComponent<Rigidbody>());
        }
        catch{}
    }


    private void OnCollisionExit(Collision collision)
    {
        bodies.Remove(collision.gameObject.GetComponent<Rigidbody>());
    }

}