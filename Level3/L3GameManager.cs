using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3GameManager : MonoBehaviour
{
    public UIManager uiManager;
    public List<GameObject> imageHolders;
    public L3ScriptTable scriptTable; // Reference to the L3ScriptTable scriptable object

    private void Start()
    {
        // Access question data
        List<L3QuestionData> questions = scriptTable.questions;

        foreach (L3QuestionData question in questions)
        {
            GameObject imageHolderPrefab = question.imageHolderPrefab;
            GameObject answerHolderPrefab = question.answerHolderPrefab;

            // Check for null references before proceeding
            if (imageHolderPrefab == null || answerHolderPrefab == null)
            {
                Debug.LogError("Image or Answer Holder Prefab is not assigned for a question.");
                continue; // Move to the next question
            }

            List<string> imageTags = question.imageTags;
            int order = question.order;

            // Instantiate image holder prefab
            GameObject instantiatedImageHolder = Instantiate(imageHolderPrefab);

            List<Sprite> draggableImages = question.draggableImages; // List of draggable images

            foreach (Sprite draggableImage in draggableImages)
            {
                // Check for null references before proceeding
                if (draggableImage == null)
                {
                    Debug.LogError("Draggable Image is not assigned for a draggable image.");
                    continue; // Move to the next draggable image
                }

                // Create a new GameObject to hold the draggable image component
                GameObject draggableObject = new GameObject("DraggableImage");
                draggableObject.transform.SetParent(instantiatedImageHolder.transform);

                // Add the SpriteRenderer component to the GameObject
                SpriteRenderer spriteRenderer = draggableObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = draggableImage;
            }
            
            // This is where you can attach the instantiatedImageHolder to other logic if needed.
        }
    }
}
