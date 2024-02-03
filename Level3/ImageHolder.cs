using UnityEngine;
using UnityEngine.EventSystems;

public class ImageHolder : MonoBehaviour, IDropHandler
{
    private bool isOccupied = false;

    public bool IsOccupied()
    {
        return isOccupied;
    }

    public void SetOccupied(bool occupied)
    {
        isOccupied = occupied;
    }

    public bool IsCorrectImageTag(string tag)
    {
        return this.tag == tag;
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Get the draggable image being dragged
        Draggable draggable = eventData.pointerDrag?.GetComponent<Draggable>();

        // If the draggable image exists and has the correct tag, snap it to this ImageHolder
        if (draggable != null && draggable.tag == this.tag)
        {
            draggable.transform.SetParent(transform, true);
            draggable.transform.position = transform.position;
            SetOccupied(true);

            //FindObjectOfType<MoveObject>().CheckPlacement(); // Trigger placement check after the drag ends
        }
    }
}