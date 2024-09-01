using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    public MythologicalCharacter character;

    public Sprite chImage;
    public string nameText;
    public string descriptionText;

    
    public void SetupCharacter()
    {
        nameText = character.CharacterName;
        descriptionText = character.CharacterDescription;
        chImage = character.CharacterImage;
    }
}

