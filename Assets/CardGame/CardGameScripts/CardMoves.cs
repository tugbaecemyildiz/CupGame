using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardMoves : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI movesText;
    private int _movesCount = 0;

    private void Start()
    {
        UpdateMovesText();
    }
    public void IncrementMoves()
    {
        _movesCount++;
        UpdateMovesText();
    }
    private void UpdateMovesText()
    {
        movesText.text = "Moves: " + _movesCount.ToString();
    }
    public void ResetMoves()
    {
        _movesCount = 0;
        UpdateMovesText();
    }

    public int GetMoves()
    {
        return _movesCount;
    }
}
