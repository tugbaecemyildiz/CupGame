using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CardFlip : MonoBehaviour
{
    public GameObject cardFront;
    public GameObject cardBack;

    private CardManager cardManager;
    private CardMoves scoreManager;
    private MythologicalCharacter _character;
    private bool isFrontVisible= false;

    private void Start()
    {
        scoreManager = FindObjectOfType<CardMoves>();
    }

    public void SetCardFront(Sprite frontSprite)
    {
        cardFront.GetComponent<SpriteRenderer>().sprite = frontSprite;
    }

    public void SetCardBack(Sprite backSprite)
    {
        cardBack.GetComponent<SpriteRenderer>().sprite = backSprite;
    }

    public void SetCardManager(CardManager manager)
    {
        cardManager = manager;
    }

    private void OnMouseDown()
    {
        if (cardManager.canFlip && !isFrontVisible)
            FlipCard(true);
    }

    public void FlipCard(bool flipToFront)
    {
        if (flipToFront && !isFrontVisible)
        {
            transform.DORotate(new Vector3(0, 90, 0), 0.2f).OnComplete(() =>
            {
                transform.DORotate(new Vector3(0, 180, 0), 0.2f);
                isFrontVisible = true;
                cardManager?.CardRevealed(this);
            });
        }
        else if(!flipToFront && isFrontVisible)
        {
            transform.DORotate(new Vector3(0, 90, 0), 0.2f).OnComplete(() =>
            {
                transform.DORotate(new Vector3(0, 0, 0), 0.2f);
                isFrontVisible = false;
            });
        }
    }

    public IEnumerator ShowPreview()
    {
        transform.DORotate(new Vector3(0, 90, 0), 0.2f).OnComplete(() =>
        {
            transform.DORotate(new Vector3(0, 180, 0), 0.2f);
            isFrontVisible = true;
        });
        yield return new WaitForSeconds(3f);
        transform.DORotate(new Vector3(0, 90, 0), 0.2f).OnComplete(() =>
        {
            transform.DORotate(new Vector3(0, 0, 0), 0.2f);
            isFrontVisible = false;
        });
    }

    public Sprite GetCardFront()
    {
        return cardFront.GetComponent<SpriteRenderer>().sprite;
    }

    public MythologicalCharacter GetCharacter()
    {
        return _character;
    }
    public void SetCharacter(MythologicalCharacter character)
    {
        _character = character;
    }

}
