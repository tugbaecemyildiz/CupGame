using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Transform buttonParent;
    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private MythologicalCharacter[] characters;
    [SerializeField] private CharacterPanel characterPanel;

    private Button[] buttons;

    private void Start()
    {
        buttons = new Button[characters.Length];
        CreateButtons();
    }

    private void CreateButtons()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            GameObject button = Instantiate(buttonPrefab, buttonParent);
            Button btn = button.GetComponent<Button>();
            buttons[i] = btn;
            btn.interactable = false;

            Image buttonImage = button.GetComponent<Image>();
            if (buttonImage != null)
            {
                buttonImage.sprite = characters[i].CardFront;
            }

            if (btn != null)
            {
                MythologicalCharacter character = characters[i];
                btn.onClick.AddListener(() => {
                    if (FindObjectOfType<CardManager>().canClick)
                    {
                        ShowCharacterPanel(character);
                        FindObjectOfType<CardManager>().canClick = false;
                    }
                });
            }
        }
    }

    private void ShowCharacterPanel(MythologicalCharacter character)
    {
        if (characterPanel != null)
        {
            characterPanel.Setup(character);
        }
    }

    public void EnableButton(int index)
    {
        if (index >= 0 && index < buttons.Length)
        {
            buttons[index].interactable = true;
        }
    }

    public void SetAllButtonsInteractable(bool interactable)
    {
        foreach (var button in buttons)
        {
            if (button != null)
            {
                button.interactable = interactable;
            }
        }
    }
}
