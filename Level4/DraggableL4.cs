using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableL4 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string correctImageTag;
    public TextMeshProUGUI correctText1; // Reference to the first TextMeshPro element
    public TextMeshProUGUI correctText2; // Reference to the second TextMeshPro element

    private ImageHolder imageHolder;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    private Vector2 originalSize;
    public bool droppedOnCorrectHolder = false;
    public MoveObject4 moveObject4;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        imageHolder = GetComponentInParent<ImageHolder>();
        originalPosition = transform.position;
        originalSize = GetComponent<RectTransform>().sizeDelta;

        // Disable both TextMeshPro components initially
        correctText1.gameObject.SetActive(false);
        correctText2.gameObject.SetActive(false);

        moveObject4 = FindObjectOfType<MoveObject4>();
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

        if (eventData.pointerEnter != null)
        {
            ImageHolder currentEnteredImageHolder = eventData.pointerEnter.GetComponent<ImageHolder>();
            if (currentEnteredImageHolder != null && currentEnteredImageHolder.IsCorrectImageTag(correctImageTag))
            {
                // The image is dropped over the correct ImageHolder, snap it to the holder
                imageHolder = currentEnteredImageHolder;
                imageHolder.SetOccupied(true);
                transform.SetParent(currentEnteredImageHolder.transform, true);
                transform.position = currentEnteredImageHolder.transform.position;

                // Resize the image when dropped on the correct holder
                GetComponent<RectTransform>().sizeDelta = originalSize * 0.5f;
                droppedOnCorrectHolder = true;

                // Enable the correctText1 element and disable correctText2
                correctText1.gameObject.SetActive(!correctText1.gameObject.activeSelf);
                correctText2.gameObject.SetActive(!correctText2.gameObject.activeSelf);

                // Call the ObjectPlacedCorrectly() function from the MoveObject script
                moveObject4.ObjectPlacedCorrectly();
            }
            else
            {
                // If the image is not dropped over the correct ImageHolder, reset its position and size
                ResetPositionAndSize();

                // Disable both text elements
                correctText1.gameObject.SetActive(false);
                correctText2.gameObject.SetActive(false);
            }
        }
        else
        {
            // The image is not dropped over any valid ImageHolder, reset its position and size
            ResetPositionAndSize();

            // Disable both text elements
            correctText1.gameObject.SetActive(false);
            correctText2.gameObject.SetActive(false);
        }
    }

    public void ResetPositionAndSize()
    {
        transform.position = originalPosition;

        if (droppedOnCorrectHolder)
        {
            GetComponent<RectTransform>().sizeDelta = originalSize;
        }

        droppedOnCorrectHolder = false;

        if (imageHolder != null)
        {
            imageHolder.SetOccupied(false);
        }
    }
}
