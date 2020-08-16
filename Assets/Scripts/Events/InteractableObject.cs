using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent onStartInteract;
    public UnityEvent onFinishInteract;

    public virtual void StartInteract()
    {
        onStartInteract.Invoke();
    }

    public virtual void FinishInteract()
    {
        onFinishInteract.Invoke();
    }
}
