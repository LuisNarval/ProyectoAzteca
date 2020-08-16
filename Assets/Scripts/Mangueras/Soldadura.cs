using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RopeMinikit;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Soldadura : MonoBehaviour
{

    [Header("REFERENCE")]
    public GameObject thisHoseEdge;
    public Collider colision;
    public Collider trigger;
    public RopeRigidbodyConnection RopeConection1;
    public RopeRigidbodyConnection RopeConection2;

    [Header("QUERY")]
    public Rope otherHose;
    public bool soldado;
    public GameObject abuelo;
    public Rope cuerdaPadre;
    public Rope cuerdaExterna;

    // Start is called before the first frame update
    void Start()
    {
        soldado = false;
        colision.enabled = false;
        abuelo = thisHoseEdge.transform.parent.gameObject;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;

        cuerdaPadre=thisHoseEdge.GetComponent<RopeRigidbodyConnection>().rope;
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject != thisHoseEdge.gameObject){

            if (other.gameObject.CompareTag("HoseEdge")){
                activarSoldadura(other.gameObject);
            }

        }

    }



    void activarSoldadura(GameObject otro)
    {
        cuerdaExterna = otro.GetComponent<RopeRigidbodyConnection>().rope;
        

        trigger.enabled = false;

        this.transform.parent = abuelo.transform;
        RopeConection1.rope = cuerdaPadre;
        RopeConection2.rope = cuerdaExterna;

        foreach (var hand in Player.instance.hands)
        {
            hand.DetachObject(otro.gameObject);
            hand.DetachObject(thisHoseEdge);
        }

        colision.enabled = true;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;

        Destroy(otro);
        Destroy(thisHoseEdge);

        soldado = true;
    }



}