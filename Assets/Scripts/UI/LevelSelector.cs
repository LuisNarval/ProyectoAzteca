using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{

    #region PUBLIC_PROPERTIES
    public Button btn_StartLevel;
    public Button[] buttonsLevel;
    #endregion

    #region PUBLIC_METHODS
    public void ButtonPressed(Button btn)
    {
        for (int i = 0; i < buttonsLevel.Length; i++)
        {
            buttonsLevel[i].interactable = i != btn.transform.GetSiblingIndex();
        }

        btn_StartLevel.interactable = true;
    }

    public void PrepareScene()
    {
        
    }
    #endregion

    #region UNITY_METHODS

    #endregion
}
