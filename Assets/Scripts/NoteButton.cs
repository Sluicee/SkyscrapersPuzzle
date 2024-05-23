using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoteButton : Selectable, IPointerClickHandler
{
    [SerializeField] private Sprite onImage;
    [SerializeField] private Sprite offImage;

    private bool active;

    private AudioSource audioSource;

    void Start()
    {
        active = false;
        audioSource = GetComponent<AudioSource>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        active = !active;
        
        if (active)
            GetComponent<Image>().sprite = onImage;
        else
            GetComponent<Image>().sprite = offImage;

        GameEvents.OnNoteActiveMethod(active);
        audioSource.Play();
    }
}
