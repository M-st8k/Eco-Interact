using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public string nextSceneName; // Set this in the Inspector to the name of the scene you want to transition to

    // Call this function when the game is completed
    public void CompleteGame()
    {
        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
