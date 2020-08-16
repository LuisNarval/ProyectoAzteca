using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimations : MonoBehaviour
{
    public static MenuAnimations instance;

    #region PUBLIC_PROPERTIES
    public Animator[] animators;
    #endregion

    #region PRIVATE_PROPERTIES
    [SerializeField]
    private int currentIndex = 0;
    #endregion

    #region UNITY_METHODS
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }
    #endregion

    #region PUBLIC_METHODS
    public void ShootNextAnimation()
    {
        animators[currentIndex].Play("Hide");

        currentIndex++;
        if (currentIndex < animators.Length)
            animators[currentIndex].Play("Show");
    }
    #endregion
}
