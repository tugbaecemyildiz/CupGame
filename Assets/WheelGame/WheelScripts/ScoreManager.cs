using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ayranText;
    public static int ayranScore = 0;
    [SerializeField] private TextMeshProUGUI _midyeText;
    public static int midyeScore = 0;
    [SerializeField] private TextMeshProUGUI _kokorecText;
    public static int kokorecScore = 0;

    void Start()
    {
        UpdateScoreText();
    }
    public void AddPrize(string prizeType) //Taglere göre ödül arttýrma
    {
        switch (prizeType)
        {
            case "Ayran":
                ayranScore++;
                _ayranText.text = ayranScore.ToString();
                break;
            case "Midye":
                midyeScore++;
                _midyeText.text = midyeScore.ToString();
                break;
            case "Kokorec":
                kokorecScore++;
                _kokorecText.text = kokorecScore.ToString();
                break;
        }
    }
    public void ResetScore()
    {
        ayranScore = 0;
        midyeScore = 0;
        kokorecScore = 0;

        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        _ayranText.text = ayranScore.ToString();
        _midyeText.text = midyeScore.ToString();
        _kokorecText.text = kokorecScore.ToString();
    }
}
