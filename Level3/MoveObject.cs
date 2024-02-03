using UnityEngine;
using System.Collections.Generic;

public class MoveObject : MonoBehaviour
{
    private List<Draggable> draggableObjects;
    public SceneTransitionManager sceneTransitionManager; // Reference to the SceneTransitionManager script
    private int correctlyPlacedCount = 0; // Number of correctly placed objects


    private void Start()
    {
        draggableObjects = new List<Draggable>(FindObjectsOfType<Draggable>());
        sceneTransitionManager = FindObjectOfType<SceneTransitionManager>();
    }

    // Call this function from each draggable object when it's placed correctly
    public void ObjectPlacedCorrectly()
    {
        correctlyPlacedCount++;

        if (correctlyPlacedCount == draggableObjects.Count)
        {
            Debug.Log("All objects correctly placed!");
            sceneTransitionManager.CompleteGame();
        }
    }
}
