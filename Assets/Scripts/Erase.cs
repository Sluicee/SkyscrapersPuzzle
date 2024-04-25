using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Erase : Selectable, IPointerClickHandler
{
    private AudioSource audioSource;

    protected override void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameEvents.ClearNumberMethod();
        audioSource.Play();
    }
}
