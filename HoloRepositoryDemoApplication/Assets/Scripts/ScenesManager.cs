using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField]
    private int LoadSceneIndex;
    [SerializeField]
    private int UnloadedSceneIndex;

    public void LoadScene()
    {
        SceneManager.LoadScene(LoadSceneIndex, LoadSceneMode.Additive);
    }

    public void UnloadScene()
    {
        SceneManager.UnloadSceneAsync(UnloadedSceneIndex);
    }
}
