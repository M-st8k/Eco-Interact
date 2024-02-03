using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string correctImageTag; // Add this public variable for the correct image tag
    //public Texture2D circularMaskTexture; // Assign the circular mask texture through the inspector

    private ImageHolder imageHolder;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
   // private Material circularMaskMaterial;
    public MoveObject moveObject; // Reference to the MoveObject script

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        imageHolder = GetComponentInParent<ImageHolder>();
        originalPosition = transform.position;
        //circularMaskMaterial = new Material(Shader.Find("Custom/CircularStencilShader"));
        //circularMaskMaterial.SetTexture("_MainTex", GetComponent<Image>().mainTexture);
        //circularMaskMaterial.SetTexture("_StencilTex", circularMaskTexture);
        moveObject = FindObjectOfType<MoveObject>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Check if the image is dropped over a valid ImageHolder
        if (eventData.pointerEnter != null)
        {
            ImageHolder currentEnteredImageHolder = eventData.pointerEnter.GetComponent<ImageHolder>();
            if (currentEnteredImageHolder != null && currentEnteredImageHolder.IsCorrectImageTag(correctImageTag))
            {
                // The image is dropped over the correct ImageHolder, snap it to the holder
                imageHolder = currentEnteredImageHolder; // Assign the currentEnteredImageHolder to imageHolder
                imageHolder.SetOccupied(true);
                transform.SetParent(currentEnteredImageHolder.transform, true);
                transform.position = currentEnteredImageHolder.transform.position;

                // Call the ObjectPlacedCorrectly() function from the MoveObject script
                moveObject.ObjectPlacedCorrectly();

            }
            else
            {
                // If the image is not dropped over the correct ImageHolder, reset its position
                ResetPosition();
            }
        }
        else
        {
            // The image is not dropped over any valid ImageHolder, reset its position
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        transform.position = originalPosition;

        if (imageHolder != null)
        {
            imageHolder.SetOccupied(false);
        }
    }

}