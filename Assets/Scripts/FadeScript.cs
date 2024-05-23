using UnityEngine;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private float fadeDuration;
    private float startTime;

    private CanvasGroup canvasGroupHide;
    private CanvasGroup canvasGroupShow;
    private bool fadeIn = false;
    private bool fadeOut = false;
    public void ShowUI(CanvasGroup canvasGroup)
    {
        fadeIn = true;
        this.canvasGroupShow = canvasGroup;
        canvasGroupShow.interactable = true;
        canvasGroupShow.blocksRaycasts = true;
        startTime = Time.time;
    }

    public void HideUI(CanvasGroup canvasGroup)
    {
        this.canvasGroupHide = canvasGroup;
        fadeOut = true;
        canvasGroupHide.interactable = false;
        canvasGroupHide.blocksRaycasts = false;
        startTime = Time.time;
    }

    private void Update()
    {
        float t = (Time.time - startTime) / fadeDuration;
        if (fadeIn)
        {
            t += Time.deltaTime;
            canvasGroupShow.alpha = Mathf.SmoothStep(0, 1, t);

            if (canvasGroupShow.alpha >= 1)
            {
                fadeIn = false;
            }
        }

        if (fadeOut)
        {
            t += Time.deltaTime;
            canvasGroupHide.alpha = Mathf.SmoothStep(1, 0, t);

            if (canvasGroupHide.alpha <= 0)
            {
                fadeOut = false;
            }
        }
    }
}
