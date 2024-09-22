using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CardFlip : MonoBehaviour
{
    public GameObject cardFront;
    public GameObject cardBack;

    private CardManager cardManager;
    private MythologicalCharacter _character;
    private bool isFrontVisible= false; //yeni eklendi

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
        Debug.Log("onmousedown");
        if (cardManager.canFlip && !isFrontVisible) //if (cardManager.canFlip)
            FlipCard(true);
    }

    public void FlipCard(bool flipToFront)
    {
        Debug.Log("flipcard");
        if (flipToFront && !isFrontVisible)//if (flip)
        {
            transform.DORotate(new Vector3(0, 90, 0), 0.2f).OnComplete(() =>
            {
                //cardBack.SetActive(false);
                //cardFront.SetActive(true);
                transform.DORotate(new Vector3(0, 180, 0), 0.2f);
                isFrontVisible = true;
                cardManager?.CardRevealed(this);
                //transform.DORotate(new Vector3(0, 180, 0), 0.2f);
                //cardManager?.CardRevealed(this);
            });
        }
        else if(!flipToFront && isFrontVisible)
        {
            transform.DORotate(new Vector3(0, 90, 0), 0.2f).OnComplete(() =>
            {
                //cardFront.SetActive(false);
                //cardBack.SetActive(true);
                transform.DORotate(new Vector3(0, 0, 0), 0.2f);
                isFrontVisible = false;
                //transform.DORotate(new Vector3(0, 0, 0), 0.2f);
            });
        }
    }

    public IEnumerator ShowPreview()
    {
        Debug.Log("çalýþtým");
        transform.DORotate(new Vector3(0, 90, 0), 0.2f).OnComplete(() =>
        {
            transform.DORotate(new Vector3(0, 180, 0), 0.2f);
            isFrontVisible = true;
        });
        yield return new WaitForSeconds(1f);
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
