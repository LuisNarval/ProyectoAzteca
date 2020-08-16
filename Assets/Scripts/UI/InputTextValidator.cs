using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTextValidator : MonoBehaviour
{
    #region PUBLIC_PROPERTIES
    public Button button;
    #endregion

    #region PUBLIC_METHODS
    public void CheckEmptyText(string inputText)
    {
        button.interactable = !string.IsNullOrEmpty(inputText);
    }
    #endregion
}
