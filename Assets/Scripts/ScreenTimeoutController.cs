using UnityEngine;

public class ScreenTimeoutController : MonoBehaviour
{
    void Start()
    {
        // Устанавливаем время отключения экрана на максимальное значение
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    // Вызывается, когда объект уничтожается
    void OnDestroy()
    {
        // Восстанавливаем настройки времени отключения экрана по умолчанию (обычно для большинства приложений это значение 15 секунд)
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }

}
