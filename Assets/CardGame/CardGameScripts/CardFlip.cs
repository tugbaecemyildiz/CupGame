using DG.Tweening;
using UnityEngine;

public class CardFlip : MonoBehaviour
{
    public GameObject cardFront;
    public GameObject cardBack;

    private CardManager cardManager;
    private MythologicalCharacter _character;

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
        if (cardManager.canFlip)
            FlipCard(true);
    }

    public void FlipCard(bool flip)
    {
        if (flip)
        {
            transform.DORotate(new Vector3(0, 90, 0), 0.2f).OnComplete(() =>
            {
                transform.DORotate(new Vector3(0, 180, 0), 0.2f);
                cardManager?.CardRevealed(this);
            });
        }
        else
        {
            transform.DORotate(new Vector3(0, 90, 0), 0.2f).OnComplete(() =>
            {
                transform.DORotate(new Vector3(0, 0, 0), 0.2f);
            });
        }
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
