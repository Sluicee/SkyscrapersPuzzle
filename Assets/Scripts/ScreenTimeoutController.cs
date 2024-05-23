using UnityEngine;

public class ScreenTimeoutController : MonoBehaviour
{
    void Start()
    {
        // ������������� ����� ���������� ������ �� ������������ ��������
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    // ����������, ����� ������ ������������
    void OnDestroy()
    {
        // ��������������� ��������� ������� ���������� ������ �� ��������� (������ ��� ����������� ���������� ��� �������� 15 ������)
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }

}
