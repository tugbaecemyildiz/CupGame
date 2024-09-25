using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    private ButtonManager buttonManager;
    private CardMoves scoreManager;
    public Transform initialPosition;
    public Transform[] cardPositions;
    public CharacterPanel characterPanel;
    public GameObject endPanel;
    public TextMeshProUGUI endMessageText;
    public List<MythologicalCharacter> characters;

    private List<GameObject> cards = new List<GameObject>();
    private CardFlip firstRevealedCard;
    private CardFlip secondRevealedCard;

    public bool canFlip = true;
    public bool canClick = true;

    private void Start()
    {
        buttonManager = FindObjectOfType<ButtonManager>();
        InitializeCards();
        StartCoroutine(HandleCardSpreading());
        scoreManager = FindObjectOfType<CardMoves>();
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

            card.transform.position = initialPosition.position;
            cardFlip.FlipCard(false);
            cards.Add(card);
        }
    }

    private IEnumerator HandleCardSpreading()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(SpreadCards());
        FlipAllCards(true);
    }

    private IEnumerator SpreadCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Vector3 targetPosition = cardPositions[i].position;
            cards[i].transform.DOMove(targetPosition, 1f).SetEase(Ease.OutQuad);
        }
        yield return new WaitForSeconds(2f);
        FlipAllCards(true);
    }

    private void FlipAllCards(bool showFront)
    {
        foreach (var card in cards)
        {
            CardFlip cardFlip = card.GetComponent<CardFlip>();
            StartCoroutine(cardFlip.ShowPreview());
        }
    }

    public void CardRevealed(CardFlip card)
    {
        if (!canFlip) return;
        if (firstRevealedCard == null)
        {
            firstRevealedCard = card;
        }
        else if (secondRevealedCard == null && firstRevealedCard != card)
        {
            secondRevealedCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        canFlip = false;
        yield return new WaitForSeconds(0.5f);

        scoreManager.IncrementMoves();

        if (firstRevealedCard.GetCardFront() == secondRevealedCard.GetCardFront())
        {
            firstRevealedCard.gameObject.SetActive(false);
            secondRevealedCard.gameObject.SetActive(false);

            canClick = false;

            characterPanel.Setup(firstRevealedCard.GetCharacter());

            ButtonManager buttonManager = FindObjectOfType<ButtonManager>();
            if (buttonManager != null)
            {
                int index1 = characters.IndexOf(firstRevealedCard.GetCharacter());
                int index2 = characters.IndexOf(secondRevealedCard.GetCharacter());
                buttonManager.EnableButton(index1);
                buttonManager.EnableButton(index2);
            }

            while (characterPanel.gameObject.activeSelf)
            {
                yield return null;
            }

            canClick = true;
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
        if (endPanel != null && endMessageText != null)
        {
            endPanel.SetActive(true);
            int movesCount = scoreManager.GetMoves();
            endMessageText.text = $"Congratulations! You completed the game in {movesCount} moves.";
        }
    }
}
