using System.Collections.Generic;
using UnityEngine;
using YG;

public class MetricaSender : MonoBehaviour
{
    public void Send(string id)
    {
        YandexMetrica.Send(id);
    }

    public void TrigerSend(string name)
    {
        var eventParams = new Dictionary<string, string>
        {
            { "triggers", name }
        };

        YandexMetrica.Send("triggers", eventParams);
    }
}