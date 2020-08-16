using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plato : MonoBehaviour
{
    [Header("CONFIG")]
    public float capacidadTotal;

    [Header("REFERENCE")]
    public Animator animSales;
    public Rigidbody cuerpo;

    public ParticleSystem grainExplotionPS;
    public Transform topOV;


    [Header("QUERY")]
    public float gramos;
    public float pesoPlato;
    public WeightScale code_WS;
    public bool canPour;


    private Vector3 vectorNormal;
    private float orientationAngle;


    // Start is called before the first frame update
    void Start()
    {
        grainExplotionPS.Stop();
        canPour = false;
        gramos = 0;
        pesoPlato = cuerpo.mass;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPour)
            checkExplotionAngle();
     
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bascula"))
            code_WS = collision.gameObject.GetComponent<WeightScale>();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bascula")) {
            if (code_WS != null)
                code_WS = null;
        }
    }


    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Granos")){
            
            if (gramos < capacidadTotal){
                recibirSales();
            }

        }
    }




    void recibirSales()
    {
        gramos++;
        animSales.Play("", 0, gramos / capacidadTotal);
        cuerpo.mass += 0.01f;
        if (code_WS != null)
            code_WS.cambioDePeso(cuerpo);

        if (!canPour)
            canPour = true;
    }







    void checkExplotionAngle()
    {
        vectorNormal = this.transform.position + Vector3.up * 1;
        orientationAngle = Vector3.Angle(vectorNormal, topOV.position);

        if (orientationAngle > 1.45)
            explote();
    }



    void explote()
    {
        animSales.Play("", 0, 0);
        canPour = false;

        float explotionSize = gramos/ 100;

        if (gramos < 100)
            explotionSize = gramos / 100;
        else
            explotionSize = 1 + gramos / 1000;


        grainExplotionPS.transform.localScale = new Vector3(explotionSize, explotionSize, explotionSize);
        grainExplotionPS.Play();

        gramos = 0;
        cuerpo.mass = pesoPlato;
    }

}
