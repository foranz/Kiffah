using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public AudioClip successSound;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        
        // Pindahkan objek yang diseret menjadi 'child' dari Drop Zone ini
        droppedObject.transform.SetParent(this.transform);
        droppedObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        // Mainkan suara sukses
        if (SFXManager.instance != null && successSound != null)
        {
            SFXManager.instance.PlaySound(successSound);
        }
    }
}