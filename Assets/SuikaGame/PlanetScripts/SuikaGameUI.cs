using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SuikaGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMoves;
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private TextMeshProUGUI levelUpText;
    [SerializeField] public Button bonusButton;
    [SerializeField] public Button addMovesButton;
    [SerializeField] private GameObject levelUpPanel;
    [SerializeField] private GameObject tryAgainPanel;
    [SerializeField] private GameObject winGamePanel;

    public void UpdateLevelText(int level)
    {
        levelUpText.text = "You have passed to level " + level;
        levelUpPanel.SetActive(true);
        textLevel.SetText("Level: " + level.ToString());
        levelUpPanel.transform.localScale = Vector3.zero;
        levelUpPanel.transform.DOScale(1.2f, 0.5f).OnComplete(() =>
        {
            levelUpPanel.transform.DOScale(1f, 0.3f).OnComplete(() =>
            {
                DOVirtual.DelayedCall(1f, () =>
                {
                    levelUpPanel.SetActive(false);
                });
            });
        });

        bonusButton.interactable = true;
        addMovesButton.interactable = true;
    }
    public void ActivateBonusTop()
    {
        bonusButton.interactable = false;
    }

    public void AddMoves(int movesLeft)
    {
        addMovesButton.interactable = false;
        UpdateTextMoves(movesLeft);
    }

    public void UpdateTextMoves(int movesLeft)
    {
        textMoves.text = "Moves: " + movesLeft.ToString();
    }
    
    public void ActivateTryAgainPanel()
    {
        tryAgainPanel.SetActive(true);
        tryAgainPanel.transform.DOScale(1.2f, 0.5f).OnComplete(() =>
        {
            levelUpPanel.transform.DOScale(1f, 0.3f);
        });
    }

    public void ActivateWinGamePanel()
    {
        winGamePanel.SetActive(true);
        winGamePanel.transform.DOScale(1.2f, 0.5f).OnComplete(() =>
        {
            levelUpPanel.transform.DOScale(1f, 0.3f);
        });
    }
}
