using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapScene : MonoBehaviour
{
    [SerializeField]
    private string loadSceneName;

    private void Start()
    {
        if (!SceneManager.GetSceneByName(loadSceneName).isLoaded)
            SceneManager.LoadScene(loadSceneName, LoadSceneMode.Additive);
    }
}
