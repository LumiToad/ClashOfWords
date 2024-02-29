using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public bool LoadScene(int buildIndex)
    {
        if (buildIndex > SceneManager.sceneCountInBuildSettings - 1) return false;

        SceneManager.LoadScene(buildIndex);
        return true;
    }
}
