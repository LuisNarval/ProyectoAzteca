using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PickableObject : InteractableObject, IPickable
{
    #region PUBLIC_PROPERTIES
    public TargetDrop targetDrop;
    #endregion

    #region PRIVATE_PROPERTIES
    private Transform objectPickedThisItem;
    #endregion

    #region INTERFACE_METHODS
    public void HoldItem(Transform objectPicking)
    {
        objectPickedThisItem = objectPicking;
        this.transform.SetParent(objectPickedThisItem);
    }

    public void ReleaseItem()
    {
        objectPickedThisItem = null;
    }
    #endregion
}


[CustomEditor(typeof(PickableObject))]
public class EditorPickableObject : Editor
{
    private SerializedProperty targetDropReference;

    private void OnEnable()
    {
        
    }

    private void OnSceneGUI()
    {
        if (serializedObject.FindProperty("targetDrop") != null)
        {
            //Debug.DrawLine(serializedObject.)
        }
    }
}