using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneAdmin : MonoBehaviour
{
    #region PUBLIC_PROPERTIES
    public Slider progressImage;
    #endregion

    #region PUBLIC_METHODS
    public void LoadScene(int index)
    {
        StartCoroutine(RoutineLoadScene(index));
    }
    #endregion

    #region ROUTINES
    IEnumerator RoutineLoadScene(int sceneIndex)
    {
        yield return new WaitForSeconds(1);
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);
        while (!async.isDone)
        {
            progressImage.value = async.progress;
            yield return null;
        }
        
        
    }
    #endregion
}
