using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTransitionManager4 : MonoBehaviour
{
    public string nextSceneName; // Set this in the Inspector to the name of the next scene

    public void TransitionToNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
