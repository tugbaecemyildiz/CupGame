using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _characterNameText;
    [SerializeField] private TextMeshProUGUI _characterDescriptionText;
    [SerializeField] private Image _characterImage;

    [SerializeField] private CardManager _cardManager;

    public void Setup(MythologicalCharacter character)
    {
        _characterNameText.text = character.CharacterName;
        _characterDescriptionText.text = character.CharacterDescription;
        _characterImage.sprite = character.CharacterImage;

        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
        _cardManager.canFlip = true;
        _cardManager.canClick = true;
    }
}