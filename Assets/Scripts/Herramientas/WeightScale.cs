﻿using UnityEngine;
using System.Collections.Generic;

public class WeightScale : MonoBehaviour
{
    float forceToMass;

    [Header("CONFIG")]
    public float calibracion = 0.105f;

    [Header("REFERENCE")]
    public bascula code_bascula;

    [Header("QUERY")]
    public float combinedForce;
    public float calculatedMass;
    public int registeredRigidbodies;

    Dictionary<Rigidbody, float> impulsePerRigidBody = new Dictionary<Rigidbody, float>();

    float currentDeltaTime;
    float lastDeltaTime;

    private void Awake()
    {
        forceToMass = 1f / Physics.gravity.magnitude;
    }

    public void UpdateWeight()
    {
        Debug.Log("Actualizando");
        registeredRigidbodies = impulsePerRigidBody.Count;
        combinedForce = 0;

        foreach (var force in impulsePerRigidBody.Values)
        {
            combinedForce += force;
        }

        calculatedMass = (float)(combinedForce * forceToMass);

        code_bascula.recieveCurrentWeight(calculatedMass);
    }

    private void FixedUpdate()
    {
        lastDeltaTime = currentDeltaTime;
        currentDeltaTime = Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            if (impulsePerRigidBody.ContainsKey(collision.rigidbody))
                impulsePerRigidBody[collision.rigidbody] = collision.impulse.y / lastDeltaTime;
            else
                impulsePerRigidBody.Add(collision.rigidbody, collision.impulse.y / lastDeltaTime);

            UpdateWeight();
        }
    }

    public void cambioDePeso(Rigidbody cuerpo)
    { 
        if (impulsePerRigidBody.ContainsKey(cuerpo))
             impulsePerRigidBody[cuerpo] = cuerpo.mass* calibracion / lastDeltaTime;
        else
            impulsePerRigidBody.Add(cuerpo, cuerpo.mass * calibracion / lastDeltaTime);

        UpdateWeight();   
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            if (impulsePerRigidBody.ContainsKey(collision.rigidbody))
                impulsePerRigidBody[collision.rigidbody] = collision.impulse.y / lastDeltaTime;
            else
                impulsePerRigidBody.Add(collision.rigidbody, collision.impulse.y / lastDeltaTime);

            UpdateWeight();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            impulsePerRigidBody.Remove(collision.rigidbody);
            UpdateWeight();
        }
    }
}