using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject4 : MonoBehaviour
{
     private List<DraggableL4> draggableObjects;
     public SceneTransitionManager4 sceneTransitionManager4; // Reference to the SceneTransitionManager script

    private void Start()
    {
        draggableObjects = new List<DraggableL4>(FindObjectsOfType<DraggableL4>());
        sceneTransitionManager4 = FindObjectOfType<SceneTransitionManager4>();
    }

    // Call this function from each draggable object when it's placed correctly
    // Call this function from each draggable object when it's placed correctly
public void ObjectPlacedCorrectly()
{
    bool allObjectsPlacedCorrectly = true;

    foreach (DraggableL4 draggable in draggableObjects)
    {
        if (!draggable.droppedOnCorrectHolder)
        {
            allObjectsPlacedCorrectly = false;
            break;
        }
    }

    if (allObjectsPlacedCorrectly)
    {
        Debug.Log("All objects correctly placed!");
        sceneTransitionManager4.TransitionToNextScene();
    }
}

}
