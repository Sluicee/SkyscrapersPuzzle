using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class NumberButton : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    public int value = 0;
    [SerializeField] private TMP_Text buttonText;
    private AudioSource audioSource;

    public void Start()
    {
        Vibration.Init();
        buttonText.SetText(value.ToString());
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vibration.VibrateAndroid(10);
        GameEvents.UpdateSquareNumberMethod(value);
        audioSource.Play();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        
    }
}
