using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plug : MonoBehaviour, IDropHandler
{
    public PlugColor PlugColor;

    public event Action OnWireChanged;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        bool occupied = transform.childCount > 0;
        if (!occupied)
        {
            draggableItem.ParentAfterDrag = transform;
            StartCoroutine(DelayEvent());
        }
    }

    private IEnumerator DelayEvent()
    {
        yield return new WaitForSeconds(0.05f);
        OnWireChanged?.Invoke();
    }
}
