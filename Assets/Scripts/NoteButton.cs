using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoteButton : Selectable, IPointerClickHandler
{
    [SerializeField] private Sprite onImage;
    [SerializeField] private Sprite offImage;

    private bool active;

    void Start()
    {
        active = false;    
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        active = !active;
        
        if (active)
            GetComponent<Image>().sprite = onImage;
        else
            GetComponent<Image>().sprite = offImage;

        GameEvents.OnNoteActiveMethod(active);
    }
}
