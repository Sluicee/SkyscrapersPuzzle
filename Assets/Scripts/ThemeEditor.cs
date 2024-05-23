using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThemeEditor : MonoBehaviour
{

    private Color[] theme;

    [SerializeField] private List<Image> graphics;

    [SerializeField] private List<Image> primaryGraphics;

    [SerializeField] private List<TMP_Text> texts;

    void Start()
    {
        theme = Themes.theme;

        foreach (var graphic in graphics)
        {
            graphic.color = theme[0];
        }

        foreach (var text in texts)
        {
            text.color = theme[0];
        }

        foreach(var graphic in primaryGraphics)
        {
            graphic.color = theme[1];
        }
    }

}
