using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void LoadScene(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex, LoadSceneMode.Additive);
    }

    public void UnloadScene(int SceneIndex)
    {
        SceneManager.UnloadSceneAsync(SceneIndex);
    }
    public static void RefreshScene(string SceneIndex)
    {
        SceneManager.UnloadSceneAsync(SceneIndex);
        SceneManager.LoadScene(SceneIndex, LoadSceneMode.Additive);
    }
}
