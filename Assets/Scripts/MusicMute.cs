using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicMute : MonoBehaviour
{
    public AudioSource music;
    public Sprite ActiveImage, DisabledImage;
    private Image btnImage;
    private bool musicMute = false;

    private void Start()
    {
        btnImage = GetComponent<Image>();
    }

    public void MusicControl()
    {
        if (musicMute)
        {
            music.UnPause();
            btnImage.sprite = ActiveImage;
            musicMute = !musicMute;
        }
        else
        {
            music.Pause();
            btnImage.sprite = DisabledImage;
            musicMute = !musicMute;
        }
        
    }

}
