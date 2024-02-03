using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform draggingObject;
    private Vector2 originalPosition;
    private Rigidbody2D rb;

    public float dragSpeed = 5f; // Adjust this value to control drag speed

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Store the reference to the dragging object
        draggingObject = eventData.pointerDrag.GetComponent<RectTransform>();

        // Store the original position of the dragging object
        originalPosition = draggingObject.anchoredPosition;

        // Make the dragging object appear on top of other objects
        draggingObject.SetAsLastSibling();

        // Enable physics for dragging
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update the position of the dragging object based on the drag speed
        rb.MovePosition(rb.position + eventData.delta / GetComponent<Canvas>().scaleFactor * dragSpeed);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Disable physics for dragging
        rb.bodyType = RigidbodyType2D.Kinematic;

        // Get the reference to the answer holder that we dropped the object onto
        Image answerHolder = eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>();

        if (answerHolder != null)
        {
            // Check if the answer holder and dragging object have matching tags
            if (answerHolder.CompareTag(draggingObject.tag))
            {
                // Correct placement, disable the dragging object
                draggingObject.GetComponent<Image>().raycastTarget = false;
                draggingObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
            else
            {
                // Incorrect placement, reset the position of the dragging object
                draggingObject.anchoredPosition = originalPosition;
            }
        }
        else
        {
            // We didn't drop the object onto an answer holder, reset its position
            draggingObject.anchoredPosition = originalPosition;
        }

        // Check if all images are properly placed
        if (CheckAllImagesPlacedCorrectly())
        {
            // Proceed to the next set of images or level
            FoodChainGame game = FindObjectOfType<FoodChainGame>();
            if (game != null)
                game.CheckPlacements();
        }
    }

    private bool CheckAllImagesPlacedCorrectly()
    {
        Image[] images = FindObjectsOfType<Image>();

        foreach (Image image in images)
        {
            // Skip non-draggable images
            if (image.raycastTarget)
                return false;
        }

        return true;
    }
}
