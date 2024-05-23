using UnityEngine;
using UnityEngine.UI;

public class MusicMute : MonoBehaviour
{
    public Sprite ActiveImage, DisabledImage;
    private Image btnImage;
    private bool musicMute = false;

    private void Start()
    {
        btnImage = GetComponent<Image>();
    }

    public void MusicControl()
    {
        musicMute = !musicMute;
        AudioListener.pause = musicMute;
        Debug.Log($"Music mute: {musicMute}");
        if (!musicMute)
        {
            btnImage.sprite = ActiveImage;
        }
        else
        {
            btnImage.sprite = DisabledImage;
        }
        
    }

}
