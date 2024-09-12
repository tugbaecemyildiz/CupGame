using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    public Transform[] cardPositions;
    public CharacterPanel characterPanel;
    public GameObject endPanel;
    public List<MythologicalCharacter> characters;

    private List<GameObject> cards = new List<GameObject>();
    private CardFlip firstRevealedCard;
    private CardFlip secondRevealedCard;
    public bool canFlip = true;

    private void Start()
    {
        InitializeCards();
    }

    private void InitializeCards()
    {
        List<MythologicalCharacter> shuffledCharacters = CardUtility.GenerateCardFronts(characters);
        
        if (shuffledCharacters.Count > gridManager.GetCards().Count)
        {
            Debug.LogError("Yeterli kart pozisyonu yok.");
            return;
        }

        shuffledCharacters = CardUtility.ShuffleList(shuffledCharacters);

        List<GameObject> cardObjects = gridManager.GetCards();
        for (int i = 0; i < shuffledCharacters.Count; i++)
        {

            GameObject card = cardObjects[i];
            card.SetActive(true);
            CardFlip cardFlip = card.GetComponent<CardFlip>();
            cardFlip.SetCardFront(shuffledCharacters[i].CardFront);
            cardFlip.SetCardBack(shuffledCharacters[i].CardBack);
            cardFlip.SetCharacter(shuffledCharacters[i]);
            cardFlip.SetCardManager(this);
   
            cards.Add(card);  
        }
    }

    public void CardRevealed(CardFlip card)
    {
        if (!canFlip) return;
        if (firstRevealedCard == null)
        {
            firstRevealedCard = card;
        }
        else if (secondRevealedCard == null & firstRevealedCard != card)
        {
            secondRevealedCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        canFlip = false;
        yield return new WaitForSeconds(0.5f);

        if (firstRevealedCard.GetCardFront() == secondRevealedCard.GetCardFront())
        {
            firstRevealedCard.gameObject.SetActive(false);
            secondRevealedCard.gameObject.SetActive(false);

            characterPanel.Setup(firstRevealedCard.GetCharacter());
            while (characterPanel.gameObject.activeSelf)
            {
                yield return null;
            }
            CheckForEndGame();
        }
        else
        {
            firstRevealedCard.FlipCard(false);
            secondRevealedCard.FlipCard(false);
        }
        
        yield return new WaitForSeconds(0.2f);
        firstRevealedCard = null;
        secondRevealedCard = null;
        canFlip = true;
    }
    private void CheckForEndGame()
    {
        if (cards.All(card => !card.activeSelf))
        {
            ShowEndPanel();
        }
    }
    private void ShowEndPanel()
    {
        if(endPanel != null)
        {
            endPanel.SetActive(true);
        }
    }
}
