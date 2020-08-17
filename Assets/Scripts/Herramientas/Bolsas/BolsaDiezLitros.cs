using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolsaDiezLitros : MonoBehaviour
{
    [Header("CONFIG")]
    public float pourAngle = 1.38f;

    [Header("REFERENCE")]
    public Transform hoseEdge;
    public Transform topOV;
    public ParticleSystem waterPS;


    [Header("QUERY")]
    public bool isPouring;

    Vector3 normalVector;
    float orientationAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkPourAngle();
    }

    void checkPourAngle()
    {
        normalVector = hoseEdge.position + Vector3.up * 1;
        orientationAngle = Vector3.Angle(normalVector, topOV.position);

        if (isPouring)
        {
            if (orientationAngle < pourAngle)
            {
                isPouring = false;
                waterPS.Stop();
            }
        }
        else
        {
            if (orientationAngle >= pourAngle)
            {
                isPouring = true;
                waterPS.Play();
            }
        }

    }


}