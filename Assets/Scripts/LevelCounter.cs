using UnityEngine;
using TMPro;
using YG;

public class LevelCounter : MonoBehaviour
{
    private int overall = 0;
    private int overall4 = 0;
    private int overall5 = 0;
    private int overall6 = 0;
    private int complited = 0;
    private int complited4 = 0;
    private int complited5 = 0;
    private int complited6 = 0;
    [SerializeField] private TMP_Text overallText, overall4Text, overall5Text, overall6Text;
    [SerializeField] private TMP_Text complitedText, complited4Text, complited5Text, complited6Text;

    private void Start()
    {
        Invoke("ShowCounter", 0.05f);
    }

    void ShowCounter()
    {
        for (int i = 4; i < 7; i++)
        {
            overall += GridData.Instance.data[i].Count;
        }

        //NORMAL SDK
        /*overall4 = GridData.Instance.data[4].Count;
        overall5 = GridData.Instance.data[5].Count;
        overall6 = GridData.Instance.data[6].Count;

        complited = PlayerPrefs.GetInt("complited");
        complited4 = PlayerPrefs.GetInt("4lvl");
        complited5 = PlayerPrefs.GetInt("5lvl");
        complited6 = PlayerPrefs.GetInt("6lvl");

        overallText.text = overall.ToString();
        overall4Text.text = overall4.ToString();
        overall5Text.text = overall5.ToString();
        overall6Text.text = overall6.ToString();

        complitedText.text = complited.ToString();
        complited4Text.text = complited4.ToString();
        complited5Text.text = complited5.ToString();
        complited6Text.text = complited6.ToString();*/

        //YG SDK
        overall4 = GridData.Instance.data[4].Count;
        overall5 = GridData.Instance.data[5].Count;
        overall6 = GridData.Instance.data[6].Count;

        complited = YandexGame.savesData.complited;
        complited4 = YandexGame.savesData.lvls[4];
        complited5 = YandexGame.savesData.lvls[5];
        complited6 = YandexGame.savesData.lvls[6];

        overallText.text = overall.ToString();
        overall4Text.text = overall4.ToString();
        overall5Text.text = overall5.ToString();
        overall6Text.text = overall6.ToString();

        complitedText.text = complited.ToString();
        complited4Text.text = complited4.ToString();
        complited5Text.text = complited5.ToString();
        complited6Text.text = complited6.ToString();

    }

}
