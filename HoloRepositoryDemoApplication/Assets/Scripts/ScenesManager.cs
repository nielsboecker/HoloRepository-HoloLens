using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void ToStartScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(2);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    public void ToDetailScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(1);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    public void StartApp(string sceneName)
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    public void RemoveComponent()
    {
        Destroy(gameObject);
    }
}