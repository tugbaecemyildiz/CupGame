using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score1Text;
    [SerializeField] private TextMeshProUGUI score2Text;
    [SerializeField] private TextMeshProUGUI score3Text;

    private int score1;
    private int score2;
    private int score3;

    private const string SCORE_1 = "BestScore1";
    private const string SCORE_2 = "BestScore2";
    private const string SCORE_3 = "BestScore3";

    void Start()
    {
        score1 = PlayerPrefs.GetInt(SCORE_1, 0);
        score2 = PlayerPrefs.GetInt(SCORE_2, 0);
        score3 = PlayerPrefs.GetInt(SCORE_3, 0);
        UpdateText();
    }

    private void SaveAll()
    {
        PlayerPrefs.SetInt(SCORE_1, score1);
        PlayerPrefs.SetInt(SCORE_2, score2);
        PlayerPrefs.SetInt(SCORE_3, score3);
    }

    private void UpdateText()
    {
        score1Text.text = score1.ToString();
        score2Text.text = score2.ToString();
        score3Text.text = score3.ToString();
    }

    public void SetHighScore(int score)
    {
        if (score > score1)
        {
            score3 = score2;
            score2 = score1;
            score1 = score;
            SaveAll();
        }
        else if (score > score2)
        {
            score3 = score2;
            score2 = score;
            SaveAll();
        }
        else if (score > score3)
        {
            score3 = score;
            SaveAll();
        }

        UpdateText();
    }
}
