using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class StaticHose : MonoBehaviour
{
    [Header("REFERENCE")]
    public float maxDistance;

    [Header("REFERENCE")]
    public GameObject handle;
    public Transform handlePosition;
    public Transform hoseEdge;
    public Transform originPosition;

    [Header("QUERY")]
    public bool isInHand;
    public float currentDistance;


    public void handleIsGrabbed(bool value)
    {
        isInHand = value;
    }

    // Update is called once per frame
    void LateUpdate(){
        if (isInHand)
            checkDistance();
    }


    void checkDistance()
    {

        currentDistance = Vector3.Distance(handlePosition.position, hoseEdge.position);
        if (currentDistance > maxDistance) {
            foreach (var hand in Player.instance.hands){
                hand.DetachObject(handle);
            }

            handlePosition.position = hoseEdge.position;
            handlePosition.LookAt(originPosition);
        }
    }



}