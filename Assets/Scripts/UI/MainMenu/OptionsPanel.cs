using UnityEngine;

public class OptionsPanel : MonoBehaviour
{
    public void OnCloseClick()
    {
        gameObject.SetActive(false);
    }

    public void OnFullscreenValueChanged(bool fullscreen)
    {
        Screen.fullScreenMode = fullscreen? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;
    }
}
