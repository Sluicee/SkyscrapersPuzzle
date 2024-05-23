using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenAdjuster : MonoBehaviour
{
    private CanvasScaler scaler;
    [SerializeField] float refWidth;
    [SerializeField] float refHeight;
    [SerializeField] Vector2 referenceResolution;
    [SerializeField] float multipier = 1;
    [SerializeField] float scaleFactor = 0.3f;
    [SerializeField] int scaleFactorMult = 1700;

    private void Start()
    {
        scaler = GetComponent<CanvasScaler>();
        ReScaleScreen();
    }

    private void OnRectTransformDimensionsChange()
    {
        ReScaleScreen();
    }

    void ReScaleScreen()
    {
        if (scaler != null)
        {
            refWidth = Screen.width;
            refHeight = Screen.height;
            //referenceResolution = new Vector2(refWidth, refHeight);
            //scaler.referenceResolution = referenceResolution;
            if (refHeight < refWidth)
                scaler.scaleFactor = ((refWidth * refWidth) / (refWidth + refHeight)) / scaleFactorMult;
            else if (refWidth + refHeight < 1500)
                scaler.scaleFactor = 0.3f;
            else
                scaler.scaleFactor = scaleFactor;
            Debug.Log($"Window dimensions changed to {Screen.width}x{Screen.height}");
        }
        

    }
}
