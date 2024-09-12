using TMPro;
using UnityEngine;

namespace SuikaGame
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI score1Text;
        [SerializeField] private TextMeshProUGUI score2Text;
        [SerializeField] private TextMeshProUGUI score3Text;
        private int score1;
        private int score2;
        private int score3;

        void Start()
        {
            score1 = PlayerPrefs.GetInt("Score1", 0);
            score2 = PlayerPrefs.GetInt("Score2", 0);
            score3 = PlayerPrefs.GetInt("Score3", 0);
            UpdateText();
        }

        private void SaveAll()
        {
            PlayerPrefs.SetInt("Score2", score2);
            PlayerPrefs.SetInt("Score1", score1);
            PlayerPrefs.SetInt("Score3", score3);
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
}
